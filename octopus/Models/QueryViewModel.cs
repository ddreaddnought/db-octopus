using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace octopus.Models
{
	public class QueryViewModel
	{
		public QueryViewModel()
		{
			Id = Guid.NewGuid().ToString();
		}

		public string Id { get; private set; }
		public string Sql { get; set; }
	}
}