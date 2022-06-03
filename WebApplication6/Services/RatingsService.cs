using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication6.Models;

namespace WebApplication6.Services
{
	public class RatingsService
	{
		private static List<Ratings> ratings = new List<Ratings>();

		public List<Ratings> GetAll()
		{
			return ratings;
		}


		public Ratings GetById(int Id)
		{
			return ratings.Find(x => x.Id == Id);
		}

		public void Edit(int id, int rate, string feedback)
		{
			Ratings cur = GetById(id);
			cur.Rate = rate;
			cur.FeedBack = feedback;
			string time = DateTime.Now.ToString("h:mm:ss tt");
			string date = DateTime.Today.ToString("dd/MM/yyyy");
			cur.Date = date;
			cur.Time = time;
		}

		public void Create(string author, int rate, string feedback)
		{
			int nextId = 0;
			if (ratings.Count > 0)
			{
				nextId = ratings.Max(x => x.Id) + 1;
			}
			else
			{
				nextId = 1;
			}
			string time = DateTime.Now.ToString("h:mm:ss tt");
			string date = DateTime.Today.ToString("dd/MM/yyyy");

			ratings.Add(new Ratings() { Id = nextId, Author = author, Rate = rate, FeedBack = feedback, Time = time, Date = date }); ;
		}

		public void Delete(int Id)
		{
			ratings.Remove(GetById(Id));
		}
	}
}