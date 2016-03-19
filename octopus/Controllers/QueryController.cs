using octopus.DAL;
using octopus.Helpers;
using octopus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace octopus.Controllers
{
	[Authorize]
	public class QueryController : Controller
	{
		private OctopusDbContext _dbContext = new OctopusDbContext();

		[HttpPost]
		public ActionResult Execute(UserQueryViewModel userQuery)
		{
			SqlQuery query = new SqlQuery();
			var preparedScript = PreparedScriptHelper.Get(userQuery.PreparedScriptId);
			if (preparedScript != null)
				query.PreparedScriptName = preparedScript.Name;
			query.Databases = userQuery.Databases;
			query.Sql = PreparedScriptHelper.SetParams(userQuery.Sql, userQuery.Params);

			query.UserId = UserHelper.GetUserId(User.Identity);
			query.DateStart = DateTime.Now;
			query.SingleTable = userQuery.SingleTable;
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

			if (!viewModel.Done)
				HttpContext.Response.AddHeader("refresh", "2");
			return View(viewModel);
		}

		/// <summary>
		/// Return query result as html table, so we can copy it and paste to excel
		/// </summary>
		public ActionResult Raw(int id)
		{
			var model = _dbContext
				.Queries
				.Where(q => q.Id == id)
				.FirstOrDefault();

			var viewModel = new RunningQueryViewModel(model);
			StringBuilder output = new StringBuilder();
			bool hasColumns = false;
			output.Append("<table>");
			foreach (var result in viewModel.Results)
			{
				if (!hasColumns)
				{
					output.Append("<tr>");
					output.Append("<th>country</th>");
					output.Append(
						string.Join(
							string.Empty,
							result
								.Columns
								.Select(col => string.Format("<th>{0}</th>", col))
							)
						);

					output.Append("</tr>");
					hasColumns = true;
				}

				foreach (var row in result.Rows)
				{
					output.Append("<tr>");
					output.Append(string.Format("<td>{0}</td>", result.DatabaseId));

					output.Append(
						string.Join(
							string.Empty,
							row
								.Select(val => string.Format("<td>{0}</td>", val))
							)
						);

					output.Append("</tr>");
				}
			}
			output.Append("</table>");

			Response.ContentType = "text/html";
			return Content(output.ToString());
		}

		/// <summary>
		/// Return query result as .csv file
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public ActionResult Save(int id)
		{
			var model = _dbContext
				.Queries
				.Where(q => q.Id == id)
				.FirstOrDefault();

			var viewModel = new RunningQueryViewModel(model);
			StringBuilder output = new StringBuilder();
			bool hasColumns = false;
			foreach (var result in viewModel.Results)
			{
				if (!hasColumns)
				{
					output.Append("country");
					output.Append("\t");
					output.Append(string.Join("\t", result.Columns));
					output.Append("\r\n");
					hasColumns = true;
				}

				foreach (var row in result.Rows)
				{
					output.Append(result.DatabaseId);
					output.Append("\t");
					output.Append(string.Join("\t", row));
					output.Append("\r\n");
				}
			}

			Response.AddHeader("Content-Disposition", "attachment; filename=query_result.tsv");
			Response.ContentType = "text/tab-separated-values";
			return Content(output.ToString());
		}
	}
}