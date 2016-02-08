using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace octopus.DAL
{
	[Table("query")]
	public class SqlQuery
	{
		[Key]
		[Column("id")]
		public int Id { get; set; }

		[Column("id_user")]
		public int UserId { get; set; }

		[Column("dt_start")]
		public DateTime DateStart { get; set; }

		[Column("dt_end")]
		public DateTime? DateEnd { get; set; }
		
		[Column("databases")]
		public string Databases { get; set; }

		[Column("prepared_script_name")]
		public string PreparedScriptName { get; set; }

		[Column("sql")]
		public string Sql { get; set; }
	}
}
