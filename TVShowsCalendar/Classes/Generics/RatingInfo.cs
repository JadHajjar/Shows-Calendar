using System;
using System.Collections.Generic;
using System.Linq;

namespace ShowsCalendar
{
	public struct RatingInfo
	{
		private List<string> categories;
		private List<string> tags;

		public bool Rated { get; set; }
		public double Rating { get; set; }
		public string[] Categories { get => categories?.ToArray() ?? Array.Empty<string>(); set => categories = value?.ToList(); }
		public string[] Tags { get => tags?.ToArray() ?? Array.Empty<string>(); set => tags = value?.ToList(); }
		public bool Loved { get; set; }

		public RatingInfo SwitchLove()
		{
			Loved = !Loved;
			return this;
		}

		public RatingInfo AddTag(string v)
		{
			if (tags == null)
				tags = new List<string> { v };
			else
				tags.Add(v);

			return this;
		}

		public RatingInfo RemoveTag(string tag)
		{
			if (tags != null && tags.Contains(tag))
				tags.Remove(tag);

			return this;
		}

		public RatingInfo Rate(double rating)
		{
			Rated = true;
			Rating = rating;
			return this;
		}

		public RatingInfo UnRate()
		{
			Rated = false;
			Rating = 0;
			return this;
		}

		public override bool Equals(object obj)
		{
			return obj is RatingInfo info &&
				   EqualityComparer<List<string>>.Default.Equals(categories, info.categories) &&
				   EqualityComparer<List<string>>.Default.Equals(tags, info.tags) &&
				   Rated == info.Rated &&
				   Rating == info.Rating &&
				   Loved == info.Loved;
		}

		public override int GetHashCode()
		{
			var hashCode = -1930886507;
			hashCode = hashCode * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(categories);
			hashCode = hashCode * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(tags);
			hashCode = hashCode * -1521134295 + Rated.GetHashCode();
			hashCode = hashCode * -1521134295 + Rating.GetHashCode();
			hashCode = hashCode * -1521134295 + Loved.GetHashCode();
			return hashCode;
		}

		public static bool operator ==(RatingInfo left, RatingInfo right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(RatingInfo left, RatingInfo right)
		{
			return !(left == right);
		}
	}
}