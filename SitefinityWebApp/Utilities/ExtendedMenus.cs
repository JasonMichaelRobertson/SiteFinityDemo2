using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Pages.Model;
using System.Web.UI.HtmlControls;
using System.Collections;

namespace SitefinityWebApp.Utilities
{
	public enum ExtendedMenuType
	{
		Corporate_Recovery = 0,
		Turnaround_and_Restructuring = 1,
		Forensics = 2,
		Real_Estate_Advisory = 3,
        Investment_Management = 4,
		War_Stories = 5,
		Major_Engagements = 6,
        //Client_Sectors = 6,
		Our_People = 7,
		Creditor_Information = 8,
		About = 9
	}

	public static class ExtendedMenus
	{
		private static readonly Dictionary<string, string> _industries;
		private static readonly Dictionary<string, string> _locations;

		static ExtendedMenus()
		{
			_industries = new Dictionary<string, string>();
			//_industries.Add("0", "Accommodation, Cafes and Restaurants");
			//_industries.Add("20", "General");
			//_industries.Add("1", "Agriculture, Forestry and Fishing");
			//_industries.Add("2", "Automotive");
			//_industries.Add("3", "Communication");
			//_industries.Add("4", "Construction");
			//_industries.Add("5", "Cultural and Recreational Services");
			//_industries.Add("6", "Education");
			//_industries.Add("7", "Electricity, Gas and Water Supply");
			//_industries.Add("8", "Environmental");
			//_industries.Add("9", "Finance and Insurance");
			//_industries.Add("10", "Health and Aged Care");
			//_industries.Add("11", "Information Technology");
			//_industries.Add("12", "Manufacturing");
			//_industries.Add("13", "Mining");
			//_industries.Add("14", "Personal and Other Services");
			//_industries.Add("15", "Pharmaceutical");
			//_industries.Add("16", "Property and Business Services");
			//_industries.Add("17", "Retail Trade");
			//_industries.Add("18", "Transport and Storage");
			//_industries.Add("19", "Wholesaling");

			_locations = new Dictionary<string, string>();
			_locations.Add("Melbourne", "Melbourne");
			_locations.Add("Sydney", "Sydney");
			_locations.Add("Perth", "Perth");
			_locations.Add("Brisbane", "Brisbane");
			_locations.Add("Gold Coast", "Gold Coast");
			_locations.Add("Townsville", "Townsville");
			_locations.Add("Adelaide", "Adelaide");
			_locations.Add("New Zealand", "New Zealand");
			_locations.Add("Singapore", "Singapore");
		}

		public static ICollection GetExtendedMenus(ExtendedMenuType p_menutype)
		{

			ICollection m_return = null;
			switch (p_menutype)
			{
				case ExtendedMenuType.Corporate_Recovery:
					var q_menu0 = App.WorkWith().Pages().LocatedIn(Telerik.Sitefinity.Fluent.Pages.PageLocation.Frontend).Where(w => w.WasPublished == true && w.ApprovalWorkflowState == "Published" && w.Parent.Title.Trim().ToLower() == "Restructuring" && w.ShowInNavigation).OrderBy(w => w.Ordinal).Get();
					m_return = q_menu0.Count() == 0 ? null : q_menu0.ToList();
					break;
                //case ExtendedMenuType.Turnaround_and_Restructuring:
                //    var q_menu1 = App.WorkWith().Pages().LocatedIn(Telerik.Sitefinity.Fluent.Pages.PageLocation.Frontend).Where(w => w.WasPublished == true && w.ApprovalWorkflowState == "Published" && w.Parent.Title.Trim().ToLower() == "turnaround & restructuring" && w.ShowInNavigation).OrderBy(w => w.Ordinal).Get();
                //    m_return = q_menu1.Count() == 0 ? null : q_menu1.ToList();
                //    break;
				case ExtendedMenuType.Forensics:
					var q_menu2 = App.WorkWith().Pages().LocatedIn(Telerik.Sitefinity.Fluent.Pages.PageLocation.Frontend).Where(w => w.WasPublished == true && w.ApprovalWorkflowState == "Published" && w.Parent.Title.Trim().ToLower() == "forensic" && w.ShowInNavigation).OrderBy(w => w.Ordinal).Get();
					m_return = q_menu2.Count() == 0 ? null : q_menu2.ToList();
					break;
				case ExtendedMenuType.Real_Estate_Advisory:
					var q_menu3 = App.WorkWith().Pages().LocatedIn(Telerik.Sitefinity.Fluent.Pages.PageLocation.Frontend).Where(w => w.WasPublished == true && w.ApprovalWorkflowState == "Published" && w.Parent.Title.Trim().ToLower() == "real estate" && w.ShowInNavigation).OrderBy(w => w.Ordinal).Get();
					m_return = q_menu3.Count() == 0 ? null : q_menu3.ToList();
					break;
                case ExtendedMenuType.Investment_Management:
                    var q_menu4 = App.WorkWith().Pages().LocatedIn(Telerik.Sitefinity.Fluent.Pages.PageLocation.Frontend).Where(w => w.WasPublished == true && w.ApprovalWorkflowState == "Published" && w.Parent.Title.Trim().ToLower() == "investment management" && w.ShowInNavigation).OrderBy(w => w.Ordinal).Get();
                    m_return = q_menu4.Count() == 0 ? null : q_menu4.ToList();
                    break;
				case ExtendedMenuType.War_Stories:
					var q_menu5 = App.WorkWith().Pages().LocatedIn(Telerik.Sitefinity.Fluent.Pages.PageLocation.Frontend).Where(w => w.WasPublished == true && w.ApprovalWorkflowState == "Published" && w.Parent.Title.Trim().ToLower() == "our stories" && w.ShowInNavigation).OrderBy(w => w.Ordinal).Get();
					m_return = q_menu5.Count() == 0 ? null : q_menu5.ToList();
					break;
				case ExtendedMenuType.Major_Engagements:
					//var q_menu5 = App.WorkWith().Pages().LocatedIn(Telerik.Sitefinity.Fluent.Pages.PageLocation.Frontend).Where(w => w.WasPublished == true && w.ApprovalWorkflowState == "Published" && w.Parent.Title == "Our Engagements" && w.ShowInNavigation).OrderBy(w => w.Ordinal).Get();
					//m_return = q_menu5.Count() == 0 ? null : q_menu5.ToList();
					m_return = _industries;
					break;
                //case ExtendedMenuType.Client_Sectors:
                //    var q_menu6 = App.WorkWith().Pages().LocatedIn(Telerik.Sitefinity.Fluent.Pages.PageLocation.Frontend).Where(w => w.WasPublished == true && w.ApprovalWorkflowState == "Published" && w.Parent.Title.Trim().ToLower() == "client sectors" && w.ShowInNavigation).OrderBy(w => w.Ordinal).Get();
                //    m_return = q_menu6.Count() == 0 ? null : q_menu6.ToList();
                //    break;
				case ExtendedMenuType.Our_People:
					//var q_menu7 = App.WorkWith().Pages().LocatedIn(Telerik.Sitefinity.Fluent.Pages.PageLocation.Frontend).Where(w => w.WasPublished == true && w.ApprovalWorkflowState == "Published" && w.Parent.Title == "Our People" && w.ShowInNavigation).OrderBy(w => w.Ordinal).Get();
					//m_return = q_menu7.Count() == 0 ? null : q_menu7.ToList();
					m_return = _locations;
					break;
				case ExtendedMenuType.Creditor_Information:
                    var q_menu8 = App.WorkWith().Pages().LocatedIn(Telerik.Sitefinity.Fluent.Pages.PageLocation.Frontend).Where(w => w.WasPublished == true && w.ApprovalWorkflowState == "Published" && w.Parent.Title.Trim().ToLower() == "creditor information" && w.ShowInNavigation).OrderBy(w => w.Ordinal).Get();
					m_return = q_menu8.Count() == 0 ? null : q_menu8.ToList();
					break;
				case ExtendedMenuType.About:
                    var q_menu9 = App.WorkWith().Pages().LocatedIn(Telerik.Sitefinity.Fluent.Pages.PageLocation.Frontend).Where(w => w.WasPublished == true && w.ApprovalWorkflowState == "Published" && w.Parent.Title.Trim().ToLower() == "about" && w.ShowInNavigation).OrderBy(w => w.Ordinal).Get();
					m_return = q_menu9.Count() == 0 ? null : q_menu9.ToList();
					break;
			}

			return m_return;
		}
	}
}