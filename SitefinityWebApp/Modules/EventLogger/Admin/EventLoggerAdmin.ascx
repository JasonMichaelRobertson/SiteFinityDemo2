<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventLoggerAdmin.ascx.cs" Inherits="SitefinityWebApp.Modules.EventLogger.Admin.EventLoggerAdmin" %>



<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sitefinity" %>
<sitefinity:ResourceLinks ID="ResourceLinks1" runat="server" UseEmbeddedThemes="false">
	<sitefinity:ResourceFile Name="~/Modules/EventLogger/Admin/Admin.css" />
</sitefinity:ResourceLinks>

<h1 class="sfBreadCrumb">Sitefinity Event Log</h1>

<div class="sfMain sfClearfix">
	<div class="sfMain sfClearfix">

		
		
		<div class="sfWorkArea sfClearfix">
		<div class="rgTopOffset">
			<asp:Textbox ID="LogText" runat="server" TextMode="MultiLine" Rows="25" Columns="100" />

			<p>
				<asp:Button ID="btnRefresh" runat="server" Text="Refresh Log" OnClick="btnRefresh_Click" />
				<asp:Button ID="btnClear" runat="server" Text="Clear Log" OnClick="btnClear_Click" OnClientClick="return confirm('Are you sure you wish to clear the log?');" /></p>
		</div>
		</div>
	</div>
</div>