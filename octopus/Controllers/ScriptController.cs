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
	/// <summary>
	/// Controller for scripts prepared by user
	/// </summary>
	[Authorize]
	public class ScriptController : Controller
	{
		/// <summary>
		/// DbContext
		/// </summary>
		private OctopusDbContext _dbContext = new OctopusDbContext();

		/// <summary>
		/// Get script by id from database
		/// </summary>
		private PreparedScript GetScriptById(int id)
		{
			var script = _dbContext
				.PreparedScripts
				.Where(s => s.Id == id)
				.FirstOrDefault();
			return script;
        }

		/// <summary>
		/// List all scripts owned by user
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
        {
			int userId = UserInfo.UserId(User.Identity);
			var scripts = _dbContext
				.PreparedScripts
				.Where(s => s.UserId == userId);

            return View(scripts);
        }

		/// <summary>
		/// Create new script -- open Edit action without Id
		/// </summary>
		/// <returns></returns>
        public ActionResult Create()
        {
            return RedirectToAction("Edit");
        }

		/// <summary>
		/// Modify existing script or create new
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Edit(PreparedScript script)
		{
			if (script.Id > 0)
			{
				var oldScript = GetScriptById(script.Id);

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

        /// <summary>
		/// Open create/edit page for script
		/// </summary>
        public ActionResult Edit(int? id)
        {
			PreparedScript script = null;
			if(id.HasValue)
				script = GetScriptById(id.Value);

			return View(script);
        }

        /// <summary>
		/// Open script delete page
		/// </summary>
        public ActionResult Delete(int id)
        {
			var script = GetScriptById(id);

			return View(script);
        }

		
		/// <summary>
		/// Delete existing script
		/// </summary>
		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection)
		{
			var script = GetScriptById(id);
			_dbContext.PreparedScripts.Remove(script);
			_dbContext.SaveChanges();

			return RedirectToAction("Index");
		}

		/// <summary>
		/// Open script execution page
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult Execute(int id)
		{
			var script = GetScriptById(id);

			var executeScript = new ExecuteScriptViewModel();
			executeScript.Name = script.Name;
			executeScript.Sql = script.Sql;

			return View("Execute", executeScript);
		}
    }
}
