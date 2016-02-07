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
    public class ScriptController : Controller
    {
		private OctopusDbContext _dbContext = new OctopusDbContext();

        public ActionResult Index()
        {
			int userId = UserInfo.UserId(User.Identity);
			var scripts = _dbContext
				.PreparedScripts
				.Where(s => s.UserId == userId);
            return View(scripts);
        }

        public ActionResult Create()
        {
            return RedirectToAction("Edit");
        }

        [HttpPost]
        public ActionResult Edit(PreparedScript script)
        {
            try
            {
				if (script.Id > 0)
				{
					var oldScript = _dbContext
						.PreparedScripts
						.Where(s => s.Id == script.Id)
						.First();

					oldScript.Name = script.Name;
					oldScript.Sql = script.Sql;
				}
				else
				{
					script.UserId = UserInfo.UserId(User.Identity);
					_dbContext.PreparedScripts.Add(script);
				}

				_dbContext.SaveChanges();
				return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult Edit(int? id)
        {
			var script = _dbContext
				.PreparedScripts
				.Where(s => s.Id == id)
				.FirstOrDefault();

            return View(script);
        }

        
        public ActionResult Delete(int id)
        {
			var script = _dbContext
				.PreparedScripts
				.Where(s => s.Id == id)
				.FirstOrDefault();

			return View(script);
        }

		
		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection)
		{
			var script = _dbContext
				.PreparedScripts
				.Where(s => s.Id == id)
				.FirstOrDefault();
			_dbContext.PreparedScripts.Remove(script);
			_dbContext.SaveChanges();

			return RedirectToAction("Index");
		}

		public ActionResult Execute(int id)
		{
			var script = _dbContext
				.PreparedScripts
				.Where(s => s.Id == id)
				.FirstOrDefault();

			var executeScript = new ExecuteScriptViewModel();
			executeScript.Name = script.Name;
			executeScript.Sql = script.Sql;

			return View("Execute", executeScript);
		}
    }
}
