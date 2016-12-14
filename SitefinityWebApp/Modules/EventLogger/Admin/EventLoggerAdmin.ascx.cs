using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.Modules.EventLogger.Admin
{
	public partial class EventLoggerAdmin : System.Web.UI.UserControl
	{
		public void btnClear_Click(object sender, EventArgs e)
		{
			LoggerHelper.ClearLog();
			LogText.Text = LoggerHelper.ReadLog();
		}

		public void btnRefresh_Click(object sender, EventArgs e)
		{
			LogText.Text = LoggerHelper.ReadLog();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			LogText.Text = LoggerHelper.ReadLog();
		}
	}
}