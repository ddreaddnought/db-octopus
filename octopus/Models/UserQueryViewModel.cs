using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace octopus.Models
{
	public class UserQueryViewModel
	{
		public UserQueryViewModel()
		{
			Params = new List<UserQueryParamModel>();
		}

		[Required]
		public string Databases { get; set; }
		public string Sql { get; set; }
		public int PreparedScriptId { get; set; }
		public List<UserQueryParamModel> Params { get; set; }
		public bool SingleTable { get; set; }
	}
}