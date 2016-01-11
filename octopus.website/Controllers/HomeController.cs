using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace octopus.website.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }

		[HttpPost]
		public IActionResult Execute()
		{
			Response.Headers.Add("Refresh", new string[] { "5" });
			return View();
		}

		public IActionResult Execute(string id)
		{
			Response.Headers.Add("Refresh", new string[] { "5" });
			return View();
		}
	}
}
