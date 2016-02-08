using Newtonsoft.Json;
using octopus.DAL.WorkerEntities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace octopus.DAL.WorkerHelpers
{
	/// <summary>
	/// Query runner class -- implements all executing logic
	/// </summary>
	class QueryRunner
	{
		/// <summary>
		/// Countdown counter for execution. Stars from database count, decrements on finish.
		/// </summary>
		private int _countdown;

		/// <summary>
		/// Databases to execute query
		/// </summary>
		private List<WorkerDb> _databases;

		/// <summary>
		/// Collection to store query results
		/// </summary>
		private ConcurrentBag<WorkerResult> _results;

		/// <summary>
		/// Sql query to execute
		/// </summary>
		private SqlQuery _query;

		/// <summary>
		/// ctor
		/// </summary>
		public QueryRunner(SqlQuery query, IEnumerable<WorkerDb> databases)
		{
			_query = query;
			_results = new ConcurrentBag<WorkerResult>();
			_databases = new List<WorkerDb>();
			_databases.AddRange(databases);
		}

		/// <summary>
		/// Run query execution inside separated thread
		/// </summary>
		public void ExecuteQuery()
		{
			Thread thread = new Thread(ExecutionThread);
			thread.Start();
		}

		/// <summary>
		/// Internal thread execution method
		/// </summary>
		private void ExecutionThread()
		{
			_countdown = _databases.Count;
            foreach (var db in _databases)
			{
				Thread databaseThread = new Thread(DatabaseThread);
				databaseThread.Start(db);
			}

			while (_countdown > 0)
			{
				Thread.Sleep(500);
			}

			using (var dbContext = new OctopusDbContext())
			{
				var queryFromContext = dbContext
					.Queries
					.Where(q => q.Id == _query.Id)
					.First();
				queryFromContext.DateEnd = DateTime.Now;

				dbContext.SaveChanges();
            }
		}

		/// <summary>
		/// A thread for executing query within each database
		/// </summary>
		private void DatabaseThread(object db)
		{
			var database = (WorkerDb)db;
			var adapter = new SqlDataAdapter(_query.Sql, database.ConnectionString);
			var table = new DataTable();
			adapter.Fill(table);

			var result = new WorkerResult();
			result.DatabaseId = database.Id;

			foreach (DataColumn column in table.Columns)
				result.AppendColumn(column.Caption);

			foreach (DataRow row in table.Rows)
				result.AppendRow(row.ItemArray);

			_results.Add(result);

			var queryResult = new SqlQueryResult();
			queryResult.QueryId = _query.Id;
			queryResult.DatabaseId = database.Id;
			queryResult.Result = JsonConvert.SerializeObject(result);

			using (var dbContext = new OctopusDbContext())
			{
				dbContext.QueryResults.Add(queryResult);
				dbContext.SaveChanges();
			}

			Interlocked.Decrement(ref _countdown);
		}
	}
}
