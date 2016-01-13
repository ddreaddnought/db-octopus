using Microsoft.AspNet.Mvc;

namespace octopus.website.Controllers
{
	public class HomeController : Controller
    {
        public IActionResult Index()
        {
			Octopus.DAL.Class1 cl;
			//var db = new OctopusDb();
			//db.Users.Add(new EntityUser());
			//db.SaveChanges();
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
