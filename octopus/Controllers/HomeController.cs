using octopus.DAL;
using octopus.Helpers;
using octopus.Models;
using System.Web.Mvc;

namespace octopus.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Index(UserQueryViewModel model)
		{
			var dbContext = new OctopusDbContext();
			var script = PreparedScriptHelper.Get(model.PreparedScriptId);

			ModelState.Remove("Sql");
			if (script != null)
			{ 
				model.Sql = script.Sql;
				model.Params = PreparedScriptHelper.PrepareParams(script.Sql);
			}
			else
				model.Sql = string.Empty;

			return View(model);
		}
	}
}