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
        public ActionResult Execute(QueryViewModel query)
        {
			SqlQuery sqlQuery = new SqlQuery()
			{
				Id = query.Id,
				UserId = UserInfo.UserId(User.Identity),
				DateStart = DateTime.Now
			};

			_dbContext.Queries.Add(sqlQuery);
			_dbContext.SaveChanges();

			return Redirect(string.Format("~/Query/Execute/{0}", query.Id));
        }

		public ActionResult Execute(string id)
		{
			var model = _dbContext.Queries.Where(q => q.Id == id).FirstOrDefault();
			var viewModel = new RunningQueryViewModel(model);

			if(!viewModel.Done)
				HttpContext.Response.AddHeader("refresh", "5");
			return View(viewModel);
		}
	}
}