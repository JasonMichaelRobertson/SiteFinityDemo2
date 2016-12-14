using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using SitefinityWebApp.Modules.EventLogger.Configuration;
using Telerik.Sitefinity.Configuration;
using System.Text;
using System.ComponentModel;
using Telerik.Sitefinity.Services.Events;

namespace SitefinityWebApp.Modules.EventLogger
{
	public static class LoggerHelper
	{
		private static EventLoggerConfig config = Config.Get<EventLoggerConfig>();

		public static void ClearLog()
		{
			// delete log file
			var path = HttpContext.Current.Server.MapPath(config.LogFilePath);
			File.Delete(path);

			// log deletion to new log
			var log = string.Concat("Log file deleted: ", DateTime.Now, "\n");
			WriteLog(log);
		}

		public static void WriteLog(string Text)
		{
			if (!config.Enabled) return;

			var logText = string.Format("{0}:\n{1}\n", DateTime.Now, Text);

			var path = HttpContext.Current.Server.MapPath(config.LogFilePath);
			File.AppendAllText(path, logText);
		}

		public static string ReadLog()
		{
			var path = HttpContext.Current.Server.MapPath(config.LogFilePath);
			if (!File.Exists(path)) return "Log file not found.";

			return File.ReadAllText(path);
		}

		public static string ParseObjectProperties(IEvent EventData)
		{
			var sb = new StringBuilder();
			sb.AppendFormat("Event Data Type: {0}\n", EventData.GetType().ToString());

			// append properties
			foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(EventData))
				sb.AppendFormat("     {0}: {1}\n", descriptor.Name, descriptor.GetValue(EventData));

			return sb.ToString();
		}
	}
}