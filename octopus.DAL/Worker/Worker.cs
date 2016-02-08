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
		/// All databases which can be accessed by worker
		/// </summary>
		private static List<WorkerDb> _databases;

		/// <summary>
		/// ctor -- init data sources collection
		/// </summary>
		static Worker()
		{
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

		public static void ExecuteQuery(SqlQuery query)
		{

		}
	}
}
