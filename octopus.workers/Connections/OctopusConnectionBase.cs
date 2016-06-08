using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using octopus.workers.Common;

namespace octopus.workers
{
	public class OctopusConnectionBase : IOctopusConnection
	{
		/// <summary>
		/// Connection settings
		/// </summary>
		public Settings Settings { get; set; }

		/// <summary>
		/// ctor
		/// </summary>
		public OctopusConnectionBase()
		{
			Settings = new Settings();
		}

		/// <summary>
		/// Obtains list of connection mandatory settings
		/// </summary>
		public virtual string[] ListMandatorySettings()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Obtains list of connection supported settings
		/// </summary>
		public virtual string[] ListSupportedSettings()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Tries to open connection and returns true is succeeded
		/// </summary>
		public virtual bool Check()
		{
			throw new NotImplementedException();
		}
	}
}
