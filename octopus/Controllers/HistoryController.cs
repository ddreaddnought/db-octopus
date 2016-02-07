using octopus.DAL;
using octopus.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace octopus.Controllers
{
	[Authorize]
    public class HistoryController : Controller
    {
        // GET: History
        public ActionResult Index()
        {
			var dbContext = new OctopusDbContext();

			int userId = UserInfo.UserId(User.Identity);

            return View(dbContext
				.Queries
				.Where(q => q.UserId == userId)
				.OrderByDescending(q => q.DateStart)
			);
        }
    }
}
