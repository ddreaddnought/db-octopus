using Newtonsoft.Json;
using octopus.DAL;
using octopus.DAL.WorkerEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace octopus.Models
{
	public class RunningQueryViewModel : SqlQuery
	{
		public RunningQueryViewModel(SqlQuery baseModel)
		{
			Id = baseModel.Id;
			Sql = baseModel.Sql;
			Databases = baseModel.Databases;
			UserId = baseModel.UserId;
			DateStart = baseModel.DateStart;
			DateEnd = baseModel.DateEnd;
			SingleTable = baseModel.SingleTable;

			Done = DateEnd.HasValue && DateEnd > DateStart;

			
			var runningTime = Done
				? DateEnd.Value - DateStart
				: DateTime.Now - DateStart;

			RunningTime = runningTime.ToString(@"dd\.hh\:mm\:ss");

			Results = new List<WorkerResult>();
			using (var dbContext = new OctopusDbContext())
			{
				var tmpResults = dbContext
					.QueryResults
					.Where(qr => qr.QueryId == Id)
					.ToList();
                foreach (var tmpResult in tmpResults)
					Results.Add(JsonConvert.DeserializeObject<WorkerResult>(tmpResult.Result));
			}
		}

		public bool Done { get; private set; }
		public string RunningTime { get; private set; }
		public List<WorkerResult> Results { get; private set; }
	}
}
