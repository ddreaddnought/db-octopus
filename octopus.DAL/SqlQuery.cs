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
		public string Id { get; set; }

		[Column("id_user")]
		public int UserId { get; set; }

		[Column("dt_start")]
		public DateTime DateStart { get; set; }

		[Column("dt_end")]
		public DateTime? DateEnd { get; set; }
	}
}
