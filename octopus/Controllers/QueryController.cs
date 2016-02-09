using octopus.DAL;
using octopus.Helpers;
using octopus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace octopus.Controllers
{
	[Authorize]
    public class QueryController : Controller
    {
		private OctopusDbContext _dbContext = new OctopusDbContext();

		[HttpPost]
        public ActionResult Execute(SqlQuery query)
        {
			query.UserId = UserHelper.GetUserId(User.Identity);
			query.DateStart = DateTime.Now;
			_dbContext.Queries.Add(query);
			_dbContext.SaveChanges();

			Worker.ExecuteQuery(query);

			return Redirect(string.Format("~/Query/Execute/{0}", query.Id));
        }

		public ActionResult Execute(int id)
		{
			var model = _dbContext
				.Queries
				.Where(q => q.Id == id)
				.FirstOrDefault();
			var viewModel = new RunningQueryViewModel(model);

			if(!viewModel.Done)
				HttpContext.Response.AddHeader("refresh", "2");
			return View(viewModel);
		}
	}
}