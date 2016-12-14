using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SitefinityWebApp.Utilities
{
    public class CultureLocation
    {

        public static void SetUserLocale(string CurrencySymbol, bool SetUiCulture)
        {
            HttpRequest Request = HttpContext.Current.Request;
            if (Request.UserLanguages == null)
                return;

            string Lang = Request.UserLanguages[0];
            if (Lang != null)
            {
                // *** Problems with Turkish Locale and upper/lower case
                // *** DataRow/DataTable indexes
                if (Lang.StartsWith("tr"))
                    return;

                if (Lang.Length < 3)
                    Lang = Lang + "-" + Lang.ToUpper();
                try
                {
                    System.Globalization.CultureInfo Culture = new System.Globalization.CultureInfo(Lang);
                    if (CurrencySymbol != null && CurrencySymbol != "")
                        Culture.NumberFormat.CurrencySymbol = CurrencySymbol;

                    System.Threading.Thread.CurrentThread.CurrentCulture = Culture;

                    if (SetUiCulture)
                        System.Threading.Thread.CurrentThread.CurrentUICulture = Culture;
                }
                catch
                { ;}
            }
        }


        public static string StripHTMLTag(string text)
        {
            string s = Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
            s = s.Replace("&nbsp;", " ");
            s = Regex.Replace(s, @"\s+", " ");
            s = Regex.Replace(s, @"\r\n", "\n");
            s = Regex.Replace(s, @"\n+", "\n");
            return s;
        }

    }   
}