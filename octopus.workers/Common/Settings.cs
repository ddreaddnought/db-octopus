using System.Collections.Generic;

namespace octopus.workers.Common
{
	/// <summary>
	/// Worker settings collection
	/// </summary>
	public class Settings
	{
		/// <summary>
		/// Settings storage
		/// </summary>
		private Dictionary<string, string> _settings;

		/// <summary>
		/// ctor
		/// </summary>
		public Settings()
		{
			_settings = new Dictionary<string, string>();
		}

		/// <summary>
		/// Sets setting value
		/// </summary>
		public void SetValue(string key, string value)
		{
			if (!_settings.ContainsKey(key))
				_settings.Add(key, value);
			else
				_settings[key] = value;
		}

		/// <summary>
		/// Gets setting value or empty string if settings does not exists
		/// </summary>
		public string GetValue(string key)
		{
			if (_settings.ContainsKey(key))
				return _settings[key];
			else
				return string.Empty;
		}
	}
}
