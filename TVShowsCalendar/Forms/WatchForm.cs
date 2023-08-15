using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AudioSwitcher.AudioApi.CoreAudio;
using Extensions;
using SlickControls.Classes;
using SlickControls.Forms;
using TVShowsCalendar.Classes;
using TVShowsCalendar.HandlerClasses;
using ProjectImages = TVShowsCalendar.Properties.Resources;

namespace TVShowsCalendar.Forms
{
	public partial class WatchForm : BaseForm
	{
		private AnimationHandler ControlsHideAnimation;
		private WaitIdentifier ControlsHideIdentifier = new WaitIdentifier();
		private bool ControlsOpening = false;
		private Episode Episode;
		private Movie Movie;
		private DateTime lastClick = DateTime.MinValue;
		private Point LastMousePoint = Point.Empty;
		private bool TimeLoop = true;
		private bool firstLoad;
		private bool sizeSet;
		private CoreAudioController audioController;

		private WatchForm(FileInfo file)
		{
			InitializeComponent();
			Location = SystemInformation.VirtualScreen.Center(Size);

			SS_TimeSlider.ValueOutput = (v) =>
			{
				var ts = TimeSpan.FromMilliseconds(v.If(double.NaN, 0));

				return (ts.Hours > 0 ? $"{ts.Hours.ToString("00")}:" : "") + $"{ts.Minutes.ToString("00")}:{ts.Seconds.ToString("00")}";
			};

			base_P_Content.Controls.Add((vlcControl = new Vlc.DotNet.Forms.VlcControl()));

			vlcControl.BeginInit();
			vlcControl.Dock = DockStyle.Fill;
			vlcControl.Spu = -1;
			vlcControl.TabIndex = 0;
			vlcControl.VlcLibDirectory = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "vlc"));
			vlcControl.VlcMediaplayerOptions = new string[] { "-vv" };
			vlcControl.Paused += vlcControl_Paused;
			vlcControl.Playing += vlcControl_Playing;
			vlcControl.TimeChanged += vlcControl_TimeChanged;
			vlcControl.EndReached += VlcControl_EndReached;
			vlcControl.EndInit();

			SlickTip.SetTo(SL_Backwards, $"Go Back {Data.Options.BackwardTime} Seconds");
			SlickTip.SetTo(SL_Forward, $"Go Forward {Data.Options.ForwardTime} Seconds");
			SlickTip.SetTo(SL_Previous, "Switch to Previous Episode");
			SlickTip.SetTo(SL_Next, "Switch to Next Episode");
			SlickTip.SetTo(SL_Play, "Play / Pause");
			SlickTip.SetTo(SI_More, "More Options");
			SlickTip.SetTo(SL_Subs, "Select Subtitles");
			SlickTip.SetTo(SL_Audio, "Select Sound Track");
			SlickTip.SetTo(SL_MiniPlayer, "Toggle Mini-Player");
			SlickTip.SetTo(SL_FullScreen, "Toggle Full-screen");

			var md = new MouseDetector();
			md.MouseMove += (s, e) =>
			{
				if (new Rectangle(vlcControl.PointToScreen(Point.Empty), vlcControl.Size).Contains(e)
					&& LastMousePoint != e)
				{
					ShowPlayControls();
				}
			};
			Disposed += (s, e) => md.Dispose();
			TLP_Controls.MouseDown += Form_MouseDown;
			TLP_Buttons.MouseDown += Form_MouseDown;
			vlcControl.MouseDown += Form_MouseDown;

			new Action(() =>
			{
				audioController = new CoreAudioController();
				this.TryInvoke(() => SL_Audio.Enabled = true);
			}).RunInBackground();

			if (Data.Options.FullScreenPlayer)
				ToggleFullscreen();
		}

		public WatchForm(Episode ep, FileInfo file) : this(file)
		{
			SetEpisode(ep, file);
			SL_Next.Visible = SL_Previous.Visible = true;
		}

		public WatchForm(Movie movie, FileInfo file) : this(file)
		{
			SetMovie(movie, file);
		}

		private void VlcControl_EndReached(object sender, Vlc.DotNet.Core.VlcMediaPlayerEndReachedEventArgs e)
		{
			if (FullScreen)
				ToggleFullscreen();
			Invoke(new Action(() => SL_Play.Image = ProjectImages.Icon_Play));
			if (Episode != null)
				SetEpisode(Episode.Next, play: false);
		}

		public bool FullScreen { get; private set; } = false;
		public bool Paused { get; private set; } = false;

		private void FileLoaded()
		{
			StopLoader();
			P_BotSpacer.Enabled = true;
			if (SL_Subs.Enabled = vlcControl.SubTitles.All.Any(x => x.ID != -1))
				vlcControl.SubTitles.Current = vlcControl.SubTitles.All.FirstThat(x => x.ID != -1);
			ToggleFullscreen(false);
			ShowPlayControls();
		}

		public void SetMovie(Movie movie, FileInfo epFile = null, bool play = true)
		{
			if (movie != null)
			{
				epFile = epFile != null && epFile.Exists ? epFile : LocalMovieHandler.GetFile(movie);
				if (epFile != null)
				{
					StartLoader();
					this.TryInvoke(() => { Text = movie.Title; P_BotSpacer.Enabled = false; });
					new Action(() =>
					{
						firstLoad = true;
						SaveWatchtime();
						Movie = movie;

						vlcControl.SetMedia(epFile, GetSubs(epFile, true).ToArray());
						if (play)
							vlcControl.Play();
						else if (vlcControl.IsPlaying)
							vlcControl.Pause();

						ShowPlayControls();

						TimeLoop = false;
						vlcControl.Time = movie.WatchTime - 5000;
						TimeLoop = true;
						sizeSet = false;

						if (!play)
						{
							this.TryInvoke(() => { SS_TimeSlider.Value = 0; P_BotSpacer.Enabled = true; });
							StopLoader();
						}
					}).RunInBackground();
				}
				else
					this.TryInvoke(() =>
					{
						MessagePrompt.Show($"\"{movie.Title}\" does not seem to have a file associated with it.\nCheck that a file exists, or is named correctly", "File Missing", icon: SlickControls.Enums.PromptIcons.Error);
					});
			}
		}

		private IEnumerable<string> GetSubs(FileInfo epFile, bool movie)
		{
			foreach (var item in Directory.GetParent(epFile.FullName).GetFilesByExtensions(".srt"))
			{
				if (movie || item.FileName().SpellCheck(epFile.FileName(), false) <= 2)
					yield return $"-sub-file=file:///{item.FullName.Replace('\\', '/')}";
			}
		}

		public void SetEpisode(Episode ep, FileInfo epFile = null, bool play = true)
		{
			if (ep != null)
			{
				epFile = epFile != null && epFile.Exists ? epFile : LocalShowHandler.GetFile(ep);
				if (epFile != null)
				{
					StartLoader();
					this.TryInvoke(() => { Text = $"{ep.Show} • {ep}"; P_BotSpacer.Enabled = false; });
					new Action(() =>
					{
						firstLoad = true;
						SaveWatchtime();
						Episode = ep;

						vlcControl.SetMedia(epFile, GetSubs(epFile, true).ToArray());
						if (play)
							vlcControl.Play();
						else if (vlcControl.IsPlaying)
							vlcControl.Pause();

						ShowPlayControls();

						TimeLoop = false;
						vlcControl.Time = ep.WatchTime - 5000;
						TimeLoop = true;
						sizeSet = false;

						if (!play)
						{
							this.TryInvoke(() => { SS_TimeSlider.Value = 0; P_BotSpacer.Enabled = true; });
							StopLoader();
						}
					}).RunInBackground();
				}
				else
					this.TryInvoke(() =>
					{
						MessagePrompt.Show($"Episode \"{ep}\" does not seem to have a file associated with it.\nCheck that a file exists, or is named correctly", "File Missing", icon: SlickControls.Enums.PromptIcons.Error);
						if (!string.IsNullOrWhiteSpace(ep.Show.ZooqleURL)
							&& DialogResult.Yes == MessagePrompt.Show($"Would you like to open the download window for \"{ep}\" ?", "Open Downloads", SlickControls.Enums.PromptButtons.YesNo, SlickControls.Enums.PromptIcons.Question))
							new DownloadForm(ep).Show();
					});
			}
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			SaveWatchtime();
			base.OnClosing(e);
		}

		private void SaveWatchtime()
		{
			if (Episode != null)
			{
				if (vlcControl.Length > 0 && vlcControl.Time > 10000)
				{
					var ended = vlcControl.Time > vlcControl.Length * 95 / 100;
					Episode.WatchTime = ended ? 0 : vlcControl.Time;
					Episode.Watched = Episode.Watched || ended;
					Episode.WatchDate = DateTime.Now;
					Episode.Progress = Episode.Started ? 100 * vlcControl.Time / vlcControl.Length : 0;

					new Action(() =>
					{
						ShowManager.Save(Episode);
						LocalShowHandler.Refresh(Episode.Show); ;
					}).RunInBackground();
				}
			}
			else if (Movie != null)
			{
				if (vlcControl.Length > 0 && vlcControl.Time > 10000)
				{
					var ended = vlcControl.Time > vlcControl.Length * 90 / 100;
					Movie.WatchTime = ended ? 0 : vlcControl.Time;
					Movie.WatchDate = DateTime.Now;
					Movie.Watched = Episode.Watched || ended;
					Movie.Progress = Movie.Started ? 100 * vlcControl.Time / vlcControl.Length : 0;

					new Action(() =>
					{
						MovieManager.Save(Movie);
						LocalMovieHandler.Refresh(Movie); ;
					}).RunInBackground();
				}
			}
		}

		internal void SL_Play_Click(object sender, EventArgs e)
		{
			if (vlcControl.IsPlaying)
			{
				vlcControl.Pause();
				Paused = true;
				SaveWatchtime();
			}
			else
			{
				vlcControl.Play();
				Paused = false;
			}
			ShowPlayControls();
		}

		protected override void DesignChanged(FormDesign design)
		{
			TLP_Controls.BackColor = design.LightColor;
			P_BotSpacer.BackColor = design.Type.If(FormDesignType.Light, design.DarkColor, design.MenuColor);
			base.DesignChanged(design);
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Space)
			{
				SL_Play_Click(null, null);
				return true;
			}

			if (keyData == Keys.Left)
			{
				SL_Backwards_Click(null, null);
				return true;
			}

			if (keyData == Keys.Right)
			{
				SL_Forward_Click(null, null);
				return true;
			}

			if (keyData == Keys.Up)
			{
				SS_Volume.Value += 5;
				return true;
			}

			if (keyData == Keys.Down)
			{
				SS_Volume.Value -= 5;
				return true;
			}

			if (keyData == Keys.Escape)
			{
				WindowState = FormWindowState.Normal;
				FullScreen = false;

				vlcControl.Parent = base_P_Content;
				P_BotSpacer.Parent = base_P_Content;
				vlcControl.SendToBack();
				P_BotSpacer.BringToFront();
				return true;
			}

			if (keyData == (Keys.Alt | Keys.Enter))
			{
				WindowState = FullScreen ? FormWindowState.Normal : FormWindowState.Maximized;
				FullScreen = WindowState == FormWindowState.Maximized;

				if (FullScreen)
				{
					vlcControl.Parent = this;
					P_BotSpacer.Parent = this;
					vlcControl.BringToFront();
					P_BotSpacer.BringToFront();
				}
				else
				{
					vlcControl.Parent = base_P_Content;
					P_BotSpacer.Parent = base_P_Content;
					vlcControl.SendToBack();
					P_BotSpacer.BringToFront();
				}

				ShowPlayControls();
			}

			return base.ProcessCmdKey(ref msg, keyData);
		}

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 0x210 && m.WParam.ToInt32() == 513) //513 is WM_LBUTTONCLICK
			{
				if (new Rectangle(vlcControl.PointToScreen(Point.Empty), vlcControl.Size).Contains(Cursor.Position)
					&& !new Rectangle(P_BotSpacer.PointToScreen(Point.Empty), P_BotSpacer.Size).Contains(Cursor.Position))
				{
					if (DateTime.Now.Ticks - lastClick.Ticks < 500 * TimeSpan.TicksPerMillisecond)
					{
						SL_Play_Click(null, null);

						ToggleFullscreen();

						lastClick = DateTime.MinValue;
					}
					else
					{
						SL_Play_Click(null, null);
						lastClick = DateTime.Now;
					}
				}
			}

			base.WndProc(ref m);
		}

		private void ShowPlayControls()
		{
			if (ControlsOpening)
				return;

			if (P_BotSpacer.Height != 65)
			{
				ControlsOpening = true;
				ControlsHideAnimation?.Dispose();
				ControlsHideAnimation = new AnimationHandler(P_BotSpacer, new Size(P_BotSpacer.Width, 65)) { IgnoreWidth = true, SpeedModifier = 5 };
				ControlsHideAnimation.StartAnimation();
				ControlsHideAnimation.OnEnd += (s, e, t) => ControlsOpening = false;
			}

			LastMousePoint = Cursor.Position;

			if (!Paused)
			{
				ControlsHideIdentifier.Enable();

				ControlsHideIdentifier.Wait(HideControls, 750);
			}
			else
			{
				ControlsHideIdentifier.Disable();
			}
		}

		private void HideControls() => this.TryInvoke(() =>
		{
			if (!Paused && !firstLoad && !new Rectangle(P_BotSpacer.PointToScreen(new Point(10, 10)), new Size(P_BotSpacer.Width - 20, P_BotSpacer.Height - 20)).Contains(Cursor.Position))
			{
				LastMousePoint = Cursor.Position;
				ControlsHideAnimation?.Dispose();
				ControlsHideAnimation = new AnimationHandler(P_BotSpacer, new Size(P_BotSpacer.Width, 0)) { IgnoreWidth = true, Interval = 10, SpeedModifier = 20 };
				ControlsHideAnimation.StartAnimation();
			}
		});

		private void SI_More_Click(object sender, EventArgs et)
		{
			List<FlatStripItem> items = null;

			if (Episode != null)
			{
				items = new List<FlatStripItem>()
				{
					new FlatStripItem("Toggle Full Screen", () =>
					{
						ToggleFullscreen();
					}, image: FullScreen ? ProjectImages.Icon_NormScreen : ProjectImages.Icon_FullScreen),
					new FlatStripItem("More Info", () =>
					{
						if (vlcControl.IsPlaying)
							SL_Play_Click(null,null);
						var frm = new VidInfoForm(Episode, vlcControl);
						frm.Location = new Point(((Width - frm.Width) / 2) + Location.X,((Height - frm.Height) / 2) + Location.Y);
						frm.ShowUp();
					}, image: ProjectImages.Icon_Info),
					new FlatStripItem("Show Page", () =>
					{
						if (vlcControl.IsPlaying)
							SL_Play_Click(null,null);
					}, image: ProjectImages.Icon_VTV),
					new FlatStripItem("View File", () =>
					{
						if (vlcControl.IsPlaying)
							SL_Play_Click(null,null);
						Cursor.Current = Cursors.WaitCursor;
						System.Diagnostics.Process.Start(Episode.VidFile.Directory.FullName);
						Cursor.Current = Cursors.Default;
					}, image: ProjectImages.Icon_Folder, show: !string.IsNullOrWhiteSpace(Episode.VidFile?.Directory?.FullName)),
					new FlatStripItem("Mark", image: ProjectImages.Icon_OK, fade: true),
					new FlatStripItem("  Ep. as " + (Episode.Watched ? "Unwatched" : "Watched"), () =>
					{
						Cursor.Current = Cursors.WaitCursor;
						Episode.Watched = !Episode.Watched;
						new Action(()=>
						{
							LocalShowHandler.Refresh(Episode.Show);
							ShowManager.Save(Episode);
						}).RunInBackground();
						Cursor.Current = Cursors.Default;
					}),
					new FlatStripItem("  Season as " + (Episode.Season.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched) ? "Unwatched" : "Watched"), () =>
					{
						Cursor.Current = Cursors.WaitCursor;
						var set = !Episode.Season.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched);
						Episode.Season.Episodes.ForEach(e => e.Watched = e.AirState == AirStateEnum.Aired ? set : e.Watched);
						new Action(()=>
						{
							LocalShowHandler.Refresh(Episode.Show);
							ShowManager.Save(Episode.Show);
						}).RunInBackground();
						Cursor.Current = Cursors.Default;
					}),
					new FlatStripItem("  Show as " + (Episode.Show.Seasons.All(s => s.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched)) ? "Unwatched" : "Watched"), () =>
					{
						Cursor.Current = Cursors.WaitCursor;
						var set = !Episode.Show.Seasons.All(s => s.Episodes.All(x => x.AirState != AirStateEnum.Aired || x.Watched));
						Episode.Show.Seasons.ForEach(x => x.Episodes.ForEach(e => e.Watched = e.AirState == AirStateEnum.Aired ? set : e.Watched));
						new Action(()=>
						{
							LocalShowHandler.Refresh(Episode.Show);
							ShowManager.Save(Episode.Show);
						}).RunInBackground();
						Cursor.Current = Cursors.Default;
					})
				};
			}
			else if (Movie != null)
			{
				items = new List<FlatStripItem>()
				{
					new FlatStripItem("Toggle Full Screen", () =>
					{
						ToggleFullscreen();
					}, image: FullScreen ? ProjectImages.Icon_NormScreen : ProjectImages.Icon_FullScreen),
					new FlatStripItem("More Info", () =>
					{
						if (vlcControl.IsPlaying)
							SL_Play_Click(null,null);
						var frm = new VidInfoForm(Movie, vlcControl);
						frm.Location = new Point(((Width - frm.Width) / 2) + Location.X,((Height - frm.Height) / 2) + Location.Y);
						frm.ShowUp();
					}, image: ProjectImages.Icon_Info),
					new FlatStripItem("Movie Page", () =>
					{
						if (vlcControl.IsPlaying)
							SL_Play_Click(null,null);
						Cursor.Current = Cursors.WaitCursor;
						if (Movie.Page == null)
							Movie.Page = new MoviePage(Movie);

						Movie.Page.ShowUp();
						Cursor.Current = Cursors.Default;
					}, image: ProjectImages.Icon_Movie),
					new FlatStripItem("View File", () =>
					{
						if (vlcControl.IsPlaying)
							SL_Play_Click(null,null);
						Cursor.Current = Cursors.WaitCursor;
						System.Diagnostics.Process.Start(Movie.VidFile.Directory.FullName);
						Cursor.Current = Cursors.Default;
					}, image: ProjectImages.Icon_Folder, show: !string.IsNullOrWhiteSpace(Movie.VidFile?.Directory?.FullName)),
					new FlatStripItem($"Mark as {(Movie.Watched ? "Unwatched" : "Watched")}", () =>
					{
						Cursor.Current = Cursors.WaitCursor;
						Movie.Watched = !Movie.Watched;
						new Action(()=>
						{
							LocalMovieHandler.Refresh(Movie);
							MovieManager.Save(Movie);
						}).RunInBackground();
						Cursor.Current = Cursors.Default;
					}, image: ProjectImages.Icon_OK)
				};
			}

			new FlatToolStrip(items).ShowUp();
		}

		private void ToggleFullscreen(bool change = true)
		{
			if (change)
				WindowState = TopMost || FullScreen ? FormWindowState.Normal : FormWindowState.Maximized;
			FullScreen = WindowState == FormWindowState.Maximized;

			if (FullScreen)
			{
				vlcControl.Parent = this;
				P_BotSpacer.Parent = this;
				vlcControl.BringToFront();
				P_BotSpacer.BringToFront();
				SL_FullScreen.Image = ProjectImages.Icon_NormScreen;
			}
			else
			{
				if (TopMost)
				{
					vlcControl.Parent = this;
					P_BotSpacer.Parent = this;
					vlcControl.BringToFront();
					P_BotSpacer.BringToFront();
				}
				else
				{
					vlcControl.Parent = base_P_Content;
					P_BotSpacer.Parent = base_P_Content;
					vlcControl.SendToBack();
					P_BotSpacer.BringToFront();
				}
				SL_FullScreen.Image = ProjectImages.Icon_FullScreen;

				if (!sizeSet)
				{
					sizeSet = true;

					var vidInf = vlcControl.GetCurrentMedia().TracksInformations.FirstThat(mediaInformation => mediaInformation.Type == Vlc.DotNet.Core.Interops.Signatures.MediaTrackTypes.Video).Video;
					var w = Width - 19;
					Height = 46 + (int)(w * vidInf.Height / vidInf.Width);
					Location = SystemInformation.VirtualScreen.Size.Center(Size);
				}
			}

			ShowPlayControls();
		}

		private void SL_Backwards_Click(object sender, EventArgs e)
		{
			vlcControl.Time -= Data.Options.BackwardTime * 1000;
		}

		private void SL_Forward_Click(object sender, EventArgs e)
		{
			vlcControl.Time += Data.Options.ForwardTime * 1000;
		}

		private void SL_Next_Click(object sender, EventArgs e)
		{
			SetEpisode(Episode.Next);
		}

		private void SL_Previous_Click(object sender, EventArgs e)
		{
			SetEpisode(Episode.Previous);
		}

		private void slickSlider1_MouseDown(object sender, MouseEventArgs e)
		{
			if (!Paused && vlcControl.IsPlaying)
				vlcControl.Pause();
		}

		private void slickSlider1_MouseUp(object sender, MouseEventArgs e)
		{
			if (!Paused)
				vlcControl.Play();
		}

		private void slickSlider1_ValuesChanged(object sender, EventArgs e)
		{
			if (TimeLoop)
			{
				TimeLoop = false;
				vlcControl.Time = (long)SS_TimeSlider.Value;
				if (Episode != null)
					Episode.Progress = 100 * vlcControl.Time / vlcControl.Length;
				var ts = TimeSpan.FromMilliseconds(vlcControl.Length - vlcControl.Time);
				L_Time.TryInvoke(() => L_Time.Text = (ts.Hours > 0 ? $"-{ts.Hours.ToString("00")}:" : "-") + $"{ts.Minutes.ToString("00")}:{ts.Seconds.ToString("00")}");

				ts = TimeSpan.FromMilliseconds(SS_TimeSlider.Value.If(double.NaN, 0));
				L_CurrentTime.TryInvoke(() => L_CurrentTime.Text = (ts.Hours > 0 ? $"{ts.Hours.ToString("00")}:" : "") + $"{ts.Minutes.ToString("00")}:{ts.Seconds.ToString("00")}");
				TimeLoop = true;
			}
		}

		private void vlcControl_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(Color.White);
		}

		private void vlcControl_Paused(object sender, Vlc.DotNet.Core.VlcMediaPlayerPausedEventArgs e)
		{
			Invoke(new Action(() => SL_Play.Image = ProjectImages.Icon_Play));
		}

		private void vlcControl_Playing(object sender, Vlc.DotNet.Core.VlcMediaPlayerPlayingEventArgs e)
		{
			SS_TimeSlider.MaxValue = vlcControl.Length;
			Invoke(new Action(() => SL_Play.Image = ProjectImages.Icon_Pause));

			if (firstLoad)
			{
				firstLoad = false;
				this.TryInvoke(FileLoaded);
			}
		}

		private void vlcControl_TimeChanged(object sender, Vlc.DotNet.Core.VlcMediaPlayerTimeChangedEventArgs e)
		{
			if (TimeLoop)
			{
				TimeLoop = false;
				SS_TimeSlider.Value = vlcControl.Time;
				if (Episode != null)
					Episode.Progress = 100 * vlcControl.Time / vlcControl.Length;
				var ts = TimeSpan.FromMilliseconds(vlcControl.Length - vlcControl.Time);
				L_Time.TryInvoke(() => L_Time.Text = (ts.Hours > 0 ? $"-{ts.Hours.ToString("00")}:" : "-") + $"{ts.Minutes.ToString("00")}:{ts.Seconds.ToString("00")}");

				ts = TimeSpan.FromMilliseconds(SS_TimeSlider.Value.If(double.NaN, 0));
				L_CurrentTime.TryInvoke(() => L_CurrentTime.Text = (ts.Hours > 0 ? $"{ts.Hours.ToString("00")}:" : "") + $"{ts.Minutes.ToString("00")}:{ts.Seconds.ToString("00")}");
				TimeLoop = true;
			}
		}

		private void WatchForm_Load(object sender, EventArgs e)
		{
			ShowPlayControls();
		}

		private void SL_Subs_Click(object sender, EventArgs e)
		{
			var subs = new List<FlatStripItem>();

			subs.AddRange(vlcControl.SubTitles.All, x =>
				new FlatStripItem(x.Name, () =>
				{
					vlcControl.SubTitles.Current = x;
				}, (vlcControl.SubTitles.Current.ID == x.ID).If(ProjectImages.ArrowRight, null)
				, fade: vlcControl.SubTitles.Current.ID == x.ID));

			if (subs.Count > 0)
				new FlatToolStrip(subs).ShowUp(); 
		}

		private void SL_Audio_Click(object sender, EventArgs e)
		{
			var tracks = new List<FlatStripItem>();

			tracks.AddRange(vlcControl.Audio.Tracks.All, x =>
				new FlatStripItem(x.Name, () =>
				{
					vlcControl.Audio.Tracks.Current = x;
				}, (vlcControl.Audio.Tracks.Current.ID == x.ID).If(ProjectImages.ArrowRight, null)
				, fade: vlcControl.Audio.Tracks.Current.ID == x.ID));

			if (tracks.Count > 0)
				new FlatToolStrip(tracks).ShowUp();
		}

		private void SS_Volume_ValuesChanged(object sender, EventArgs e)
		{
			vlcControl.Audio.Volume = (int)SS_Volume.Value;
		}

		private void SL_MiniPlayer_Click(object sender, EventArgs e)
		{
			var vidInf = vlcControl.GetCurrentMedia().TracksInformations.FirstThat(mediaInformation => mediaInformation.Type == Vlc.DotNet.Core.Interops.Signatures.MediaTrackTypes.Video).Video;

			if (TopMost)
			{
				TopMost = false;
				ToggleFullscreen(false);
				SL_MiniPlayer.Image = ProjectImages.Icon_MiniPlayerOn;

				var s = new Size(913, 46 + (int)(894 * vidInf.Height / vidInf.Width));
				Bounds = new Rectangle(SystemInformation.VirtualScreen.Center(s), s);
			}
			else
			{
				TopMost = true;
				ToggleFullscreen();
				SL_MiniPlayer.Image = ProjectImages.Icon_MiniPlayerOff;

				var h = 290;
				Bounds = new Rectangle(22, SystemInformation.VirtualScreen.Height - 325, (int)(h * vidInf.Width / vidInf.Height) - 19, 290);
			}

			SI_More.Visible = SS_Volume.Visible = !TopMost;
			TLP_Buttons.ColumnStyles[2].Width = TLP_Buttons.ColumnStyles[TLP_Buttons.ColumnCount - 1].Width = TopMost ? 0 : 50;
		}

		private void SL_FullScreen_Click(object sender, EventArgs e)
		{
			if (TopMost)
				SL_MiniPlayer_Click(null, null);
			ToggleFullscreen();
		}

		private void WatchForm_MouseLeave(object sender, EventArgs e)
		{
			ControlsHideIdentifier.Wait(HideControls, 500);
		}

		private void WatchForm_WindowStateChanged(object sender, EventArgs e)
		{
			if (!TopMost && WindowState == FormWindowState.Normal)
				ToggleFullscreen(false);
		}
	}
}