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

		public static List<UserQueryParamModel> PrepareParams(string sql)
		{
			var matches = Regex
				.Matches(sql, @"\{.*?\}", RegexOptions.Multiline)
				.Cast<Match>();

			return matches
				.Select(x => new UserQueryParamModel() { Name = x.Value.Trim(new char[] { '{', '}' }) })
				.ToList();
		}

		public static string SetParams(string sql, List<UserQueryParamModel> param)
		{
			var prepared = PrepareParams(sql);
			var sqlWithParams = sql;
			for(int i = 0; i < prepared.Count; i++)
			{
				double fake = 0;
				string paramValue = string.Empty;
				if (Double.TryParse(param[i].Value, out fake))
					paramValue = param[i].Value;
				else
					paramValue = string.Format("'{0}'", param[i].Value.Replace("'", "''"));
				sqlWithParams = sqlWithParams.Replace(string.Format("{{{0}}}", prepared[i].Name), paramValue);
			}
			return sqlWithParams;
		}
	}
}