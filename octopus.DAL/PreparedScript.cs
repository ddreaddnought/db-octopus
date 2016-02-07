using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace octopus.DAL
{
	[Table("prepared_script")]
	public class PreparedScript
	{
		[Key]
		[Column("id")]
		public int Id { get; set; }

		[Column("id_user")]
		public int UserId { get; set; }

		[Column("name")]
		public string Name { get; set; }

		[Column("sql")]
		public string Sql { get; set; }
	}
}
