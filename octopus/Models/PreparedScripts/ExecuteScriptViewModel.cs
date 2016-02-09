using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace octopus.Models
{
	public class ExecuteScriptViewModel
	{
		public ExecuteScriptViewModel()
		{
			Params = new List<PreparedScriptParameter>();
        }

		public string Name { get; set; }
		public string Databases { get; set; }
		public List<PreparedScriptParameter> Params { get; private set; }
	}
}
