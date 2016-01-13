using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace octopus.Data
{
	[Table("user")]
	public class EntityUser
	{
		[Key]
		[Column("id")]
		public int Id { get; set; }

		[Column("name")]
		public string Name { get; set; }

		[Column("password")]
		public string Password { get; set; }
	}
}
