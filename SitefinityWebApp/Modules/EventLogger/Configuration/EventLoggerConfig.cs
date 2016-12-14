using System;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace SitefinityWebApp.Modules.EventLogger.Configuration
{
	[ObjectInfo(Title = "Event Logger Configuration", Description = "Configuration for the Sitefinity Event Logger")]
	public class EventLoggerConfig : ConfigSection
	{
		[ObjectInfo(Title = "Log File Path", Description = "Relative path to the log file")]
		[ConfigurationProperty("LogFilePath", DefaultValue = "~/App_Data/Sitefinity/Logs/EventLog.log")]
		public string LogFilePath
		{
			get
			{
				return (string)this["LogFilePath"];
			}
			set
			{
				this["LogFilePath"] = value;
			}
		}

		[ObjectInfo(Title = "Enable Logging", Description = "Log all events if selected")]
		[ConfigurationProperty("Enabled", DefaultValue = true)]
		public bool Enabled
		{
			get
			{
				return (bool)this["Enabled"];
			}
			set
			{
				this["Enabled"] = value;
			}
		}
	}
}