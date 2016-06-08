using octopus.workers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace octopus.workers
{
	/// <summary>
	/// Describes single octopus connection. It may contains several real data-source connections
	/// </summary>
	public interface IOctopusConnection
	{
		/// <summary>
		/// Connection settings
		/// </summary>
		Settings Settings { get; set; }

		/// <summary>
		/// Obtains list of connection mandatory settings
		/// </summary>
		string[] ListMandatorySettings();

		/// <summary>
		/// Obtains list of connection supported settings
		/// </summary>
		string[] ListSupportedSettings();

		/// <summary>
		/// Tries to open connection and returns true is succeeded
		/// </summary>
		bool Check();
	}
}
