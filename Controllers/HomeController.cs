using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Site.Models;
using Site.Services;

namespace Site.Controllers
{
	public class HomeController : Controller
	{
		private readonly IPageLoadRepository pageLoadRepository;

		public HomeController(IPageLoadRepository pageLoadRepository)
		{
			this.pageLoadRepository = pageLoadRepository ?? throw new ArgumentNullException(nameof(pageLoadRepository));
		}

		public IActionResult Index()
		{
			pageLoadRepository.SavePageView(Request.Path);
			List<PageView> models = pageLoadRepository.GetAllPageViews();
			return View(models ?? new List<PageView>());
		}

	}
}
