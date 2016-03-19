using octopus.DAL;
using octopus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace octopus.Helpers
{
	/// <summary>
	/// Helper for prepared scripts
	/// </summary>
	public static class PreparedScriptHelper
	{
		/// <summary>
		/// Get prepared script for View
		/// </summary>
		/// <returns></returns>
		public static List<SelectListItem> GetScripts()
		{
			var dbContext = new OctopusDbContext();

			var result = new List<SelectListItem>();
			result.Add(new SelectListItem() { Value = "0", Text = "( none )" });

			result.AddRange(dbContext
				.PreparedScripts
				.Select(s => new SelectListItem()
				{
					Text = s.Name,
					Value = s.Id.ToString()
				}));

			return result;
		}

		/// <summary>
		/// Get script from script repository by id
		/// </summary>
		public static PreparedScript Get(int id)
		{
			var dbContext = new OctopusDbContext();

			return dbContext
				.PreparedScripts
				.Where(s => s.Id == id)
				.FirstOrDefault();
		}

		/// <summary>
		/// Extract params from sql script
		/// </summary>
		public static List<UserQueryParamModel> PrepareParams(string sql)
		{
			var matches = Regex
				.Matches(sql, @"\{.*?\}", RegexOptions.Multiline)
				.Cast<Match>();

			return matches
				.Select(x => new UserQueryParamModel() { Name = x.Value.Trim(new char[] { '{', '}' }) })
				.ToList();
		}

		/// <summary>
		/// Create sql query using param values
		/// </summary>
		public static string SetParams(string sql, List<UserQueryParamModel> param)
		{
			var sqlWithParams = sql;
			for(int i = 0; i < param.Count; i++)
			{
				double fake = 0;
				string paramValue = string.Empty;
				if (Double.TryParse(param[i].Value, out fake))
					paramValue = param[i].Value;
				else
					paramValue = string.Format("'{0}'", param[i].Value.Replace("'", "''"));
				sqlWithParams = sqlWithParams.Replace(string.Format("{{{0}}}", param[i].Name), paramValue);
			}
			return sqlWithParams;
		}
	}
}