using Extensions;

using System;
using System.Collections.Generic;
using System.Linq;

using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;

using MovieCast = TMDbLib.Objects.Movies.Cast;
using TmdbPerson = TMDbLib.Objects.People.Person;
using TvCast = TMDbLib.Objects.TvShows.Cast;

namespace ShowsCalendar
{
	public class Person
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ProfilePath { get; set; }
		public TmdbPerson TmdbPerson { get; set; }
		public List<string> Jobs { get; private set; }
		public int Hits { get; private set; }

		public int Age
		{
			get
			{
				if (TmdbPerson?.Birthday == null) return 0;

				var today = TmdbPerson.Deathday ?? DateTime.Today;
				var age = today.Year - TmdbPerson.Birthday.Value.Year;

				if (TmdbPerson.Birthday.Value.Date > today.AddYears(-age)) age--;

				return age;
			}
		}

		private string job;
		private string location;

		public static Person Merge(IEnumerable<Person> c) => new Person
		{
			Id = c.First().Id,
			Name = c.First().Name,
			ProfilePath = c.First().ProfilePath,
			Jobs = c.Select(x => x.job).Concat(c.Select(y => y.location)).Distinct().ToList(),
			Hits = c.Distinct(y => y.location).Count()
		};

		public override bool Equals(object obj)
		{
			return obj is Person person &&
				   Id == person.Id;
		}

		public override int GetHashCode()
		{
			return 2108858624 + Id.GetHashCode();
		}

		public Person()
		{
		}

		public Person(SearchPerson c)
		{
			Id = c.Id;
			Name = c.Name;
			ProfilePath = c.ProfilePath;
		}

		public Person(Crew c, string location)
		{
			Id = c.Id;
			Name = c.Name;
			ProfilePath = c.ProfilePath;
			job = c.Job;
			this.location = location;
		}

		public Person(MovieCast c, string location)
		{
			Id = c.Id;
			Name = c.Name;
			ProfilePath = c.ProfilePath;
			job = c.Character;
			this.location = location;
		}

		public Person(TvCast c, string location)
		{
			Id = c.Id;
			Name = c.Name;
			ProfilePath = c.ProfilePath;
			job = c.Character;
			this.location = location;
		}

		public Person(CreatedBy c, string location)
		{
			Id = c.Id;
			Name = c.Name;
			ProfilePath = c.ProfilePath;
			job = "Creator";
			this.location = location;
		}

		public static bool operator ==(Person left, Person right) => EqualityComparer<Person>.Default.Equals(left, right);

		public static bool operator !=(Person left, Person right) => !(left == right);
	}
}