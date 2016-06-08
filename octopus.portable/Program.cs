using octopus.workers;
using octopus.workers.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace octopus.portable
{
	class Program
	{
		static void Main(string[] args)
		{
			var settings = new Settings();

			IOctopusConnection connection = new ElasticOctopusConnection();
			connection.Settings.SetValue("node", "nl6.jooble.com:9200");
			//connection.Settings.SetValue("login", "writer");
			connection.Settings.SetValue("password", "husq!51Ron2b");

			Console.WriteLine(connection.Check());

			if (Debugger.IsAttached)
				Console.ReadLine();
		}
	}
}
