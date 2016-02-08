using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace octopus.DAL
{
	[Table("account")]
	public class Account
	{
		[Key]
		[Column("id")]
		public int Id { get; set; }

		[Column("name")]
		public string Name { get; set; }

		[Column("password")]
		public string Password { get; set; }

		[Column("email")]
		public string Email { get; set; }

		[Column("is_admin")]
		public bool IsAdmin { get; set; }
	}
}
