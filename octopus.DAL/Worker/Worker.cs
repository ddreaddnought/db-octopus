using octopus.DAL.WorkerEntities;
using octopus.DAL.WorkerHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace octopus.DAL
{
	/// <summary>
	/// Worker class -- a single entry point connected to all datasources and databases
	/// </summary>
	public static class Worker
	{
		/// <summary>
		/// QueryRunner collection locker
		/// </summary>
		private static object _locker;

		/// <summary>
		/// All databases which can be accessed by worker
		/// </summary>
		private static List<WorkerDb> _databases;

		/// <summary>
		/// All active QueryRunners
		/// </summary>
		private static List<QueryRunner> _runners;

		/// <summary>
		/// ctor -- init data sources collection
		/// </summary>
		static Worker()
		{
			_locker = new object();
			_runners = new List<QueryRunner>();
			_databases = new List<WorkerDb>();
            using (var dbContext = new OctopusDbContext())
			{
				var dataSources = dbContext.DataSources.ToArray();
				foreach (var source in dataSources)
				{
					_databases.AddRange(InitHelper.LoadDatabases(source));
                }
			}
		}

		/// <summary>
		/// Execute query
		/// </summary>
		public static void ExecuteQuery(SqlQuery query)
		{
			var databaseIds = query
				.Databases
				.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(id => id
					.Trim()
					.ToLower())
				.ToList();

			bool isAllDatabases = databaseIds.Count == 1 && databaseIds[0] == "all";
			List<WorkerDb> executeInDatabases = (isAllDatabases)
				? _databases.ToList()
				: new List<WorkerDb>();

			if (!isAllDatabases)
			{
				foreach (var id in databaseIds)
				{
					var database = _databases
						.Where(db => db.Id == id)
						.FirstOrDefault();

					if (database != null)
						executeInDatabases.Add(database);
				}
			}

			QueryRunner runner = new QueryRunner(query, executeInDatabases);
			runner.ExecuteQuery();
		}
	}
}
