using octopus.DAL;
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
			UserId = baseModel.UserId;
			DateStart = baseModel.DateStart;
			DateEnd = baseModel.DateEnd;

			Done = DateEnd.HasValue && DateEnd > DateStart;

			
			var runningTime = Done
				? DateEnd.Value - DateStart
				: DateTime.Now - DateStart;

			RunningTime = runningTime.ToString(@"dd\.hh\:mm\:ss");
        }

		public bool Done { get; private set; }
		public string RunningTime { get; private set; }
	}
}
