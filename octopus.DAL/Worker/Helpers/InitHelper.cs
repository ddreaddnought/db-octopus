using octopus.DAL.WorkerEntities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace octopus.DAL.WorkerHelpers
{
	/// <summary>
	/// Anything we need to initialize worker
	/// </summary>
	static class InitHelper
	{
		/// <summary>
		/// Sql query to get suitable database list
		/// </summary>
		private const string GetSuitableDatabases = "select name from sys.databases where name like @pattern";

		/// <summary>
		/// Get list of DBs which can be accessed by worker
		/// </summary>
		public static List<WorkerDb> LoadDatabases(DataSource source)
		{
			List<WorkerDb> result = new List<WorkerDb>();

			var csBuilder = new SqlConnectionStringBuilder();
			csBuilder.DataSource = source.Address;
			csBuilder.UserID = source.Username;
			csBuilder.Password = source.Password;

			var conn = new SqlConnection(csBuilder.ConnectionString);
			var cmd = new SqlCommand(GetSuitableDatabases, conn);
			cmd.Parameters.AddWithValue("@pattern", source.SearchPattern);
			conn.Open();

			var reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				var name = reader.GetString(0);
				var id = Regex
					.Replace(name, source.NameRegex, string.Empty)
					.ToLower();

				var db = new WorkerDb();
				db.Id = id;
				csBuilder.InitialCatalog = name;
				db.ConnectionString = csBuilder.ConnectionString;

				result.Add(db);
			}

			conn.Close();

			return result;
		}
	}
}
