using System.IO;
using System.Net;

namespace octopus.workers
{
	public class ElasticOctopusConnection : OctopusConnectionBase
	{
		/// <summary>
		/// Obtains list of connection mandatory settings
		/// </summary>
		public override string[] ListMandatorySettings()
		{
			return new string[]
			{
				"type",
				"node"
			};
		}
		
		/// <summary>
		/// Obtains list of connection supported settings
		/// </summary>
		public override string[] ListSupportedSettings()
		{
			return new string[]
			{
				"type",
				"node",
				"login",
				"password",
				"target-template"
			};
		}

		/// <summary>
		/// Tries to open connection and returns true is succeeded
		/// </summary>
		public override bool Check()
		{
			var node = Settings.GetValue("node");
			if (string.IsNullOrEmpty(node))
				return false;

			if (!node.StartsWith("http://"))
				node = string.Format("http://{0}", node);

			node = string.Format("{0}/_stats/index", node);

			var request = WebRequest.Create(node);

			var login = Settings.GetValue("login");
			var password = Settings.GetValue("password");

			if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
				request.Credentials = new NetworkCredential(login, password);

			var response = request.GetResponse();
			var reader = new StreamReader(response.GetResponseStream());
			var responseContent = reader.ReadToEnd();

			return true;
		}
	}
}
