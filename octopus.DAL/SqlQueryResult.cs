using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace octopus.DAL
{
	[Table("query_result")]
	public class SqlQueryResult
	{
		[Key]
		[Column("id")]
		public int Id { get; set; }

		[Column("db")]
		public string DatabaseId { get; set; }

		[Column("id_query")]
		public int QueryId { get; set; }

		[Column("result")]
		public string Result { get; set; }
	}
}
