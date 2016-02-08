using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace octopus.DAL.WorkerEntities
{
	/// <summary>
	/// Worker result class -- stores column headers and rows with values
	/// </summary>
	[JsonObject]
	public class WorkerResult
	{
		/// <summary>
		/// Database id
		/// </summary>
		public string DatabaseId { get; set; }

		/// <summary>
		/// ctor
		/// </summary>
		public WorkerResult()
		{
			Columns = new List<string>();
			Rows = new List<object[]>();
		}

		/// <summary>
		/// Column headers
		/// </summary>
		public List<string> Columns { get; private set; }

		/// <summary>
		/// Rows with data
		/// </summary>
		public List<object[]> Rows { get; private set; }

		/// <summary>
		/// Append new column
		/// </summary>
		public void AppendColumn(string name)
		{
			Columns.Add(name);
		}

		/// <summary>
		/// Append new row
		/// </summary>
		public void AppendRow(object[] row)
		{
			Rows.Add(row);
		}
	}
}
