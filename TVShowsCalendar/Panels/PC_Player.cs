using Extensions;

using ShowsRenamer.Module.Classes;

using SlickControls;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

using YoutubeExplode.Models;
using YoutubeExplode.Models.MediaStreams;

using MediaStates = Vlc.DotNet.Core.Interops.Signatures.MediaStates;
using ProjectImages = ShowsCalendar.Properties.Resources;

namespace ShowsCalendar
{
#pragma warning disable CS0618 // Type or member is obsolete

	public partial class PC_Player : PanelContent
	{
		internal enum ScreenState { Normal, FullScreen, MiniPlayer, PictureInPicture }

		public const int GWL_STYLE = -16;
		public const uint WS_VISIBLE = 0X94000000;

		private static bool AnyWindowFullScreen;

		private int CONTROLS_SIZE => MiniPlayer || PnP ? 36 :
									(int)(Math.Round(14 * UI.UIScale) + SS_TimeSlider.Padding.Vertical + 30 + 7 + TLP_MoreInfo.Visible.If(UI.Font(8.25F).Height, 0));

		private bool ControlsOpening => (controlsHideAnimation?.Animating ?? false) && controlsHideAnimation.NewBounds.Height > 1;

		#region Fields

		private readonly WaitIdentifier bufferIdentifier = new WaitIdentifier();
		private readonly WaitIdentifier controlsHideIdentifier = new WaitIdentifier();
		private readonly WaitIdentifier lazyPlayerWaitIdentifier = new WaitIdentifier();
		private readonly WaitIdentifier pauseWaitIdentifier = new WaitIdentifier();
		private readonly WaitIdentifier subDelayWaitIdentifier = new WaitIdentifier();
		private readonly WaitIdentifier topNotchIdentifier = new WaitIdentifier();
		private readonly WaitIdentifier volumeIdentifier = new WaitIdentifier();
		private readonly WaitIdentifier upnextHideIdentifier = new WaitIdentifier();
		private readonly WaitIdentifier subDelaySetIdentifier = new WaitIdentifier();
		private bool cancelDispose;
		private AnimationHandler controlsHideAnimation;
		private FormDesign currentDesign;
		private MuxedStreamInfo currentStream;
		private bool firstLoad;
		private FullScreenPlayer fullScreenPlayer;
		private bool hoveredTime;
		private Rectangle lastBounds;
		private DateTime lastClick = DateTime.MinValue;
		private Rectangle? lastMiniBounds;
		private Point LastMousePoint = Point.Empty;
		private DateTime lastMouseVolumeUp = DateTime.MinValue;
		private ScreenState lastScreenState;
		private FormWindowState lastState;
		private AnimationHandler miniPlayerAnimation;
		private bool miniPlayerMoving;
		private MouseDetector mouseDetector;
		private MouseHook mouseHook;
		private bool pausedFromFocus;
		private bool pausedFromScroll;
		private Point startMiniPlayerMoveCursor;
		private bool stateChanging;
		private AnimationHandler subDelayAnimation;
		private bool timeLoop = true;
		private bool togglingPnP;
		private bool pauseAfterBuffer;
		private bool isMuted;
		private bool upnextHidden;
		private bool firstUpNextShown;
		private AnimationHandler topNotchAnimation;
		private AnimationHandler upNextAnimation;
		private AnimationHandler ratingAnimation;
		private Size videoInformation = new Size(16, 9);
		private AnimationHandler volumeAnimation;
		private System.Timers.Timer watchTimeTimer;

		#endregion Fields

		#region Properties

		public Episode Episode { get; private set; }
		public bool FullScreen => CurrentScreenState == ScreenState.FullScreen;
		public bool MiniPlayer => CurrentScreenState == ScreenState.MiniPlayer;
		public Movie Movie { get; private set; }
		public bool Paused => !(vlcControl?.IsPlaying ?? false);
		public bool PnP => CurrentScreenState == ScreenState.PictureInPicture;
		public Season Season { get; private set; }
		public MediaStreamInfoSet StreamInfo { get; private set; }
		public bool Streaming { get; private set; }
		public Video StreamingVideo { get; private set; }
		public TvShow TvShow { get; private set; }
		public FileInfo VidFile { get; private set; }
		private ScreenState CurrentScreenState { get; set; }

		#endregion Properties

		#region Constructors

		public PC_Player(FileInfo file, bool findRelatedMedia = false) : this()
		{
			SetFile(file, findRelatedMedia);
			if (!findRelatedMedia)
			{
				TLP_MoreInfo.Visible = TLP_Suggestions.Visible = false;
			}
		}

		public PC_Player(Episode ep, FileInfo file) : this()
		{
			SetFile(file);
			SetEpisode(ep);
			SL_Next.Visible = SL_Previous.Visible = true;
		}

		public PC_Player(Movie movie, FileInfo file) : this()
		{
			SetFile(file);
			SetMovie(movie);
		}

		public PC_Player(Movie movie, Video video) : this(video)
		{
			SetMovie(movie);
		}

		public PC_Player(TvShow show, Video video) : this(video)
		{
			SetShow(show);
		}

		public PC_Player(Season season, Video video) : this(video)
		{
			SetSeason(season);
		}

		public PC_Player(Episode episode, Video video) : this(video)
		{
			SetEpisode(episode);
		}

		private PC_Player()
		{
			InitializeComponent();

			vlcControl = new Vlc.DotNet.Forms.VlcControl();

			vlcControl.BeginInit();
			vlcControl.Dock = DockStyle.Fill;
			vlcControl.TabStop = false;
			vlcControl.Spu = -1;
			vlcControl.TabIndex = 0;
			vlcControl.VlcLibDirectory = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "vlc"));
			vlcControl.VlcMediaplayerOptions = Array.Empty<string>();
			vlcControl.Playing += vlcControl_Playing;
			vlcControl.TimeChanged += vlcControl_TimeChanged;
			vlcControl.EndReached += VlcControl_EndReached;
			vlcControl.Buffering += VlcControl_Buffering;
			vlcControl.EndInit();

			SlickTip.SetTo(SL_Backwards, $"Go Back {Data.Options.BackwardTime} Seconds");
			SlickTip.SetTo(SL_Forward, $"Go Forward {Data.Options.ForwardTime} Seconds");
			SlickTip.SetTo(SL_Previous, "Switch to Previous Episode");
			SlickTip.SetTo(SL_Next, "Switch to Next Episode");
			SlickTip.SetTo(SL_Play, "Play / Pause");
			SlickTip.SetTo(SL_More, "More Options");
			SlickTip.SetTo(SL_Subs, "Select Subtitles");
			SlickTip.SetTo(SL_Audio, "Select Sound Track");
			SlickTip.SetTo(SL_PnP, "Toggle Picture-in-Picture (Ctrl + P)");
			SlickTip.SetTo(SL_MiniPlayer, "Toggle Mini-Player (Ctrl + Tab)");
			SlickTip.SetTo(SL_FullScreen, "Toggle Full-screen (Alt + Enter)");

			Data.ActivePlayer = this;

			slickScroll.CustomSizeSource = () =>
			{
				if (MiniPlayer || PnP || !TLP_MoreInfo.Visible)
				{
					return 0;
				}

				var c = TLP_SimilarContent.Visible ? (Control)TLP_SimilarContent : SP_Cast;

				return c.Height
					+ c.Top
					+ TLP_Suggestions.Top
					+ P_Info.Top;
			};
		}

		private PC_Player(Video video) : this()
		{
			SetStream(video);
		}

		#endregion Constructors

		#region Media

		public void ClearMedia()
		{
			Streaming = firstUpNextShown = false;
			Movie = null;
			Episode = null;
			Season = null;
			TvShow = null;
			StreamingVideo = null;
			StreamInfo = null;
			upnextHidden = false;
			thumbs?.Dispose();
			thumbs = null;
			watchTimeTimer?.Dispose();

			this.TryInvoke(() =>
			{
				upnextHidden = false;
				UpNextCountdown.Visible = PB_Thumbnail.Visible = TLP_MoreInfo.Visible = TLP_Suggestions.Visible = PB_UpNext.Visible = C_Rate.Visible = false;
				SP_Cast.Controls.Clear(true);
				SP_Crew.Controls.Clear(true);
				TLP_Suggestions.Controls.Clear(true, x => x is IWatchControl);
			});
		}

		public void SetFile(FileInfo file, bool findRelatedMedia = false)
		{
			this.TryInvoke(() =>
			{
				Text = file.FileName().FormatWords();
				vlcControl.Parent = null;
				PB_Loader.Visible = true;
			});

			if (vlcControl.IsDisposed)
			{
				return;
			}

			VidFile = file;
			firstLoad = true;

			Task.Run(() =>
			{
				vlcControl.Stop();
				vlcControl.SetMedia(file, GetSubs(file, true).Select(x => $"sub-file={x}").ToArray());

				if (IsHandleCreated)
				{
					Play();
				}
			});

			ShowPlayControls();

			if (findRelatedMedia)
			{
				var ep = LocalShowHandler.MatchFile(ShowManager.Shows, file);
				if (ep != null)
				{
					SetEpisode(ep);
				}
				else
				{
					var mov = LocalMovieHandler.MatchFile(MovieManager.Movies, file);
					if (mov != null)
					{
						SetMovie(mov);
					}
					else
					{
						TLP_MoreInfo.Visible = TLP_Suggestions.Visible = false;
					}
				}
			}
		}

		public void SetEpisode(Episode ep)
		{
			if (ep != null)
			{
				StartLoader();

				Episode = ep;

				this.TryInvoke(() =>
				{
					TLP_MoreInfo.Visible = TLP_Suggestions.Visible = true;
					vlcControl.Visible = true;
					slickScroll.SetPercentage(0);
					enableControls(false);

					Text = $"{ep.Show} • {ep}";

					if (Streaming)
						Text += $" • {StreamingVideo.Title}";

					if (!Streaming)
					{
						L_Plot.Text = string.IsNullOrWhiteSpace(ep.Overview) ? "No Overview" : ep.Overview;
						L_ShowPlot.Text = ep.Show.Overview.IfEmpty("No Overview");
						L_ShowPlot.Visible = L_ShowPlotLabel.Visible = true;
					}

					SP_Cast.Info = "From your beloved cast to this episode's notable guest stars";
					SP_Cast.Controls.Clear(true);
					foreach (var cast in Episode.Season.Credits.Cast.Concat(Episode.GuestStars).Distinct(x => x.Id).Take(30))
					{
						SP_Cast.Add(new CharacterControl(cast));
					}

					SP_Crew.Controls.Clear(true);
					foreach (var crew in Episode.Crew.Where(x => !string.IsNullOrWhiteSpace(x.ProfilePath)).Take(30))
					{
						SP_Crew.Add(new CharacterControl(crew));
					}

					setSimilarCharacters(Episode.Season.Credits.Cast.Concat(Episode.GuestStars));

					if (Streaming)
					{
						return;
					}

					TLP_Suggestions.Controls.Clear(true, x => x is IWatchControl);
					TLP_Suggestions.Controls.Add(new WatchControl<Episode>(ep, true, "NOW PLAYING") { Enabled = false, Anchor = AnchorStyles.Left }, 1, 1);

					var next = ep.Next;

					TLP_Suggestions.SetColumnSpan(TLP_Info, next != null ? 1 : 2);

					if (next != null)
					{
						TLP_Suggestions.Controls.Add(new WatchControl<Episode>(next, true, "UP NEXT") { Anchor = AnchorStyles.Right, TabStop = false }, 3, 1);
					}

					if (next != null && next.AirState == AirStateEnum.Aired && !(next.VidFiles.Any(y => y.Exists)))
					{
						var episodes = next.Season.Episodes.All(predicate) ? next.Season.Episodes.ToArray() : new[] { next };

						Data.Mainform.TryInvoke(() =>
						{
							Notification.Create(
								(f, x) => ShowManager.PaintEpNotification(f, episodes, x)
								, () => 
								{
									ToggleScreen(ScreenState.Normal);
									if (!Paused)
									{
										Pause();
									}
									Data.Mainform.ShowUp();
									Data.Mainform.PushPanel(null, episodes.Length == 1 ? new PC_Download(next) : new PC_Download(next.Season));
								}
								, NotificationSound.None
								, new Size(220, 110))
								.Show(Data.Mainform, 15)
								.PictureBox.GetImage(next.BackdropPath, 220, false);
						});

						bool predicate(Episode x) =>
								x.EN > 0 &&
								x.AirState == AirStateEnum.Aired &&
								!x.Watched &&
								!x.Playable;

						foreach (var e in episodes)
							e.LastReminder = DateTime.Today;

						next.Show.Save(ChangeType.Preferences);
					}

					SL_Next.Enabled = Episode.Next?.Playable ?? false;
					SL_Previous.Enabled = Episode.Previous?.Playable ?? false;
				});

				if (!Streaming && Episode.WatchTime > 0)
					Task.Run(() =>
					{
						vlcControl.Play();
						vlcControl.Time = Episode.WatchTime - 10000;
					});
			}
		}

		public void SetMovie(Movie movie)
		{
			if (movie != null)
			{
				StartLoader();

				Movie = Movie ?? movie;

				this.TryInvoke(() =>
				{
					TLP_MoreInfo.Visible = TLP_Suggestions.Visible = true;
					slickScroll.SetPercentage(0);
					enableControls(false);

					Text = movie.ToString();

					if (Streaming)
						Text += $" • {StreamingVideo.Title}";

					if (!Streaming)
					{
						L_Plot.Text = string.IsNullOrWhiteSpace(Movie.Overview) ? "No Overview" : Movie.Overview;
					}

					L_ShowPlot.Visible = L_ShowPlotLabel.Visible = false;

					SP_Cast.Info = "Meet the actors behind the movie.";
					SP_Cast.Controls.Clear(true);
					foreach (var cast in Movie.Cast.Take(30))
					{
						SP_Cast.Add(new CharacterControl(cast));
					}

					SP_Crew.Controls.Clear(true);
					foreach (var crew in Movie.Crew.Where(x => !string.IsNullOrWhiteSpace(x.ProfilePath)).Take(30))
					{
						SP_Crew.Add(new CharacterControl(crew));
					}

					setSimilarCharacters(Movie.Cast);

					if (Streaming)
					{
						return;
					}

					TLP_Suggestions.Controls.Clear(true, x => x is IWatchControl);
					TLP_Suggestions.Controls.Add(new WatchControl<Movie>(movie, true, "NOW PLAYING") { Enabled = false, Anchor = AnchorStyles.Left }, 1, 1);

					TLP_Suggestions.SetColumnSpan(TLP_Info, 2);
				});

				if (!Streaming && Movie.WatchTime > 0)
					Task.Run(() =>
					{
						vlcControl.Play();
						vlcControl.Time = Movie.WatchTime - 10000;
					});
			}
		}

		public void SetSeason(Season season)
		{
			if (season != null)
			{
				StartLoader();

				Season = Season ?? season;

				this.TryInvoke(() =>
				{
					vlcControl.Visible = true;
					slickScroll.SetPercentage(0);
					enableControls(false);

					Text = $"{season.Show} • {season}";

					if (Streaming)
						Text += $" • {StreamingVideo.Title}";

					SP_Cast.Controls.Clear(true);
					foreach (var cast in Season.Credits.Cast.Distinct(x => x.Id).Take(30))
					{
						SP_Cast.Add(new CharacterControl(cast));
					}

					SP_Crew.Controls.Clear(true);

					setSimilarCharacters(Season.Credits.Cast);
				});
			}
		}

		public void SetShow(TvShow show)
		{
			if (show != null)
			{
				StartLoader();

				TvShow = TvShow ?? show;

				this.TryInvoke(() =>
				{
					TLP_MoreInfo.Visible = TLP_Suggestions.Visible = true;
					vlcControl.Visible = true;
					slickScroll.SetPercentage(0);
					enableControls(false);

					Text = show.ToString();

					if (Streaming)
						Text += $" • {StreamingVideo.Title}";

					SP_Cast.Controls.Clear(true);
					foreach (var cast in TvShow.Cast.Take(30))
					{
						SP_Cast.Add(new CharacterControl(cast));
					}

					SP_Crew.Controls.Clear(true);
					foreach (var crew in TvShow.Crew.Where(x => !string.IsNullOrWhiteSpace(x.ProfilePath)).Take(30))
					{
						SP_Crew.Add(new CharacterControl(crew));
					}

					setSimilarCharacters(TvShow.Cast);
				});
			}
		}

		public void SetStream(Video video)
		{
			Streaming = true;
			StreamingVideo = video;

			TLP_Suggestions.Controls.Add(new YoutubeControl(StreamingVideo, "NOW PLAYING") { Enabled = false, Anchor = AnchorStyles.Left }, 1, 1);
			TLP_Suggestions.SetColumnSpan(TLP_Info, 2);

			L_PlotLabel.Text = "Video Description";
			L_Plot.Text = video.Description;
			L_ShowPlotLabel.Text = "Author";
			L_ShowPlot.Text = video.Author;

			L_ShowPlot.Visible = L_ShowPlotLabel.Visible = !string.IsNullOrWhiteSpace(L_ShowPlot.Text);
			L_PlotLabel.Visible = L_Plot.Visible = !string.IsNullOrWhiteSpace(L_Plot.Text);

			vlcControl.Parent = null;
			PB_Loader.Visible = true;

			new BackgroundAction(async () =>
			{
				if (vlcControl.IsDisposed)
				{
					return;
				}

				firstLoad = true;

				try
				{
					StreamInfo = await Data.YoutubeClient.GetVideoMediaStreamInfosAsync(StreamingVideo.Id);

					SetVideoStream(StreamInfo.Muxed.WithHighestVideoQuality());

					if (IsHandleCreated)
					{
						Play();
					}

					ShowPlayControls();
				}
				catch
				{
					try { System.Diagnostics.Process.Start($"https://www.youtube.com/watch?v={StreamingVideo.Id}"); }
					catch { Cursor.Current = Cursors.Default; MessagePrompt.Show("Could not open the link because you do not have a default browser selected", "No Browser Selected", PromptButtons.OK, PromptIcons.Error); }

					Form.TryInvoke(() => Form.PushBack());
				}
			}).Run();
		}

		private void FileLoaded()
		{
			Task.Run(() =>
			{
				var subs = vlcControl.SubTitles.All.ToArray();
				var auds = vlcControl.Audio.Tracks.All.ToArray();
				var fps = vlcControl.VlcMediaPlayer.FramesPerSecond.ToString("0.#");
				videoInformation = vlcControl.GetCurrentMedia().TracksInformations.FirstOrDefault(mediaInformation => mediaInformation.Type == Vlc.DotNet.Core.Interops.Signatures.MediaTrackTypes.Video).Video.As(x => new Size(((int)x.Width).If(0, 16), ((int)x.Height).If(0, 9)));

				if (subs.Any(x => x.ID != -1))
				{
					vlcControl.SubTitles.Current = subs.FirstOrDefault(x => x.Name.ContainsWord("english", true)) ?? subs.FirstOrDefault(x => x.Name.ContainsWord("en", true)) ?? subs.FirstOrDefault(x => x.ID != -1);
				}

				if (auds.Length > 0)
				{
					vlcControl.Audio.Tracks.Current = auds.FirstOrDefault(x => x.Name.ContainsWord("english", true)) ?? auds.FirstOrDefault(x => x.Name.ContainsWord("en", true)) ?? auds.FirstOrDefault(x => x.ID != -1);
				}

				this.TryInvoke(() =>
				{
					SL_Subs.Enabled = subs.Any(x => x.ID != -1);
					SL_Audio.Enabled = auds.Length > 0;
					enableControls(true);
					PC_Player_Resize(null, null);

					var notchOpen = L_TopNotch.Width == L_TopNotch.MaximumSize.Width && L_TopNotch.MaximumSize.Width > 0;
					using (var g = CreateGraphics())
						L_TopNotch.MaximumSize = new Size((int)g.Measure(Text.ToUpper(), UI.Font(12.75F, FontStyle.Bold)).Width + 8, 8 + (UI.Font(12.75F, FontStyle.Bold).Height * 12 / 10));

					L_TopNotch.MinimumSize = new Size(0, L_TopNotch.MaximumSize.Height);

					if (notchOpen)
						L_TopNotch.Size = L_TopNotch.MaximumSize;
					else
						L_TopNotch.Height = L_TopNotch.MaximumSize.Height;

					ShowPlayControls();
					StopLoader();

					if (PnP && P_BackContent.Parent is PictureInPictureControl pnp)
					{
						var h = P_VLC.Height;
						var w = h * videoInformation.Width / videoInformation.Height;

						pnp.Size = new Size(w + 2, h + 2);
					}

					PB_Thumb.Size = UI.Scale(new Size(videoInformation.Width * 90 / videoInformation.Height, 90), UI.UIScale);
				});

				if (!Streaming)
				{
					thumbs = new ThumbnailCollection(VidFile, vlcControl.Length);
					thumbs.ImageLoaded += (s, e) =>
					{
						if (PB_Thumb.Visible)
						{
							this.TryInvoke(() => { SS_TimeSlider_MouseMove(null, new MouseEventArgs(MouseButtons.Left, 1, SS_TimeSlider.PointToClient(Cursor.Position).X, 0, 0)); });
						}
					};
				}

				if (string.IsNullOrWhiteSpace(ISave.CustomSaveDirectory) || new DirectoryInfo(ISave.CustomSaveDirectory).IsNetwork())
				{
					watchTimeTimer?.Dispose();
					watchTimeTimer = new System.Timers.Timer(5000);
					watchTimeTimer.Elapsed += (s, e) => { if (!Paused) { SaveWatchtime(); } };
					watchTimeTimer.Start();
				}
			});

			new BackgroundAction(() =>
			{
				if (Movie != null)
				{
					PB_Thumbnail.Image = ImageHandler.GetImage(Movie.BackdropPath, 0, false);
				}
				else if (Episode != null)
				{
					PB_Thumbnail.Image = ImageHandler.GetImage(Episode.StillPath.IfEmpty(Episode.Show.BackdropPath), 0, false);

					if (!Streaming && Episode != null && (Episode.Next?.Playable ?? false))
					{
						PB_UpNext.Image = ImageHandler.GetImage(Episode.Next.StillPath.IfEmpty(Episode.Show.BackdropPath), 192, false);
						UpNextCountdown.Image = ImageHandler.GetImage(Episode.Next.StillPath.IfEmpty(Episode.Show.BackdropPath), 0, false);

						if (PB_UpNext.Image == null || UpNextCountdown.Image == null)
							PB_UpNext.Image = UpNextCountdown.Image = Episode.Next.VidFiles.FirstOrDefault().Info.GetThumbnail();
					}
				}
				else if (Season != null)
				{
					PB_Thumbnail.Image = ImageHandler.GetImage(Season.Show.BackdropPath, 0, false);
				}
				else if (TvShow != null)
				{
					PB_Thumbnail.Image = ImageHandler.GetImage(TvShow.BackdropPath, 0, false);
				}

				if (PB_Thumbnail.Image == null)
					PB_Thumbnail.Image = VidFile.GetThumbnail();
			}).Run();
		}

		private IEnumerable<string> GetSubs(FileInfo epFile, bool movie)
		{
			var subs = movie
				? new MovieFile(epFile).GetSubtitleFiles(new NamingStyle())
				: new EpisodeFile(epFile).GetSubtitleFiles(LocalShowHandler.GetShowFolder(Episode.Show, new EpisodeFile(epFile)), new NamingStyle());

			subNames = new List<string>();
			foreach (var item in subs)
			{
				if ($"{item.CurrentFile.DirectoryName}\\{item.CurrentFile.FileName()}" != $"{epFile.DirectoryName}\\{epFile.FileName()}")
				{
					subNames.Add(item.Language.IfEmpty(item.CurrentFile.FileName()));
					yield return item.CurrentFile.FullName;
				}
			}
		}

		private void SetVideoStream(MuxedStreamInfo muxedStreamInfo)
		{
			vlcControl.SetMedia(new Uri(muxedStreamInfo.Url));
			currentStream = muxedStreamInfo;
		}

		private void VlcControl_Buffering(object sender, Vlc.DotNet.Core.VlcMediaPlayerBufferingEventArgs e)
		{
			if (!buffering && !Paused)
			{
				buffering = true;

				if (isMuted)
				{
					Task.Run(() => vlcControl.Audio.IsMute = true);
				}

				this.TryInvoke(() =>
				{
					vlcControl.Parent = null;
					PB_Loader.Visible = true;
				});
			}
		}

		private void VlcControl_EndReached(object sender, Vlc.DotNet.Core.VlcMediaPlayerEndReachedEventArgs e)
		{
			if (SS_TimeSlider.Value / SS_TimeSlider.MaxValue < .95)
			{
				Replay((long)SS_TimeSlider.Value);

				return;
			}

			SaveWatchtime();
			ShowPlayControls();

			this.TryInvoke(() =>
			{
				SL_Play.Image = ProjectImages.Tiny_PlayNoBorder;
				enableControls(false);

				if (!Streaming && Episode != null && (Episode.Next?.Playable ?? false))
				{
					PB_UpNext.Visible = C_Rate.Visible = false;
					upnextHidden = false;
					UpNextCountdown.Visible = true;

					startCountdown();
				}
				else
				{
					PB_Thumbnail.Visible = true;
					SL_Play.Enabled = true;
				}

				vlcControl.Parent = null;
				PB_Loader.Visible = false;
			});
		}

		#endregion Media

		#region Player Management

		public void SaveWatchtime()
		{
			if (Streaming)
			{
				return;
			}

			var current = SS_TimeSlider.Value;
			var total = SS_TimeSlider.MaxValue;

			if (Episode != null)
			{
				if (total > 0 && current > 10000)
				{
					var ended = current > total * 90 / 100;
					Episode.WatchTime = ended ? 0 : (long)current;
					Episode.Watched = Episode.Watched || ended;
					Episode.WatchDate = DateTime.Now;
					Episode.Progress = Episode.Started ? 100 * current / total : 0;

					new BackgroundAction(() =>
					{
						Episode.Save(ChangeType.Preferences);
						LocalShowHandler.OnWatchInfoChanged(Episode?.Show, this);
					}).Run();
				}
			}
			else if (Movie != null)
			{
				if (total > 0 && current > 10000)
				{
					var ended = current > total * 90 / 100;
					Movie.WatchTime = ended ? 0 : (long)current;
					Movie.WatchDate = DateTime.Now;
					Movie.Watched = Movie.Watched || ended;
					Movie.Progress = Movie.Started ? 100 * current / total : 0;

					new BackgroundAction(() =>
					{
						Movie.Save(ChangeType.Preferences);
						LocalMovieHandler.OnWatchInfoChanged(Movie, this);
					}).Run();
				}
			}
		}

		internal void ToggleScreen(ScreenState value)
		{
			if (Form.WindowState == FormWindowState.Maximized && !FullScreen && value == ScreenState.Normal) value = ScreenState.FullScreen;

			if (CurrentScreenState == value && !stateChanging)
			{
				return;
			}

			if (value == ScreenState.MiniPlayer || (value == ScreenState.Normal && CurrentScreenState == ScreenState.MiniPlayer))
			{
				Form.Opacity = 0;
			}

			Form.CurrentFormState = FormState.ForcedFocused;
			Form.FreezeFocus = true;
			Form.FullScreenIgnoresTaskbar = value == ScreenState.FullScreen;
			stateChanging = true;
			miniPlayerMoving = false;
			vlcControl.Parent = null;
			SL_PnP.Visible = value != ScreenState.MiniPlayer;
			SL_MiniPlayer.Visible = value != ScreenState.PictureInPicture;

			var pnpForm = PnP ? P_BackContent.Parent : null;

			if (PnP)
			{
				Form.PushPanel(null, this);
				PB_Close.Visible = false;
			}

			if (CurrentScreenState == ScreenState.Normal || PnP)
			{
				lastState = Form.WindowState;
			}

			var resetSize = CurrentScreenState == ScreenState.MiniPlayer && value == ScreenState.FullScreen;

			lastScreenState = CurrentScreenState;
			CurrentScreenState = value;

			if (value == ScreenState.PictureInPicture)
			{
				Visible = false;
				Application.DoEvents();
			}

			if (!System.Diagnostics.Debugger.IsAttached && (FullScreen || (Form.WindowState == FormWindowState.Maximized && value == ScreenState.Normal)))
			{
				fullScreenPlayer = new FullScreenPlayer(this);
				fullScreenPlayer.Show();
			}

			if (resetSize)
			{
				Form.Bounds = lastBounds;
			}

			if (value == ScreenState.Normal)
			{
				P_BackContent.Parent = this;
				P_BotSpacer.Parent = P_AllContent;
				P_BotSpacer.Dock = DockStyle.Top;
				P_BackContent.BringToFront();
				P_BotSpacer.SendToBack();
				P_VLC.SendToBack();
				controlsHideAnimation?.StopAnimation();
				Form.WindowState = lastState;
				P_VLC.Height = Height - P_BotSpacer.Height - Padding.Top - TLP_MoreInfo.Height;
				if (P_BotSpacer.Height != CONTROLS_SIZE && IsHandleCreated)
					BeginInvoke(new Action(() => { P_BotSpacer.Height = CONTROLS_SIZE; PC_Player_Resize(null, null); }));
			}
			else
			{
				if (value != ScreenState.PictureInPicture)
				{
					P_BackContent.Parent = Form;
				}

				P_BackContent.BringToFront();
				P_BotSpacer.Parent = P_VLC;
				P_BotSpacer.Dock = DockStyle.Bottom;
				P_BotSpacer.BringToFront();
				slickScroll.SetPercentage(0, true);
			}

			if (value == ScreenState.MiniPlayer || value == ScreenState.PictureInPicture)
			{
				SS_TimeSlider.Visible = L_Time.Visible = L_CurrentTime.Visible = false;
				P_Progress.Height = 2;
				TLP_Controls.Controls.Add(P_Progress, 0, 0);
				TLP_Controls.SetColumnSpan(P_Progress, 3);
				P_BotSpacer.Padding = new Padding(0);
				P_BotSpacer.Height = CONTROLS_SIZE;
			}
			else
			{
				P_Progress.Parent = P_VLC;
				P_Progress.Height = 1;
				P_Progress.SendToBack();
			}

			if (value == ScreenState.MiniPlayer)
			{
				if (!Data.Options.TopMostPlayer)
				{
					new BackgroundAction(IsForegroundFullScreen).Run();
				}

				SL_MiniPlayer.Image = ProjectImages.Tiny_RestoreWindow;
				P_Info.Visible = false;
				Notification.Clear();
			}
			else
			{
				Form.TopMost = false;
				SL_MiniPlayer.Image = ProjectImages.Tiny_MiniWindow;
				P_Info.Visible = true;

				if (value != ScreenState.PictureInPicture)
				{
					SS_TimeSlider.Visible = L_Time.Visible = L_CurrentTime.Visible = true;
					P_BotSpacer.Padding = new Padding(0, 2, 0, 0);
				}

				Form.MinimumSize = new Size(765, 445);
				if (Form.WindowState != FormWindowState.Maximized && (value == ScreenState.Normal || value == ScreenState.PictureInPicture))
				{
					Form.Bounds = lastBounds;
				}
			}

			if (value == ScreenState.FullScreen)
			{
				SL_FullScreen.Image = ProjectImages.Tiny_NormalScreen;
				P_VLC.Height = Screen.FromControl(this).Bounds.Height;
				Form.WindowState = FormWindowState.Maximized;
				L_TopNotch.Visible = true;
				L_TopNotch.BringToFront();

				if (FormDesign.Design.Name != "Theater")
				{
					currentDesign = FormDesign.Design;
				}

				var design = new FormDesign("Theater", FormDesign.Design.ID, FormDesignType.Dark, true)
				{
					BackColor = Color.Black,
					MenuColor = Color.Black,
					ActiveColor = FormDesign.Design.ActiveColor,
					GreenColor = FormDesign.Design.GreenColor,
					YellowColor = FormDesign.Design.YellowColor,
					RedColor = FormDesign.Design.RedColor,

					ActiveForeColor = FormDesign.Dark.ActiveForeColor,
					ForeColor = FormDesign.Dark.ForeColor,
					ButtonColor = FormDesign.Dark.ButtonColor,
					ButtonForeColor = FormDesign.Dark.ButtonForeColor,
					AccentColor = FormDesign.Dark.AccentColor,
					MenuForeColor = FormDesign.Dark.MenuForeColor,
					LabelColor = FormDesign.Dark.LabelColor,
					InfoColor = FormDesign.Dark.InfoColor,
					IconColor = FormDesign.Dark.IconColor
				};

				design.DarkMode = design;

				FormDesign.Switch(design, false, true);
			}
			else
			{
				SL_FullScreen.Image = ProjectImages.Tiny_Fullscreen;
				topNotchIdentifier?.Cancel();
				L_TopNotch.Width = 0;
				L_TopNotch.Visible = false;

				if (FormDesign.Design.Temporary)
				{
					FormDesign.Switch(currentDesign, false, true);
				}
			}

			PB_Volume.Visible = false;
			PB_Volume.BringToFront();

			if (value == ScreenState.MiniPlayer)
			{
				Form.WindowState = FormWindowState.Normal;
				Form.MinimumSize = new Size((150 * videoInformation.Width / videoInformation.Height) - Form.Padding.Horizontal, 150);

				Form_ResizeEnd(null, null);
			}

			if (value == ScreenState.PictureInPicture)
			{
				P_VLC.Height = 210;
				SL_MiniPlayer.Image = ProjectImages.Tiny_RestoreWindow;
				P_Info.Visible = false;

				var h = (int)(P_VLC.Height * UI.UIScale);
				var w = h * videoInformation.Width / videoInformation.Height;
				var pnp = new PictureInPictureControl(this)
				{
					Bounds = new Rectangle(
						new Point(Form.Width - Form.Padding.Right - w - 12, Form.Height - Form.Padding.Bottom - h - 12),
						new Size(w + 2, h + 2))
				};

				P_BackContent.Parent = pnp;
				P_AllContent.MaximumSize = new Size(P_BackContent.Width, int.MaxValue);
				P_AllContent.MinimumSize = new Size(P_BackContent.Width, 0);
				pnp.BringToFront();
			}

			vlcControl.Parent = P_VLC;

			SL_Previous.Visible = SL_Next.Visible = !MiniPlayer && !PnP && Episode != null;

			ShowPlayControls();

			PC_Player_Resize(null, null);

			pnpForm?.Dispose();

			stateChanging = false;
			Form.FreezeFocus = false;
			Form.CurrentFormState = FormState.NormalFocused;
			Form.Activate();

			Form.OnNextIdle(() =>
			{
				Form.Opacity = 1;
				if (PB_UpNext.Visible)
					showUpNextPopup();
				if (C_Rate.Visible)
					showRatePopup();
				fullScreenPlayer?.Dispose();
			});
		}

		private void enableControls(bool enabled)
		{
			SS_TimeSlider.Enabled = SS_Volume.Enabled = SL_Subs.Enabled = SL_Audio.Enabled
				= SL_Previous.Enabled = SL_Backwards.Enabled = SL_Play.Enabled = SL_Forward.Enabled
				= SL_Next.Enabled =
				enabled;
		}

		private string getSnapShotFile()
		{
			var ts = TimeSpan.FromMilliseconds(SS_TimeSlider.Value.If(double.NaN, 0));
			var time = (ts.Hours > 0 ? $"{ts.Hours:00}:" : "") + $"{ts.Minutes:00}:{ts.Seconds:00}";

			return (Episode != null
				? $"{Episode.Show} {Episode} at {time}.png"
				: $"{Movie} at {time}.png").EscapeFileName();
		}

		private void HideControls()
		{
			this.TryInvoke(() =>
			{
				if (CurrentScreenState != ScreenState.Normal
					//&& !Paused
					&& !firstLoad
					&& !new Rectangle(P_BotSpacer.PointToScreen(new Point(10, 0)), new Size(P_BotSpacer.Width - 20, P_BotSpacer.Height - 10)).Contains(Cursor.Position)
					&& (!PB_UpNext.Visible || !new Rectangle(PB_UpNext.PointToScreen(Point.Empty), PB_UpNext.Size).Contains(Cursor.Position)))
				{
					if (PnP)
					{
						new AnimationHandler(PB_Close, new Point(P_VLC.Width, -PB_Close.Height)) { Speed = 0.5 }.StartAnimation(PB_Close.Hide);
					}

					this.TryInvoke(() => P_BotSpacer.Height--);

					LastMousePoint = Cursor.Position;

					controlsHideAnimation = new AnimationHandler(P_BotSpacer, new Size(P_BotSpacer.Width, PnP || MiniPlayer ? 1 : 0), AnimationOption.IgnoreWidth);
					controlsHideAnimation.StartAnimation(P_Progress.Invalidate);

					topNotchIdentifier.Wait(() =>
					{
						//if (!Paused)
						{
							topNotchAnimation = new AnimationHandler(L_TopNotch, L_TopNotch.MinimumSize) { Speed = 1.25 };
							topNotchAnimation.StartAnimation();
						}
					}, 4000);

					if (PB_UpNext.Visible)
						showUpNextPopup();

					if (C_Rate.Visible)
						showRatePopup();

					if (P_SubDelay.Visible)
						showSubDelay();
				}
			});
		}

		private void Pause()
		{
			if (buffering)
			{
				pauseAfterBuffer = true;
			}
			else if (vlcControl.IsPlaying)
			{
				Task.Run(vlcControl.Pause);

				this.TryInvoke(() => SL_Play.Image = ProjectImages.Tiny_PlayNoBorder);
				SaveWatchtime();
				//ShowPlayControls();
			}
		}

		private void Play()
		{
			if (buffering)
			{
				pauseAfterBuffer = false;
			}
			else if (!vlcControl.IsPlaying)
			{
				if (slickScroll.Percentage != 0)
				{
					pausedFromScroll = true;
					slickScroll.ScrollTo(0, 7);
				}
				else if (vlcControl.State == MediaStates.Ended)
				{
					Replay(0);
				}
				else
				{
					Task.Run(vlcControl.Play);

					pausedFromScroll = false;
					pausedFromFocus = false;

					this.TryInvoke(() => SL_Play.Image = ProjectImages.Tiny_Pause);
				}
			}
		}

		private void PlayPause()
		{
			if (buffering ? !pauseAfterBuffer : vlcControl.IsPlaying)
			{
				Pause();
			}
			else
			{
				Play();
			}
		}

		private void Replay(long time)
		{
			this.TryInvoke(PB_Thumbnail.Hide);
			VlcControl_Buffering(null, null);
			Task.Run(() =>
			{
				firstLoad = true;
				if (Streaming)
					SetVideoStream(currentStream);
				else
					SetFile(VidFile);

				vlcControl.Play();
				vlcControl.Time = time;
				HideControls();
			});
		}

		private void ShowPlayControls()
		{
			if (CurrentScreenState == ScreenState.Normal || miniPlayerMoving)
			{
				return;
			}

			if (!PnP && !MiniPlayer)
				this.TryInvoke(P_Progress.Show);

			if (PnP)
			{
				this.TryInvoke(() =>
				{
					if (!PB_Close.Visible)
					{
						PB_Close.Location = new Point(P_VLC.Width, -PB_Close.Height);
						PB_Close.Show();
						PB_Close.BringToFront();
					}

					new AnimationHandler(PB_Close, new Point(P_VLC.Width - PB_Close.Height, 0)).StartAnimation();
				});
			}

			if (P_BotSpacer.Height != CONTROLS_SIZE)
			{
				controlsHideAnimation = new AnimationHandler(P_BotSpacer, new Size(P_BotSpacer.Width, CONTROLS_SIZE), AnimationOption.IgnoreWidth);
				controlsHideAnimation.StartAnimation();
			}

			if (FullScreen && L_TopNotch.Width != L_TopNotch.MaximumSize.Width)
			{
				topNotchIdentifier.Cancel();
				topNotchAnimation = new AnimationHandler(L_TopNotch, L_TopNotch.MaximumSize) { Speed = 1.25 };
				topNotchAnimation.StartAnimation();
			}

			LastMousePoint = Cursor.Position;

			//if (!Paused)
			{
				controlsHideIdentifier.Wait(HideControls, 1500);
			}
			//else
			//{
			//	controlsHideIdentifier.Cancel();
			//}

			if (PB_UpNext.Visible)
				showUpNextPopup();

			if (C_Rate.Visible)
				showRatePopup();

			if (P_SubDelay.Visible)
				showSubDelay();
		}

		private void updateTimeLabels()
		{
			if (vlcControl.Length > 0)
			{
				var ts = TimeSpan.FromMilliseconds(vlcControl.Length - (hoveredTime ? 0 : (long)SS_TimeSlider.Value));
				L_Time.TryInvoke(() => L_Time.Text = $"{(hoveredTime ? string.Empty : "-")}{(ts.Hours > 0 ? $"{ts.Hours:00}:" : string.Empty)}{$"{ts.Minutes:00}:{ts.Seconds:00}"}");

				ts = TimeSpan.FromMilliseconds(SS_TimeSlider.Value.If(double.NaN, 0));
				L_CurrentTime.TryInvoke(() => L_CurrentTime.Text = $"{(ts.Hours > 0 ? $"{ts.Hours:00}:" : "")}{$"{ts.Minutes:00}:{ts.Seconds:00}"}");

				P_Progress.TryInvoke(P_Progress.Invalidate);
			}
		}

		private void vlcControl_Playing(object sender, Vlc.DotNet.Core.VlcMediaPlayerPlayingEventArgs e)
		{
			if (firstLoad)
			{
				firstLoad = false;

				SS_TimeSlider.MaxValue = vlcControl.Length;
				this.TryInvoke(FileLoaded);
			}
		}

		private void vlcControl_TimeChanged(object sender, Vlc.DotNet.Core.VlcMediaPlayerTimeChangedEventArgs e)
		{
			if (timeLoop && Form.Visible)
			{
				timeLoop = false;

				if (vlcControl.Audio.Volume != (int)SS_Volume.Value)
				{
					Task.Run(() => vlcControl.Audio.Volume = (int)SS_Volume.Value);
				}

				SS_TimeSlider.Value = vlcControl.Time;

				if (!Streaming)
				{
					if (Episode != null)
					{
						Episode.Progress = 100D * vlcControl.Time / vlcControl.Length;
					}
					else if (Movie != null)
					{
						Movie.Progress = 100D * vlcControl.Time / vlcControl.Length;
					}
				}

				timeLoop = true;
			}

			if (buffering && !firstLoad)
			{
				buffering = false;

				if (isMuted)
				{
					Task.Run(() => vlcControl.Audio.IsMute = false);
				}

				this.TryInvoke(() =>
				{
					vlcControl.Parent = P_VLC;
					PB_Loader.Visible = false;
				});

				if (pauseAfterBuffer)
				{
					Pause();
				}

				pauseAfterBuffer = false;
			}

			if (vlcControl.Length > 0 && vlcControl.Time > 10000 && vlcControl.Time > vlcControl.Length - (Movie != null ? 360000 : 90000))
			{
				if (!C_Rate.Visible)
					showRatePopup();

				if (Episode?.Next != null && !PB_UpNext.Visible && SL_Next.Enabled)
					showUpNextPopup();
			}
		}

		#endregion Player Management

		public override bool CanExit(bool toBeDisposed)
		{
			SaveWatchtime();

			if (togglingPnP || !toBeDisposed || (Data.Options.KeepPlayerOpen && !Streaming && !vlcControl.State.AnyOf(MediaStates.Stopped, MediaStates.Ended) && (SS_TimeSlider.Value < SS_TimeSlider.MaxValue * 90 / 100)))
			{
				cancelDispose = toBeDisposed;

				ToggleScreen(ScreenState.PictureInPicture);
			}
			else
			{
				ToggleScreen(ScreenState.Normal);
				Pause();
			}

			if (FormDesign.Design.Temporary)
			{
				FormDesign.Switch(currentDesign, false, true);
			}

			return true;
		}

		public void IsForegroundFullScreen()
		{
			AnyWindowFullScreen = false;
			var screen = Screen.PrimaryScreen;
			var processlist = System.Diagnostics.Process.GetProcesses();

			foreach (var item in processlist)
			{
				try
				{
					if (item.MainWindowHandle != IntPtr.Zero)
					{
						GetWindowRect(item.MainWindowHandle, out var rect);
						if (new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top).Contains(screen.Bounds))
						{
							if (GetWindowLong(item.MainWindowHandle, GWL_STYLE) != WS_VISIBLE)
							{ AnyWindowFullScreen = true; return; }
						}
					}
				}
				catch { }
				finally
				{
					if (AnyWindowFullScreen && !Form.TopMost && !(miniPlayerAnimation?.Animating ?? false))
					{
						Form.TryInvoke(() => Form.TopMost = true);
					}
				}
			}
		}

		[DllImport("user32.dll", SetLastError = true)]
		private static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

		[DllImport("user32.dll")]
		private static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);

		private void hideSubDelay()
		{
			subDelayAnimation = new AnimationHandler(P_SubDelay, new Point(-P_SubDelay.Width, P_VLC.Height - P_SubDelay.Height - 15 - (CurrentScreenState == ScreenState.Normal || (!ControlsOpening && P_BotSpacer.Height != CONTROLS_SIZE) ? 0 : CONTROLS_SIZE)));
			subDelayAnimation.StartAnimation(P_SubDelay.Hide);
		}

		private void Hook_MouseWheel(MouseHook.MSLLHOOKSTRUCT mouseStruct)
		{
			this.TryInvoke(() =>
			{
				if (!MiniPlayer && !PnP
					&& (Form.CurrentFormState == FormState.NormalFocused || Form.CurrentFormState == FormState.ForcedFocused)
					&& new Rectangle(vlcControl.PointToScreen(Point.Empty), vlcControl.Size).Contains(Cursor.Position)
					&& !new Rectangle(P_BotSpacer.PointToScreen(Point.Empty), P_BotSpacer.Size).Contains(Cursor.Position))
				{
					var delta = mouseStruct.mouseData > 4000000000 ? -120 : 120;
					if ((delta > 0 || (DateTime.Now - lastMouseVolumeUp).TotalSeconds < 4 || MiniPlayer || PnP || !TLP_MoreInfo.Visible) && slickScroll.Percentage == 0)
					{
						SS_Volume.Value += delta > 0 ? 5 : -5;
						lastMouseVolumeUp = DateTime.Now;
					}
					else
					{
						slickScroll.TriggerMouseWheel(new MouseEventArgs(MouseButtons.None, 0, 0, 0, delta));
					}
				}
			});
		}

		private void Md_MouseMove(object sender, Point p)
		{
			if (PnP && P_BackContent.Parent is PictureInPictureControl pnp && pnp.IsMoving)
			{
				pnp.MouseMoved();
			}
			else if (LastMousePoint != p)
			{
				if (new Rectangle(vlcControl.PointToScreen(Point.Empty), vlcControl.Size).Contains(p))
				{
					if (MiniPlayer || PnP)
					{
						if (!lazyPlayerWaitIdentifier.Waiting)
						{
							lazyPlayerWaitIdentifier.Wait(ShowPlayControls, 750);
						}

						if (controlsHideIdentifier.Waiting)
						{
							controlsHideIdentifier.Wait(HideControls, 1000);
						}
					}
					else if (Form.FormIsActive)
					{
						ShowPlayControls();
					}

					if (PB_UpNext.Visible && (upnextHidden || Paused))
					{
						upnextHidden = false;
						showUpNextPopup();
					}
				}
				else if (MiniPlayer || PnP)
				{
					lazyPlayerWaitIdentifier.Cancel();
					if (P_BotSpacer.Height > 1)
					{
						HideControls();
					}
				}
			}

			LastMousePoint = p;
		}

		private void MouseHook_MouseUp(MouseHook.MSLLHOOKSTRUCT mouseStruct)
		{
			if (PnP && P_BackContent.Parent is PictureInPictureControl pnp && pnp.IsMoving)
			{
				pnp.ReleaseMove();
			}
		}

		private void setSimilarCharacters(IEnumerable<dynamic> cast)
		{
			TLP_SimilarContent.Controls.Clear();
			TLP_SimilarContent.RowStyles.Clear();

			var showsDic = new Dictionary<TvShow, List<dynamic>>();
			var moviesDic = new Dictionary<Movie, List<dynamic>>();

			var ind = 0;

			foreach (var c in cast)
			{
				foreach (var item in ShowManager.Shows)
				{
					if (item != Episode?.Show)
					{
						var ct = item.Seasons.FirstOrDefault(y => y.Credits != null && y.Credits.Cast.Any(z => z.Id == c.Id))?.Credits.Cast.FirstOrDefault(z => z.Id == c.Id)
							?? item.Episodes.FirstOrDefault(y => y.GuestStars.Any(z => z.Id == c.Id))?.GuestStars.FirstOrDefault(z => z.Id == c.Id);

						if (ct != null)
						{
							if (showsDic.ContainsKey(item))
							{
								showsDic[item].Add(ct);
							}
							else
							{
								showsDic.Add(item, new List<dynamic>() { ct });
							}
						}
					}
				}

				foreach (var item in MovieManager.Movies)
				{
					if (item != Movie)
					{
						var ct = item.Cast.FirstOrDefault(z => z.Id == c.Id);

						if (ct != null)
						{
							if (moviesDic.ContainsKey(item))
							{
								moviesDic[item].Add(ct);
							}
							else
							{
								moviesDic.Add(item, new List<dynamic>() { ct });
							}
						}
					}
				}
			}

			if (showsDic.Count + moviesDic.Count > 0)
			{
				foreach (var item in showsDic)
				{
					TLP_SimilarContent.RowStyles.Add(new RowStyle());

					var flp = new FlowLayoutPanel()
					{
						Dock = DockStyle.Top,
						AutoSize = true,
						AutoSizeMode = AutoSizeMode.GrowOnly
					};

					foreach (var c in item.Value)
					{
						flp.Controls.Add(new CharacterControl(c));
					}

					TLP_SimilarContent.Controls.Add(new ContentControl<TvShow>(item.Key), 0, ind);
					TLP_SimilarContent.Controls.Add(flp, 1, ind);

					ind++;
				}

				foreach (var item in moviesDic)
				{
					TLP_SimilarContent.RowStyles.Add(new RowStyle());

					var flp = new FlowLayoutPanel()
					{
						Dock = DockStyle.Top,
						AutoSize = true,
						AutoSizeMode = AutoSizeMode.GrowOnly
					};

					foreach (var c in item.Value)
					{
						flp.Controls.Add(new CharacterControl(c));
					}

					TLP_SimilarContent.Controls.Add(new ContentControl<Movie>(item.Key), 0, ind);
					TLP_SimilarContent.Controls.Add(flp, 1, ind);

					ind++;
				}
			}

			TLP_SimilarContent.Visible = SP_Similar.Visible = TLP_SimilarContent.Controls.Count > 0;
		}

		private void showSubDelay()
		{
			if (!P_SubDelay.Visible)
			{
				P_SubDelay.TryInvoke(() =>
				{
					P_SubDelay.Location = new Point(-P_SubDelay.Width, P_VLC.Height - P_SubDelay.Height - 15 - (CurrentScreenState == ScreenState.Normal || (!ControlsOpening && P_BotSpacer.Height != CONTROLS_SIZE) ? 0 : CONTROLS_SIZE));
					P_SubDelay.Visible = true;
				});
			}

			var newPoint = new Point(
				15,
				P_VLC.Height - P_SubDelay.Height - 15 - (CurrentScreenState == ScreenState.Normal || (!ControlsOpening && P_BotSpacer.Height != CONTROLS_SIZE) ? 0 : CONTROLS_SIZE));

			if (P_SubDelay.Location != newPoint)
			{
				subDelayAnimation = new AnimationHandler(P_SubDelay, newPoint);
				subDelayAnimation.StartAnimation();
			}

			subDelayWaitIdentifier.Wait(hideSubDelay, 5000);
		}

		private void showRatePopup()
		{
			if ((Episode != null || Movie != null) && !Streaming)
			{
				if (!C_Rate.Visible)
				{
					C_Rate.TryInvoke(() =>
					{
						C_Rate.Location = new Point(-C_Rate.Width, (P_VLC.Height - C_Rate.Height) / 2);
						C_Rate.Visible = true;
					});
				}

				ratingAnimation = new AnimationHandler(C_Rate, new Point(0, (P_VLC.Height - C_Rate.Height) / 2));
				ratingAnimation.StartAnimation();
			}
		}

		private void showUpNextPopup()
		{
			if (Episode?.Next?.Playable ?? false)
			{
				if (!PB_UpNext.Visible)
				{
					PB_UpNext.TryInvoke(() =>
					{
						PB_UpNext.Location = new Point(P_VLC.Width, P_VLC.Height - PB_UpNext.Height - 15 - (CurrentScreenState == ScreenState.Normal || (!ControlsOpening && P_BotSpacer.Height != CONTROLS_SIZE) ? 0 : CONTROLS_SIZE));
						PB_UpNext.Visible = true;
					});
				}
				else if (!Form.FormIsActive)
					return;

				var newPoint = new Point(
					P_VLC.Width - PB_UpNext.Width - 15,
					P_VLC.Height - PB_UpNext.Height - 15 - (CurrentScreenState == ScreenState.Normal || (!ControlsOpening && P_BotSpacer.Height != CONTROLS_SIZE) ? 0 : CONTROLS_SIZE));

				if (!firstUpNextShown || (!upnextHidden && PB_UpNext.Location != newPoint))
				{
					upNextAnimation = new AnimationHandler(PB_UpNext, newPoint);
					upNextAnimation.StartAnimation();
				}

				upnextHideIdentifier.Wait(() =>
				{
					upnextHidden = true;
					this.TryInvoke(() =>
					{
						if (!new Rectangle(PB_UpNext.PointToScreen(Point.Empty), PB_UpNext.Size).Contains(Cursor.Position))
						{
							newPoint.X = P_VLC.Width - UI.Font(10.5F, FontStyle.Bold).Height - 3;
							upNextAnimation = new AnimationHandler(PB_UpNext, newPoint);
							upNextAnimation.StartAnimation();
						}
					});
				}, upnextHidden ? 0 : firstUpNextShown ? 1600 : 6000);
				firstUpNextShown = true;
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}

		#region Form Design

		public override bool KeyPressed(ref Message msg, Keys keyData)
		{
			if (PnP && Form.GetCurrentlyFocusedControl() is TextBoxBase)
			{
				return false;
			}

			switch (keyData)
			{
				case Keys.Space:
				case Keys.MediaPlayPause:
					PlayPause();
					return true;

				case Keys.Apps:
					SI_More_Click(null, new MouseEventArgs(MouseButtons.None, 1, SL_More.Width, 0, 0));
					return true;

				case Keys.Left:
					if (P_SubDelay.Visible)
					{
						SL_SubDelayMinus_Click(null, null);
					}
					else
					{
						Task.Run(() => vlcControl.Time -= (Data.Options.BackwardTime * 1000) + (Data.Options.ForwardTime % Data.Options.BackwardTime).If(0, 1000 * Data.Options.BackwardTime / Data.Options.ForwardTime, 0));
					}

					return true;

				case Keys.Right:
					if (P_SubDelay.Visible)
					{
						SL_SubDelayPlus_Click(null, null);
					}
					else
					{
						Task.Run(() => vlcControl.Time += Data.Options.ForwardTime * 1000);
					}

					return true;

				case Keys.Shift | Keys.Left:
					Task.Run(() => vlcControl.Time -= Data.Options.BackwardTime * 3000);
					return true;

				case Keys.Shift | Keys.Right:
					Task.Run(() => vlcControl.Time += Data.Options.ForwardTime * 3000);
					return true;

				case Keys.MediaPreviousTrack:
				case Keys.Control | Keys.Left:
					SL_Previous_Click(null, null);
					return true;

				case Keys.MediaNextTrack:
				case Keys.Control | Keys.Right:
					SL_Next_Click(null, null);
					return true;

				case Keys.Up:
				case Keys.Add:
					SS_Volume.Value += 5;
					return true;

				case Keys.Down:
				case Keys.Subtract:
					SS_Volume.Value -= 5;
					return true;

				case Keys.Control | Keys.Up:
				case Keys.Shift | Keys.Up:
					if (slickScroll.Percentage != 0)
					{
						slickScroll.ScrollTo(P_VLC, 7);
						return true;
					}
					break;

				case Keys.Control | Keys.Down:
				case Keys.Shift | Keys.Down:
					if (slickScroll.Percentage == 0)
					{
						slickScroll.ScrollTo(TLP_Suggestions, 7);
					}
					else
					{
						slickScroll.ScrollTo(100, 7);
					}

					return true;

				case Keys.Control | Keys.S:
				case Keys.PrintScreen:
					Task.Run(() => vlcControl.TakeSnapshot(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), getSnapShotFile())));
					return true;

				case Keys.D1:
				case Keys.NumPad1:
					Task.Run(() => vlcControl.Rate = 1);
					return true;

				case Keys.D2:
				case Keys.NumPad2:
					Task.Run(() => vlcControl.Rate = 1.125F);
					return true;

				case Keys.D3:
				case Keys.NumPad3:
					Task.Run(() => vlcControl.Rate = 1.35F);
					return true;

				case Keys.A:
					if (SL_Audio.Enabled)
					{
						SL_Audio_Click(null, null);
					}

					return true;

				case Keys.S:
					if (SL_Subs.Enabled)
					{
						SL_Subs_Click(null, null);
					}

					return true;

				case Keys.M:
					Task.Run(() =>
					{
						isMuted = vlcControl.Audio.IsMute = !vlcControl.Audio.IsMute;
						this.TryInvoke(() => SL_Audio.Image = isMuted ? ProjectImages.Tiny_Mute : ProjectImages.Tiny_Sound);
					});
					return true;

				case Keys.Alt | Keys.Enter:
				case Keys.F:
					ToggleScreen(FullScreen ? lastScreenState : ScreenState.FullScreen);
					return true;

				case Keys.Control | Keys.Tab:
					if (PnP)
					{
						ToggleScreen(ScreenState.Normal);
					}
					else
					{
						ToggleScreen(MiniPlayer ? lastScreenState : ScreenState.MiniPlayer);
					}

					return true;

				case Keys.Control | Keys.P:
					SL_PnP_Click(null, null);
					return true;

				case Keys.Escape:
					if (CurrentScreenState != ScreenState.Normal && (Form.CurrentPanel == this || !Form.PanelHistory.Any()))
					{
						if (PnP)
						{
							PB_Close_Click(null, null);
						}
						else
						{
							ToggleScreen(ScreenState.Normal);
						}

						return true;
					}
					break;
			}

			return false;
		}

		public override bool OnWndProc(Message m)
		{
			try
			{
				if (m.Msg == 0x210 && m.WParam == (IntPtr)0x1020b && CurrentScreenState != ScreenState.Normal && (Form.CurrentPanel == this || !Form.PanelHistory.Any()))
				{
					if (PnP)
						PB_Close_Click(null, null);
					else
						ToggleScreen(ScreenState.Normal);

					return true;
				}

				var frm = (SlickForm)(PnP ? P_BackContent.FindForm() : Form);
				if ((Form.CurrentPanel == this || PnP) && m.Msg == 0x21 && new Rectangle(P_VLC.PointToScreen(Point.Empty), P_VLC.Size).Contains(Cursor.Position)
					&& !new Control[] { P_BotSpacer, C_Rate, PB_UpNext, P_SubDelay, UpNextCountdown }
					.Any(x => x.Visible && new Rectangle(x.PointToScreen(Point.Empty), x.Size).Contains(Cursor.Position)))
				{
					if (m.LParam == (IntPtr)0x2010001)
					{
						if (!MiniPlayer && frm.CurrentFormState == FormState.ForcedFocused)
						{
							frm.CurrentFormState = FormState.NormalFocused;
						}
						else if ((DateTime.Now - lastClick).TotalMilliseconds < 200)
						{
							pauseWaitIdentifier.Cancel();
							ToggleScreen(CurrentScreenState != ScreenState.Normal ? ScreenState.Normal : ScreenState.FullScreen);

							if (FullScreen)
							{
								Play();
							}
						}
						else
						{
							if (PnP && P_BackContent.Parent is PictureInPictureControl pnp)
							{
								pnp.StartMouseMove();
							}
							else if (MiniPlayer)
							{
								miniPlayerMoving = true;
								startMiniPlayerMoveCursor = Cursor.Position;
								new BackgroundAction(() => frm.TryInvoke(() => frm.ForceWindowMove())).RunIn(50);
							}
							else if (frm.CurrentFormState != FormState.ForcedFocused)
							{
								if (!pausedFromFocus)
								{
									pauseWaitIdentifier.Wait(() => this.TryInvoke(PlayPause), 200);
								}
							}
							else
							{
								frm.CurrentFormState = FormState.NormalFocused;
							}
						}

						lastClick = DateTime.Now;

						return true;
					}
					else if (m.LParam == (IntPtr)0x2040001 && frm.CurrentFormState != FormState.NormalUnfocused)
					{
						if (frm.CurrentFormState != FormState.ForcedFocused)
						{
							new BackgroundAction(() => frm.TryInvoke(() => SI_More_Click(null, null))).RunIn(50);
						}
						else
						{
							frm.CurrentFormState = FormState.NormalFocused;
						}

						return true;
					}
				}
				else if (!Data.Options.KeepPlayerOpen && m.Msg == 0x210 && m.WParam == (IntPtr)0x1020b && Form.CurrentPanel == this && Form.PanelHistory.Any())
				{
					this.TryInvoke(() => Form.PushBack());
					return true;
				}
				else if (MiniPlayer && Form.CurrentPanel == this && m.Msg == 0x214)
				{   // WM_SIZING
					var ratio = (double)videoInformation.Width / videoInformation.Height;
					var rc = (RECT)Marshal.PtrToStructure(m.LParam, typeof(RECT));
					var vert = frm.Padding.Vertical + PnP.If(2, 0);
					var horz = frm.Padding.Horizontal + PnP.If(2, 0);
					var w = rc.Right - rc.Left - horz; // get width
					var h = rc.Bottom - rc.Top - vert; // get height
					switch ((int)m.WParam)
					{
						case 1: //left
							rc.Bottom = rc.Top + (int)(w / ratio) + vert;
							break;

						case 2: //right
							rc.Bottom = rc.Top + (int)(w / ratio) + vert;
							break;

						case 3: //top
							rc.Right = rc.Left + (int)(h * ratio) + horz;
							break;

						case 4:
							if (w / ratio < h)
							{
								rc.Left = rc.Right - (int)(h * ratio) - horz;
							}
							else
							{
								rc.Top = rc.Bottom - (int)(w / ratio) - vert;
							}

							break;

						case 5:
							if (w / ratio < h)
							{
								rc.Right = rc.Left + (int)(h * ratio) + horz;
							}
							else
							{
								rc.Top = rc.Bottom - (int)(w / ratio) - vert;
							}

							break;

						case 6:
							rc.Right = rc.Left + (int)(h * ratio) + horz;
							break;

						case 7:
							if (w / ratio < h)
							{
								rc.Left = rc.Right - (int)(h * ratio) - horz;
							}
							else
							{
								rc.Bottom = rc.Top + (int)(w / ratio) + vert;
							}

							break;

						case 8:
							if (w / ratio < h)
							{
								rc.Right = rc.Left + (int)(h * ratio) + horz;
							}
							else
							{
								rc.Bottom = rc.Top + (int)(w / ratio) + vert;
							}

							break;

						default:
							break;
					}
					Marshal.StructureToPtr(rc, m.LParam, false);
					m.Result = (IntPtr)1;
					return true;
				}
			}
			catch { }

			return false;
		}

		protected override void DesignChanged(FormDesign design)
		{
			base.DesignChanged(design);

			P_Progress.Invalidate();
			P_BotSpacer.ForeColor = design.ForeColor;
			L_ShowPlotLabel.ForeColor = L_PlotLabel.ForeColor = design.LabelColor;
			P_BackContent.BackColor = TLP_MoreInfo.BackColor = TLP_MoreInfo2.BackColor
				= P_BotSpacer.BackColor = PB_Close.BackColor
				= SL_SubDelayPlus.BackColor = SL_SubDelayMinus.BackColor = L_SubDelay.BackColor = design.BackColor;
			P_SubDelay.BackColor = P_Spacer.BackColor = P_Spacer2.BackColor = design.AccentColor;
			TLP_Suggestions.BackColor = P_Info.BackColor = slickScroll.BackColor = design.AccentBackColor;
			I_MoreInfo.Color(design.ForeColor);
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);

			Form.VisibleChanged += Form_VisibleChanged;
			Form.WindowStateChanged += Form_WindowStateChanged;
			Form.WindowStateChanging += Form_WindowStateChanging;
			Form.LocationChanged += Form_LocationChanged;
			Form.ResizeEnd += Form_ResizeEnd;
			Form.FormStateChanged += Form_FormStateChanged;

			Disposed += (s, _) =>
			{
				Form.VisibleChanged -= Form_VisibleChanged;
				Form.WindowStateChanged -= Form_WindowStateChanged;
				Form.WindowStateChanging -= Form_WindowStateChanging;
				Form.LocationChanged -= Form_LocationChanged;
				Form.ResizeEnd -= Form_ResizeEnd;
				Form.FormStateChanged -= Form_FormStateChanged;
			};

			Play();
		}

		protected override void UIChanged()
		{
			base.UIChanged();

			L_PlotLabel.Font = L_ShowPlotLabel.Font = UI.Font(9F, FontStyle.Bold);
			L_SubDelay.Font = UI.Font(9.75F, FontStyle.Bold);

			PB_Close.Size = UI.Scale(new Size(20, 20), UI.UIScale);
			PB_Close.Location = new Point(P_VLC.Width - PB_Close.Height, 0);
			PB_Thumb.Size = UI.Scale(new Size(160, 90), UI.UIScale);
			L_MoreInfo.Font = L_MoreInfo2.Font = L_Time.Font = L_CurrentTime.Font = new Font(UI.FontFamily, Math.Min(11.25F, (float)(8.25F * UI.UIScale)));
			TLP_MoreInfo.Height = TLP_MoreInfo2.Height = (int)(22 * UI.UIScale);
			P_BotSpacer.Height = CONTROLS_SIZE;
			PB_UpNext.Size = UI.Scale(new Size(192, 108), UI.UIScale);
			P_SubDelay.Size = UI.Scale(new Size(130, 30), UI.UIScale);
			SS_Volume.Width = (int)(100 * UI.UIScale);
			C_Rate.Size = UI.Scale(new Size(40, 80), UI.UIScale);

			var notchOpen = L_TopNotch.Width == L_TopNotch.MaximumSize.Width && L_TopNotch.MaximumSize.Width > 0;

			using (var g = CreateGraphics())
			{
				TLP_Controls.ColumnStyles[0].Width = TLP_Controls.ColumnStyles[2].Width =
					(int)g.Measure("-99:99:99", L_Time.Font).Width + 16;

				L_TopNotch.MaximumSize = new Size((int)g.Measure(Text.ToUpper(), UI.Font(12.75F, FontStyle.Bold)).Width + 8, 8 + (UI.Font(12.75F, FontStyle.Bold).Height * 12 / 10));
			}

			L_TopNotch.MinimumSize = new Size(0, L_TopNotch.MaximumSize.Height);

			if (notchOpen)
				L_TopNotch.Size = L_TopNotch.MaximumSize;
			else
				L_TopNotch.Height = L_TopNotch.MaximumSize.Height;
		}

		#endregion Form Design

		#region Form Events

		internal void Form_ResizeEnd(object sender, EventArgs e)
		{
			if (MiniPlayer)
			{
				var h = (sender == null ? lastMiniBounds?.Height ?? (int)(280 * UI.UIScale) : Form.Bounds.Height) - Form.Padding.Vertical;
				var newFormSize = new Size((h * videoInformation.Width / videoInformation.Height) + Form.Padding.Horizontal, h + Form.Padding.Vertical);
				Form.Bounds = new Rectangle(Form.Bounds.Center(newFormSize), newFormSize);
				P_VLC.Height = h;

				if (Data.Options.StickyMiniPlayer || sender == null)
				{
					var taskbar = SlickForm.CurrentTaskbarLocation;
					var screen = Screen.FromControl(this).Bounds;
					var cursor = miniPlayerMoving ? Cursor.Position : (sender == null ? lastMiniBounds ?? new Rectangle(0, screen.Height, 1, 1) : Form.Bounds).Center(Size.Empty);
					var deltaCursor = miniPlayerMoving ? new Point(cursor.X - startMiniPlayerMoveCursor.X, cursor.Y - startMiniPlayerMoveCursor.Y) : Point.Empty;
					var point = (Data.Options.StickyMiniPlayer ? null : lastMiniBounds?.Location) ?? new Point(
						cursor.X < screen.Width / 3
						? 22 + taskbar.If(SlickForm.TaskbarLocation.Left, 65, 0) /* Left */ : (cursor.X < screen.Width * 2 / 3
						? (deltaCursor.X < 0 ? 22 + taskbar.If(SlickForm.TaskbarLocation.Left, 65, 0) : (screen.Width - Form.Width - 22 - taskbar.If(SlickForm.TaskbarLocation.Right, 65, 0))) /* Middle */
						: screen.Width - Form.Width - 22 - taskbar.If(SlickForm.TaskbarLocation.Right, 65, 0)/* Right */),
						cursor.Y < screen.Height / 3
						? 22 + taskbar.If(SlickForm.TaskbarLocation.Top, 65, 0)/* Top */ : (cursor.Y < screen.Height * 2 / 3
						? (cursor.X.IsWithin(screen.Width / 3, screen.Width * 2 / 3) ? (deltaCursor.Y < 0 ? 22 + taskbar.If(SlickForm.TaskbarLocation.Top, 65, 0) : (screen.Height - Form.Height - 22 - taskbar.If(SlickForm.TaskbarLocation.Bottom, 65, 0))) : ((screen.Height - Form.Height) / 2)) /* Middle */
						: screen.Height - Form.Height - 22 - taskbar.If(SlickForm.TaskbarLocation.Bottom, 65, 0) /* Bottom */));

					miniPlayerAnimation = new AnimationHandler(Form, point, 2);
					miniPlayerAnimation.StartAnimation(() =>
					{
						if (Data.Options.TopMostPlayer || AnyWindowFullScreen)
						{ Form.TopMost = false; Form.TopMost = true; }
					});
				}
			}

			miniPlayerMoving = false;
		}

		internal void PC_Player_Resize(object sender, EventArgs e)
		{
			try
			{
				switch (CurrentScreenState)
				{
					case ScreenState.Normal:
						P_VLC.Height = Height - P_BotSpacer.Height - Padding.Top;
						break;

					case ScreenState.FullScreen:
						P_VLC.Height = Height;
						break;

					case ScreenState.MiniPlayer:
					case ScreenState.PictureInPicture:
						P_VLC.Height = P_BackContent.Height;
						break;

					default:
						break;
				}

				SS_Volume.Visible = SL_More.Visible = P_BackContent.Width > 325;
				SL_Subs.Visible = SL_FullScreen.Visible = P_BackContent.Width > 455;

				if (!stateChanging && Form != null)
				{
					if (Form.WindowState == FormWindowState.Normal && CurrentScreenState == ScreenState.Normal)
					{
						lastBounds = Form.Bounds;
					}
					else if (Form.WindowState == FormWindowState.Normal && CurrentScreenState == ScreenState.MiniPlayer)
					{
						lastMiniBounds = Form.Bounds;
					}
				}

				if ((Width - (int)(2 * 350 * UI.FontScale) < 300) != (TLP_Suggestions.GetRow(TLP_Info) == 2))
				{
					TLP_Suggestions.SetCellPosition(TLP_Info, (Width - (int)(2 * 350 * UI.FontScale) < 300)
						? new TableLayoutPanelCellPosition(1, 2)
						: new TableLayoutPanelCellPosition(2, 1));
				}
			}
			catch { }
		}

		private void Form_FormStateChanged(object sender, EventArgs e)
		{
			if (Data.Options.PauseWhenOutOfFocusFullScreen)
			{
				if (FullScreen && Form.CurrentFormState == FormState.NormalUnfocused && !pausedFromFocus && !Paused)
				{
					Pause();
					pausedFromFocus = true;
				}
				else if (FullScreen && Form.CurrentFormState != FormState.NormalUnfocused && !pausedFromScroll && pausedFromFocus && Paused)
				{
					Play();
					pausedFromFocus = false;
				}
			}
		}

		private void Form_LocationChanged(object sender, EventArgs e)
		{
			if (!stateChanging)
			{
				if (Form.WindowState == FormWindowState.Normal && CurrentScreenState == ScreenState.Normal)
				{
					lastBounds = Form.Bounds;
				}
				else if (Form.WindowState == FormWindowState.Normal && CurrentScreenState == ScreenState.MiniPlayer)
				{
					lastMiniBounds = Form.Bounds;
				}
			}
		}

		private void Form_VisibleChanged(object sender, EventArgs e)
		{
			if (!Form.Visible && !Paused && CurrentScreenState != ScreenState.MiniPlayer)
			{
				Pause();
			}
		}

		private void Form_WindowStateChanged(object sender, EventArgs e)
		{
			if (Form.WindowState == FormWindowState.Minimized && !Paused)
			{
				Pause();
			}
		}

		private void Form_WindowStateChanging(object sender, StateChangingEventArgs e)
		{
			if (e.WindowState != FormWindowState.Maximized && FullScreen)
			{
				ToggleScreen(ScreenState.Normal);
				e.Cancel = true;
			}
			else if (e.WindowState == FormWindowState.Maximized && !FullScreen && !PnP)
			{
				ToggleScreen(ScreenState.FullScreen);
				e.Cancel = true;
			}
		}

		private void L_Time_MouseEnter(object sender, EventArgs e)
		{
			hoveredTime = true;
			updateTimeLabels();
		}

		private void L_Time_MouseLeave(object sender, EventArgs e)
		{
			hoveredTime = false;
			updateTimeLabels();
		}

		private void PB_Close_Click(object sender, EventArgs e)
		{
			P_BackContent.Parent?.Hide();
			Application.DoEvents();
			P_BackContent.Parent?.Dispose();
			Dispose();
		}

		private void PB_UpNext_Click(object sender, EventArgs e)
		{
			if (vlcControl.State != MediaStates.Ended)
			{
				SL_Next_Click(null, null);
			}
		}

		private void PC_Player_Load(object sender, EventArgs e)
		{
			if (Form.WindowState == FormWindowState.Normal)
			{
				lastBounds = Form.Bounds;
			}

			if (Data.Options.FullScreenPlayer || Form.WindowState == FormWindowState.Maximized)
			{
				ToggleScreen(ScreenState.FullScreen);
			}
			else
			{
				PC_Player_Resize(null, null);
			}

			slickScroll.Visible = true;
			slickScroll.Visible = false;

			mouseHook = new MouseHook();
			mouseHook.MouseWheel += Hook_MouseWheel;
			mouseHook.LeftButtonUp += MouseHook_MouseUp;
			mouseHook.Install();

			mouseDetector = new MouseDetector();
			mouseDetector.MouseMove += Md_MouseMove;

			vlcControl.Cursor = Cursors.Default;
		}

		private void PC_Player_Shown(object sender, EventArgs e)
		{
			if (!stateChanging && PnP)
				ToggleScreen(ScreenState.Normal);
			else if (!stateChanging && Form.WindowState == FormWindowState.Maximized)
				ToggleScreen(CurrentScreenState);
		}

		private void SI_More_Click(object sender, MouseEventArgs et)
		{
			var speeds = new Dictionary<string, float>
			{
				{ "Slow Motion", 0.65F },
				{ "Slower", 0.875F },
				{ "Normal", 1F },
				{ "Faster", 1.125F },
				{ "Fastest", 1.35F },
			};

			var items = new List<SlickStripItem>()
			{
				new SlickStripItem("Toggle Full-screen", () => SL_FullScreen_Click(null, null), SL_FullScreen.Image as Bitmap),

				new SlickStripItem("Toggle Mini-Player", () => SL_MiniPlayer_Click(null, null), SL_MiniPlayer.Image as Bitmap),

				new SlickStripItem("Toggle Picture-in-Picture", () => SL_PnP_Click(null, null), ProjectImages.Tiny_PictureInPicture),

				SlickStripItem.Empty,

				new SlickStripItem("Take Screen-shot",
				() => Task.Run(() => vlcControl.TakeSnapshot(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), getSnapShotFile()))),
				ProjectImages.Tiny_Screenshot)
			};

			if (Streaming)
			{
				items.AddRange(new[]
				{
					new SlickStripItem("View on YouTube", () =>
					{
						if (!Paused) { Pause(); } Cursor.Current = Cursors.WaitCursor;
						try { System.Diagnostics.Process.Start($"https://www.youtube.com/watch?v={StreamingVideo.Id}"); }
						catch { Cursor.Current = Cursors.Default; MessagePrompt.Show("Could not open the link because you do not have a default browser selected", "No Browser Selected", PromptButtons.OK, PromptIcons.Error); }
						Cursor.Current = Cursors.Default;
					}, image: ProjectImages.Tiny_Youtube),

					SlickStripItem.Empty,

					new SlickStripItem("Set Quality to", image: ProjectImages.Tiny_Resolution, fade: true),
				});

				foreach (var item in StreamInfo.Muxed.OrderByDescending(x => x.VideoQuality))
				{
					var strip = new SlickStripItem(item.VideoQualityLabel,
					() => Task.Run(() =>
					{
						var time = vlcControl.Time;
						var paused = Paused;
						if (!paused)
						{
							vlcControl.Pause();
						}

						SetVideoStream(item);
						if (!paused)
						{
							vlcControl.Play();
						}

						vlcControl.Time = time;
					}), fade: currentStream == item, tab: 1);

					switch (item.VideoQuality)
					{
						case VideoQuality.Low144:
						case VideoQuality.Low240:
						case VideoQuality.Medium360:
						case VideoQuality.Medium480:
							strip.Image = ProjectImages.Tiny_SD;
							break;

						case VideoQuality.High720:
							strip.Image = ProjectImages.Tiny_720;
							break;

						case VideoQuality.High1080:
							strip.Image = ProjectImages.Tiny_1080;
							break;

						default:
							strip.Image = ProjectImages.Tiny_4K;
							break;
					}

					if (strip.Fade)
					{
						strip.Text += " (selected)";
					}

					items.Add(strip);
				}
			}
			else
			{
				items.Add(new SlickStripItem("View File", () =>
				{
					Pause();

					if (FullScreen)
					{
						ToggleScreen(ScreenState.Normal);
					}

					System.Diagnostics.Process.Start("explorer.exe", $"/select, \"{VidFile.FullName}\"");
				}, image: ProjectImages.Tiny_Folder, show: VidFile != null && VidFile.Exists)
				);
			}

			items.Add(SlickStripItem.Empty);
			items.Add(new SlickStripItem("Set Playback Speed", image: ProjectImages.Tiny_Speedometer, fade: true));

			foreach (var item in speeds)
			{
				items.Add(new SlickStripItem(item.Key, () => Task.Run(() => vlcControl.Rate = item.Value), tab: 1, fade: vlcControl.Rate == item.Value, image: vlcControl.Rate == item.Value ? ProjectImages.Tiny_Ok : null));
			}

			items.Add(new SlickStripItem("", tab: 1));
			items.Add(new SlickStripItem("Set Aspect Ratio", image: ProjectImages.Tiny_AspectRatio, fade: true));

			foreach (var item in new[] { null, "16:9", "16:10", "4:3", "1:1" })
			{
				items.Add(new SlickStripItem(item ?? "Default", () => Task.Run(() =>
				{
					vlcControl.Video.AspectRatio = item ?? string.Empty;
					videoInformation = item == null
						? vlcControl.GetCurrentMedia().TracksInformations.FirstOrDefault(mediaInformation => mediaInformation.Type == Vlc.DotNet.Core.Interops.Signatures.MediaTrackTypes.Video).Video.As(x => new Size((int)x.Width, (int)x.Height))
						: new Size((int)(videoInformation.Height * item.Split(':')[0].SmartParseD() / item.Split(':')[1].SmartParseD(1)), videoInformation.Height);

					if (CurrentScreenState == ScreenState.MiniPlayer)
					{
						var h = lastMiniBounds?.Height ?? 280;

						Form.TryInvoke(() =>
						{
							Form.MinimumSize = new Size(150 * videoInformation.Width / videoInformation.Height - Form.Padding.Horizontal, 150);
							Form.Bounds = new Rectangle(
								lastMiniBounds?.Location ?? new Point(22, Screen.FromControl(this).Bounds.Height - 325),
								new Size(h * videoInformation.Width / videoInformation.Height + Form.Padding.Horizontal, h + Form.Padding.Vertical));
						});
					}

					this.TryInvoke(() => PB_Thumb.Size = UI.Scale(new Size(videoInformation.Width * 90 / videoInformation.Height, 90), UI.UIScale));
				}), tab: 1, fade: (vlcControl.Video.AspectRatio ?? string.Empty) == (item ?? string.Empty), image: (vlcControl.Video.AspectRatio ?? string.Empty) == (item ?? string.Empty) ? ProjectImages.Tiny_Ok : null));
			}

			items.AddRange(new SlickStripItem[]
			{
				SlickStripItem.Empty,

				new SlickStripItem("Episode Page", () =>
				{
					Data.Mainform.PushPanel(null, new PC_ShowPage(Episode));
				}, image: ProjectImages.Tiny_Info, show: Episode != null),

				new SlickStripItem("Season Page", () =>
				{
					if (!Data.Options.OpenAllPagesForEp) { Data.Mainform.PushPanel(null, new PC_ShowPage(Season ?? Episode.Season, Episode)); } else { Data.Mainform.PushPanel(null, new PC_SeasonView(Season ?? Episode.Season, Episode)); } }, image: ProjectImages.Tiny_Season, show: (Season ?? Episode?.Season) != null),

				new SlickStripItem("Show Page", () =>
				{
					Data.Mainform.PushPanel(null, new PC_ShowPage(TvShow ?? Episode.Show));
				}, image: ProjectImages.Tiny_TV, show: (TvShow ?? Episode?.Show) != null),

				new SlickStripItem("Movie Page", () =>
				{
					Form.PushPanel(null, new PC_MoviePage(Movie));
				}, image: ProjectImages.Tiny_Movie, show: Movie != null)
			});

			SlickToolStrip.Show(Data.Mainform, et == null ? (Point?)null : SL_More.PointToClient(et.Location), items.ToArray());
		}

		private void SL_Audio_Click(object sender, MouseEventArgs e)
		{
			if (e != null && e.Button == MouseButtons.Left)
			{
				Task.Run(() =>
				{
					isMuted = vlcControl.Audio.IsMute = !vlcControl.Audio.IsMute;
					this.TryInvoke(() => SL_Audio.Image = isMuted ? ProjectImages.Tiny_Mute : ProjectImages.Tiny_Sound);
				});
			}
			else
			{
				var tracks = new List<SlickStripItem>();
				var isMute = vlcControl.Audio.IsMute;
				var currentId = vlcControl.Audio.Tracks.Current.ID;

				tracks.AddRange(vlcControl.Audio.Tracks.All, x =>
				{
					return new SlickStripItem(x.Name.If("Disable", "Mute"), () =>
					{
						if (x.ID == -1)
						{
							Task.Run(() => vlcControl.Audio.IsMute = isMuted = true);
						}
						else
						{
							Task.Run(() => { vlcControl.Audio.Tracks.Current = x; vlcControl.Audio.IsMute = isMuted = false; });
						}

						SL_Audio.Image = x.ID == -1 ? ProjectImages.Tiny_Mute : ProjectImages.Tiny_Sound;
					}, (x.ID == (isMute ? -1 : currentId)).If(ProjectImages.Tiny_Ok, null)
					, fade: x.ID == (isMute ? -1 : currentId));
				});

				if (tracks.Count > 0)
				{
					SlickToolStrip.Show(Data.Mainform, tracks.ToArray());
				}
			}
		}

		private void SL_Backwards_Click(object sender, EventArgs e)
		{
			Task.Run(() => vlcControl.Time -= (Data.Options.BackwardTime * 1000) + (Data.Options.ForwardTime % Data.Options.BackwardTime).If(0, 1000 * Data.Options.BackwardTime / Data.Options.ForwardTime, 0));
		}

		private void SL_Forward_Click(object sender, EventArgs e)
		{
			Task.Run(() => vlcControl.Time += Data.Options.ForwardTime * 1000);
		}

		private void SL_FullScreen_Click(object sender, EventArgs e)
		{
			ToggleScreen(FullScreen ? ScreenState.Normal : ScreenState.FullScreen);
		}

		private void SL_MiniPlayer_Click(object sender, EventArgs e)
		{
			ToggleScreen(MiniPlayer || PnP ? ScreenState.Normal : ScreenState.MiniPlayer);
		}

		private void SL_PnP_Click(object sender, EventArgs e)
		{
			if (PnP)
				ToggleScreen(ScreenState.Normal);
			else
			{
				togglingPnP = true;
				Form.PushBack();
				togglingPnP = false;
			}
		}

		private void SL_Next_Click(object sender, EventArgs e)
		{
			if (Episode?.Next?.Playable ?? false)
			{
				countdownTimer?.Dispose();
				if (!Paused)
				{
					Pause();
				}

				var ep = Episode;
				new BackgroundAction(() =>
				{
					ep.MarkAs(true);
					LocalShowHandler.OnWatchInfoChanged(ep.Show, this);
					ep.Save(ChangeType.Preferences);
				}).Run();

				Episode.Next.Play();
			}
		}

		private void SL_Play_Click(object sender, EventArgs e)
		{
			PlayPause();
		}

		private void SL_Previous_Click(object sender, EventArgs e)
		{
			if (Episode?.Previous?.Playable ?? false)
			{
				if (!Paused)
				{
					Pause();
				}

				Episode.Previous.Play();
			}
		}

		private int subDelay;

		private int SubDelay
		{
			get => subDelay;
			set
			{
				subDelay = value;
				subDelaySetIdentifier.Wait(() => Task.Run(() => vlcControl.SubTitles.Delay = subDelay * 1000), 200);
			}
		}

		private void SL_SubDelayMinus_Click(object sender, EventArgs e)
		{
			showSubDelay();
			SubDelay += 100;
			this.TryInvoke(() => L_SubDelay.Text = TimeSpan.FromMilliseconds(SubDelay).ToTinyString());
		}

		private void SL_SubDelayPlus_Click(object sender, EventArgs e)
		{
			showSubDelay();
			SubDelay -= 100;
			this.TryInvoke(() => L_SubDelay.Text = TimeSpan.FromMilliseconds(SubDelay).ToTinyString());
		}

		private void SL_Subs_Click(object sender, EventArgs e)
		{
			var subs = new List<SlickStripItem>();

			subs.AddRange(vlcControl.SubTitles.All, x =>
				new SlickStripItem(x.Name, () =>
				{
					Task.Run(() => vlcControl.SubTitles.Current = x);
				}, (vlcControl.SubTitles.Current.ID == x.ID).If(ProjectImages.Tiny_Ok, null)
				, fade: vlcControl.SubTitles.Current.ID == x.ID));

			for (var i = 0; i < subNames.Count; i++)
				subs[subs.Count - subNames.Count + i].Text += $" - [{subNames[i]}]";

			if (subs.Count == 0) return;

			subs.Add(SlickStripItem.Empty);
			subs.Add(new SlickStripItem("Edit Delay", showSubDelay, ProjectImages.Tiny_Edit/*, show: false*/));

			SlickToolStrip.Show(Data.Mainform, subs.ToArray());
		}

		private void SlickScroll_Scroll(object sender, ScrollEventArgs e)
		{
			if (Data.Options.AutoPauseOnInfo)
			{
				if (slickScroll.Percentage == 0)
				{
					if (pausedFromScroll && !stateChanging)
					{
						Play();
					}
				}
				else
				{
					pausedFromScroll |= !Paused;
					Pause();
				}
			}
			else if (pausedFromScroll && slickScroll.Percentage == 0)
			{
				Play();
				pausedFromScroll = false;
			}

			var showInfo2 = (P_Info.Parent?.Location.Y ?? 0) + P_Info.Location.Y < TLP_MoreInfo.Height;
			var mouseDown = slickScroll.IsMouseDown;

			slickScroll.Visible = slickScroll.ShowHandle = TLP_MoreInfo2.Visible = showInfo2;

			if (!showInfo2 && mouseDown)
			{
				slickScroll.Enabled = false;
				slickScroll.ScrollTo(P_VLC, 7);
				slickScroll.Enabled = true;
			}

			P_Spacer.Visible = slickScroll.Percentage != 0;
			L_MoreInfo.Text = L_MoreInfo2.Text = slickScroll.Percentage.If(0, "More Info", "Less Info");
			I_MoreInfo.Image = I_MoreInfo2.Image = slickScroll.Percentage.If(0, ProjectImages.ArrowDown, ProjectImages.ArrowUp).Color(FormDesign.Design.ForeColor);

			if (mouseDown && !slickScroll.Visible)
			{
				slickScroll.ScrollTo(P_VLC, 7);
			}
		}

		private void SS_TimeSlider_ValuesChanged(object sender, EventArgs e)
		{
			if (timeLoop && Form.Visible)
			{
				bufferIdentifier.Wait(() =>
				{
					if (timeLoop && Form.Visible)
					{
						Task.Run(() =>
						{
							timeLoop = false;

							vlcControl.Time = (long)SS_TimeSlider.Value;

							if (!Streaming)
							{
								if (Episode != null)
								{
									Episode.Progress = 100D * vlcControl.Time / vlcControl.Length;
								}
								else if (Movie != null)
								{
									Movie.Progress = 100D * vlcControl.Time / vlcControl.Length;
								}
							}

							vlcControl_TimeChanged(null, null);

							timeLoop = true;
						});
					}
				}, 25);
			}

			updateTimeLabels();
		}

		private void SS_Volume_ValuesChanged(object sender, EventArgs e)
		{
			Task.Run(() => { vlcControl.Audio.IsMute = isMuted = false; vlcControl.Audio.Volume = (int)SS_Volume.Value; });

			SL_Audio.Image = ProjectImages.Tiny_Sound;
			var targetSize = UI.Scale(new Size(32, 150), UI.UIScale);
			var animating = volumeAnimation?.Animating ?? false;

			if (!volumeIdentifier.Waiting && (!PB_Volume.Visible || !animating))
			{
				if (!animating)
				{
					PB_Volume.TryInvoke(() => PB_Volume.Bounds = new Rectangle(new Point(P_VLC.Width + targetSize.Width, targetSize.Height / 3), targetSize));
				}

				if (!PB_Volume.Visible)
				{
					PB_Volume.TryInvoke(() => PB_Volume.Visible = true);
				}

				volumeAnimation = new AnimationHandler(PB_Volume, new Rectangle(new Point(P_VLC.Width - 15 - targetSize.Width, targetSize.Height / 3), targetSize));

				volumeAnimation.StartAnimation();
			}

			volumeIdentifier.Wait(() =>
			{
				volumeAnimation = new AnimationHandler(PB_Volume, new Point(P_VLC.Width + targetSize.Width, targetSize.Height / 3))
				{ Speed = 0.75 };

				volumeAnimation.StartAnimation(PB_Volume.Hide);
			}, 1500);

			PB_Volume.TryInvoke(PB_Volume.Invalidate);
		}

		private void TLP_Buttons_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && MiniPlayer)
			{
				miniPlayerMoving = true;
				startMiniPlayerMoveCursor = Cursor.Position;
			}
		}

		private void TLP_MoreInfo_Click(object sender, EventArgs e)
		{
			slickScroll.ScrollTo(slickScroll.Percentage.If(0, TLP_Suggestions, P_VLC), 7);
		}

		private void TLP_MoreInfo_MouseEnter(object sender, EventArgs e)
		{
			L_MoreInfo.ForeColor = FormDesign.Design.ActiveColor;
			I_MoreInfo.Color(FormDesign.Design.ActiveColor);
		}

		private void TLP_MoreInfo_MouseLeave(object sender, EventArgs e)
		{
			L_MoreInfo.ForeColor = Color.Empty;
			I_MoreInfo.Color(FormDesign.Design.ForeColor);
		}

		private void TLP_MoreInfo2_MouseEnter(object sender, EventArgs e)
		{
			L_MoreInfo2.ForeColor = FormDesign.Design.ActiveColor;
			I_MoreInfo2.Color(FormDesign.Design.ActiveColor);
		}

		private void TLP_MoreInfo2_MouseLeave(object sender, EventArgs e)
		{
			L_MoreInfo2.ForeColor = Color.Empty;
			I_MoreInfo2.Color(FormDesign.Design.ForeColor);
		}

		private void UpNextCountdown_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (!coundownCancelRect.Contains(e.Location))
				{
					SL_Next_Click(null, null);
				}
				else
				{
					countdownTimer?.Dispose();
					timeLeft = TimeSpan.FromSeconds(5);
					PB_UpNext.Visible = C_Rate.Visible = false;
					upnextHidden = false;
					UpNextCountdown.Invalidate();
				}
			}
		}

		#endregion Form Events

		#region Paint Events

		private void L_TopNotch_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(FormDesign.Design.AccentBackColor);
			e.Graphics.DrawString(Text.ToUpper(), UI.Font(12.75F, FontStyle.Bold), Gradient(new Rectangle(Point.Empty, L_TopNotch.Size), FormDesign.Design.ForeColor), new Rectangle(-5000, 0, L_TopNotch.MaximumSize.Width + 5000 - 8 - ((L_TopNotch.MaximumSize.Width - L_TopNotch.Width) / 4), L_TopNotch.Height), new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center });
			e.Graphics.FillRectangle(Gradient(new Rectangle(Point.Empty, L_TopNotch.Size), FormDesign.Design.ActiveColor), new Rectangle(L_TopNotch.Width - 3, 0, 3, L_TopNotch.Height));
		}

		private void P_Progress_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(P_BotSpacer.Height == 1 ? Color.Black : FormDesign.Design.BackColor);

			if (PnP || MiniPlayer || P_BotSpacer.Height == 0)
				e.Graphics.FillRectangle(new SolidBrush(FormDesign.Design.ActiveColor), new Rectangle(0, -1, (int)(P_Progress.Width * SS_TimeSlider.Value / SS_TimeSlider.MaxValue.If(0, 1)), 4));
		}

		private void PB_Close_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(FormDesign.Design.RedColor);
			e.Graphics.DrawImage(ProjectImages.Tiny_Close.Color(Color.White), new Rectangle(new Rectangle(Point.Empty, PB_Close.Size).Center(ProjectImages.Tiny_Close.Size), ProjectImages.Tiny_Close.Size));
		}

		private void PB_UpNext_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(Color.Black);

			if (PB_UpNext.Loading)
			{
				PB_UpNext.DrawLoader(e.Graphics, new Rectangle(Point.Empty, PB_UpNext.Size));
			}
			else if (PB_UpNext.Image != null)
			{
				var imgRect = new Rectangle(Point.Empty, PB_UpNext.Size);

				e.Graphics.DrawImage(PB_UpNext.Image, imgRect, ImageSizeMode.CenterScaled);

				e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(75, FormDesign.Design.BackColor)), imgRect);
				e.Graphics.FillRectangle(new System.Drawing.Drawing2D.LinearGradientBrush(Point.Empty, new Point(UI.Font(10.5F, FontStyle.Bold).Height + 30, 0), Color.FromArgb(150, FormDesign.Design.BackColor), Color.Empty), new Rectangle(0, 0, UI.Font(10.5F, FontStyle.Bold).Height + 30, PB_UpNext.Height));
				e.Graphics.FillRectangle(new System.Drawing.Drawing2D.LinearGradientBrush(new Point(0, PB_UpNext.Height - UI.Font(8F, FontStyle.Bold).Height - 20), new Point(0, PB_UpNext.Height), Color.Empty, Color.FromArgb(150, FormDesign.Design.BackColor)), new Rectangle(0, PB_UpNext.Height - UI.Font(8F, FontStyle.Bold).Height - 19, PB_UpNext.Width, UI.Font(8F, FontStyle.Bold).Height + 20));

				e.Graphics.DrawImage(ProjectImages.Huge_Play.Color(PB_UpNext.HoverState.HasFlag(HoverState.Hovered) ? FormDesign.Design.ActiveColor : FormDesign.Design.ForeColor), new Rectangle(imgRect.Center(new Size(64, 64)), new Size(64, 64)));

				e.Graphics.DrawString($"Ep. {Episode?.Next?.EN}" + (string.IsNullOrEmpty(Episode?.Next?.Name) ? string.Empty : $" • {Episode?.Next?.Name}"), UI.Font(7.5F, FontStyle.Bold), new SolidBrush(FormDesign.Design.ForeColor), imgRect.Pad(3), new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Far });

				e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
				e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
				e.Graphics.TranslateTransform(0, PB_UpNext.Height);
				e.Graphics.RotateTransform(270);
				e.Graphics.DrawString("UP NEXT", UI.Font(10F, FontStyle.Bold), new SolidBrush(FormDesign.Design.ForeColor), new Rectangle(0, 0, PB_UpNext.Height, PB_UpNext.Width), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near });
			}
		}

		private void PB_Volume_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.FillRectangle(Gradient(new Rectangle(Point.Empty, PB_Volume.Size), FormDesign.Design.BackColor), new Rectangle(Point.Empty, PB_Volume.Size));

			var height = PB_Volume.Height - 24 - 12;
			var size = new Size(PB_Volume.Width * 2 / 5, height * 85 / 100);
			var rect = new Rectangle(new Size(PB_Volume.Width, height).Center(size), size);
			var barHeight = Math.Max(3, (int)(size.Height * SS_Volume.Value / SS_Volume.MaxValue));

			e.Graphics.FillRectangle(Gradient(rect, FormDesign.Design.ForeColor), rect);
			e.Graphics.FillRectangle(Gradient(new Rectangle(rect.X, rect.Y + rect.Height - barHeight, rect.Width, barHeight), FormDesign.Design.ActiveColor),
				new Rectangle(rect.X, rect.Y + rect.Height - barHeight, rect.Width, barHeight));

			e.Graphics.DrawString($"{SS_Volume.Value:0}%", UI.Font(6.75F), Gradient(rect, FormDesign.Design.ForeColor), new Rectangle(0, rect.Height + rect.Y, PB_Volume.Width, PB_Volume.Height - 22 - rect.Height - rect.Y), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

			e.Graphics.DrawImage((isMuted || SS_Volume.Value == 0 ? ProjectImages.Tiny_Mute : ProjectImages.Tiny_Sound).Color(FormDesign.Design.ForeColor)
				, new Rectangle((PB_Volume.Width - 16) / 2, PB_Volume.Height - 22, 16, 16));
		}

		#endregion Paint Events

		#region Up Next Countdown

		private Timer countdownTimer;

		private TimeSpan timeLeft;

		private void startCountdown()
		{
			timeLeft = TimeSpan.FromSeconds(5);

			if (Data.Options.AutomaticEpisodeSwitching)
			{
				countdownTimer = new Timer { Interval = 40 };

				countdownTimer.Tick += (s, e) =>
				{
					timeLeft -= TimeSpan.FromMilliseconds(countdownTimer.Interval);
					UpNextCountdown.Invalidate(new Rectangle(UpNextCountdown.Size.Center(new Size(145, 135)), new Size(135, 135)).Pad(-5));

					if (timeLeft.TotalMilliseconds <= 0)
					{
						SL_Next_Click(null, null);
					}
				};

				this.TryInvoke(countdownTimer.Start);
			}
			else
			{
				this.TryInvoke(PB_UpNext.Hide);
				this.TryInvoke(C_Rate.Hide);
				upnextHidden = false;
			}
		}

		private void UpNextCountdown_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(Color.Black);

			if (UpNextCountdown.Loading)
			{
				UpNextCountdown.DrawLoader(e.Graphics, new Rectangle(100, 100, 32, 32));
			}
			else if (UpNextCountdown.Image != null)
			{
				e.Graphics.DrawImage(UpNextCountdown.Image, new Rectangle(Point.Empty, UpNextCountdown.Size), ImageSizeMode.Fill);
				e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, 0, 0, 0)), new Rectangle(Point.Empty, UpNextCountdown.Size));
			}

			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			var angles = (float)(360 * timeLeft.TotalMilliseconds / 5000);
			var arcRect = new Rectangle(UpNextCountdown.Size.Center(new Size(145, 135)), new Size(135, 135));

			e.Graphics.DrawArc(new Pen(Color.White, 5F)
				, arcRect
				, 272.5F - angles
				, angles);

			e.Graphics.DrawImage(ProjectImages.Play_100.Color(Color.White), new Rectangle(UpNextCountdown.Size.Center(new Size(100, 100)), new Size(100, 100)));

			if (countdownTimer?.Enabled ?? false)
			{
				var bnds = e.Graphics.Measure("CANCEL", UI.Font(11.25F, FontStyle.Bold));
				coundownCancelRect = new Rectangle((UpNextCountdown.Width - (int)bnds.Width - 30) / 2, arcRect.Y + arcRect.Height + 100, (int)bnds.Width + 20, (int)bnds.Height + 10);

				e.Graphics.FillRoundedRectangle(Gradient(coundownCancelRect, Color.FromArgb(coundownCancelRect.Contains(UpNextCountdown.PointToClient(Cursor.Position)) ? 150 : 50, 255, 255, 255)), coundownCancelRect, 7);
				e.Graphics.DrawString("CANCEL", UI.Font(11.25F, FontStyle.Bold), Gradient(coundownCancelRect, Color.White), coundownCancelRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
			}
			else
			{
				coundownCancelRect = Rectangle.Empty;
			}
		}

		#endregion Up Next Countdown

		#region Thumbs

		private string _hoveredTime;
		private bool buffering;
		private Rectangle coundownCancelRect;
		private ThumbnailCollection thumbs;
		private List<string> subNames;

		private void PB_Thumb_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(Color.Black);

			var bitRect = new Rectangle(Point.Empty, PB_Thumb.Size);

			if (PB_Thumb.Image != null)
			{
				e.Graphics.DrawImage(PB_Thumb.Image, bitRect, new Rectangle(
					Math.Abs(bitRect.Center(PB_Thumb.Image.Size).X),
					Math.Abs(bitRect.Center(PB_Thumb.Image.Size).Y),
					bitRect.Width, bitRect.Height), GraphicsUnit.Pixel);
			}
			else if (PB_Thumb.Loading)
			{
				PB_Thumb.DrawLoader(e.Graphics, new Rectangle(bitRect.Center(new Size(32, 32)), new Size(32, 32)));
			}
			else
			{
				e.Graphics.DrawImage(ProjectImages.Icon_Dots_H.Color(Color.WhiteSmoke), new Rectangle(bitRect.Center(ProjectImages.Icon_Dots_H.Size), ProjectImages.Icon_Dots_H.Size));
			}

			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
			e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

			e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, 0, 0, 0)), bitRect);

			var bnds = e.Graphics.Measure(_hoveredTime, UI.Font(12.75F));
			var p = new System.Drawing.Drawing2D.GraphicsPath();

			p.AddString(
				_hoveredTime,
				new FontFamily(UI.FontFamily),
				(int)FontStyle.Regular,
				(float)(e.Graphics.DpiY * 12F * UI.FontScale / 72 / UI.WindowsScale),
				new Point((int)((PB_Thumb.Width - bnds.Width) / 2), PB_Thumb.Height - UI.Font(12F).Height - 3),
				new StringFormat());

			e.Graphics.DrawPath(new Pen(Color.FromArgb(150, 0, 0, 0), (int)(3 * UI.FontScale)), p);
			e.Graphics.FillPath(new SolidBrush(Color.White), p);
		}

		private void SS_TimeSlider_MouseEnter(object sender, EventArgs e)
		{
			if (!Streaming)
			{
				this.TryInvoke(PB_Thumb.Show);
			}
		}

		private void SS_TimeSlider_MouseLeave(object sender, EventArgs e)
		{
			this.TryInvoke(PB_Thumb.Hide);
		}

		private void SS_TimeSlider_MouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				var ts = TimeSpan.FromMilliseconds((SS_TimeSlider.MaxValue * (e.X - SS_TimeSlider.Padding.Left) / (SS_TimeSlider.Width - SS_TimeSlider.Padding.Horizontal)).Between(SS_TimeSlider.MinValue, SS_TimeSlider.MaxValue));
				_hoveredTime = $"{(ts.Hours > 0 ? $"{ts.Hours:00}:" : "")}{$"{ts.Minutes:00}:{ts.Seconds:00}"}";

				PB_Thumb.Image = thumbs?[(float)ts.TotalSeconds];
				PB_Thumb.Loading = PB_Thumb.Image == null;
				PB_Thumb.Location = new Point(P_VLC.PointToClient(new Point(Cursor.Position.X - (PB_Thumb.Width / 2), 0)).X.Between(10, P_VLC.Width - 10 - PB_Thumb.Width), P_VLC.Height - PB_Thumb.Height - 10 - (CurrentScreenState == ScreenState.Normal ? 0 : CONTROLS_SIZE));
			}
			catch { }
		}

		#endregion Thumbs

		private void PlayerControls_FocusEnter(object sender, EventArgs e)
		{
			slickScroll.ScrollTo(0, 7);
			ShowPlayControls();
		}

		private void C_Rate_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(FormDesign.Design.MenuColor);
			e.Graphics.DrawRectangle(new Pen(Color.FromArgb(85, FormDesign.Design.ActiveColor), 1F), new Rectangle(-1, 0, C_Rate.Width, C_Rate.Height - 1));
			e.Graphics.DrawLine(new Pen(FormDesign.Design.AccentColor, 1F), 4, C_Rate.Height / 2, C_Rate.Width - 8, C_Rate.Height / 2);

			var rating_ = Episode?.Rating ?? Movie?.Rating;

			if (rating_ != null)
			{
				var rating = rating_.Value;
				var cur = C_Rate.PointToClient(Cursor.Position);
				var rect = new Rectangle(-1, 0, C_Rate.Width, C_Rate.Height / 2);

				e.Graphics.DrawImage((rating.Loved ? ProjectImages.Icon_HeartFilled : ProjectImages.Icon_Heart)
					.Color(rect.Contains(cur) ? FormDesign.Design.ActiveColor : rating.Loved ? FormDesign.Design.RedColor : FormDesign.Design.MenuForeColor)
					, rect
					, ImageSizeMode.Center);

				rect.Y += rect.Height;

				e.Graphics.DrawImage((rating.Rated ? ProjectImages.Icon_StarFilled : ProjectImages.Icon_Star)
					.Color(rect.Contains(cur) ? FormDesign.Design.ActiveColor : rating.Rated ? rating.Rating.RatingColor() : FormDesign.Design.MenuForeColor)
					, rect
					, ImageSizeMode.Center);

				if (rating.Rated)
					e.Graphics.DrawString(rating.Rating.ToString("0"), new Font(UI.FontFamily, 12.75F, FontStyle.Bold), new SolidBrush(FormDesign.Design.BackColor), rect.Pad(0, 2, 0, 0), new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
			}
		}

		private void C_Rate_MouseClick(object sender, MouseEventArgs e)
		{
			var c = Episode ?? Movie as IContent;

			if (c != null)
			{
				if (e.Y <= C_Rate.Height / 2)
					c.Rating = c.Rating.SwitchLove();
				else if (e.Button == MouseButtons.Right)
					c.Rating = c.Rating.UnRate();
				else
				{
					var replay = !Paused;
					Pause();

					var res = RateForm.Show(c.Rating.Rated ? c.Rating.Rating : 5);
					if (res.Item1)
						c.Rating = c.Rating.Rate(res.Item2);

					if (replay)
						Play();
				}

				new BackgroundAction(() => c.Save(ChangeType.Preferences)).Run();
			}
		}
	}

#pragma warning restore CS0618 // Type or member is obsolete
}