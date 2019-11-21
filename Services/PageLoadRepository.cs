using LiteDB;
using Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Site.Services
{
	public interface IPageLoadRepository
	{
		List<PageView> GetAllPageViews();
		void SavePageView(string url);
	}

	public class PageLoadRepository : IPageLoadRepository
	{
		private readonly string databasePath;

		public PageLoadRepository(string databasePath)
		{
			this.databasePath = databasePath ?? throw new ArgumentNullException(nameof(databasePath));
		}

		public List<PageView> GetAllPageViews()
		{
			// Open or create database
			using (LiteDatabase db = new LiteDatabase(databasePath))
			{
				LiteCollection<PageView> pageViews = db.GetCollection<PageView>("pageviews");
				List<PageView> allViews = pageViews.Find(Query.All()).ToList();
				return (
					from p in allViews
					orderby p.ViewDate descending
					select p
				).ToList();
			}
		}

		public void SavePageView(string url)
		{
			PageView page = new PageView
			{
				Url = url,
				Server = Environment.MachineName,
				ViewDate = DateTime.UtcNow
			};


			// Open or create database
			using (LiteDatabase db = new LiteDatabase(databasePath))
			{
				LiteCollection<PageView> pageViews = db.GetCollection<PageView>("pageviews");
				pageViews.Insert(page);
			}
		}

	}
}
