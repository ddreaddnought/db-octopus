using System.ComponentModel.DataAnnotations.Schema;

namespace octopus.DAL
{
	/// <summary>
	/// Data source -- an MS SQL server instance which can be accessed by worker
	/// </summary>
	[Table("data_source")]
	public class DataSource
	{
		/// <summary>
		/// MS SQL Server intstance address
		/// </summary>
		[Column("address")]
		public string Address { get; set; }
		
		/// <summary>
		/// SQL Server username
		/// </summary>
		[Column("uid")]
		public string Username { get; set; }

		/// <summary>
		/// SQL Server password
		/// </summary>
		[Column("pwd")]
		public string Password { get; set; }

		/// <summary>
		/// Search-by-name database pattern
		/// </summary>
		[Column("search_pattern")]
		public string SearchPattern { get; set; }

		/// <summary>
		/// Regex to get database short name from full name
		/// </summary>
		[Column("name_regex")]
		public string NameRegex { get; set; }
	}
}
