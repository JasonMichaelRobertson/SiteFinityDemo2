using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace SitefinityWebApp.Utilities
{
    public static class Settings
    {
        public static string SMTPServerName()
        {
            return ConfigurationManager.AppSettings["smtpservername"].ToString();
        }

        public static int SMTPPortNo()
        {
            return System.Convert.ToInt32(ConfigurationManager.AppSettings["smtpportno"].ToString());
        }
                            
        public static string SubscribeFormSenderEmail()
        {
            return ConfigurationManager.AppSettings["SubscribeFormSenderEmail"].ToString();
        }

        public static string SubscribeFormFromEmail()
        {
            return ConfigurationManager.AppSettings["SubscribeFormFromEmail"].ToString();
        }

        public static string SubscribeFormRecipientEmail()
        {
            return ConfigurationManager.AppSettings["SubscribeFormRecipientEmail"].ToString();
        }

       
        // IM Form Sender Email Address
        public static string IMSenderEmail()
        {
            return ConfigurationManager.AppSettings["IMSenderEmail"].ToString();
        }

        // IM Form From Email Address
        public static string IMFromEmail()
        {
            return ConfigurationManager.AppSettings["IMFromEmail"].ToString();
        }

        // IM Form Recipient (company internal -  marketing) Email Address
        public static string IMRecipientEmail()
        {
            return ConfigurationManager.AppSettings["IMRecipientEmail"].ToString();
        }

        public static string ASSenderEmail()
        {
            return ConfigurationManager.AppSettings["ASSenderEmail"].ToString();
        }

        public static string ASFromEmail()
        {
            return ConfigurationManager.AppSettings["ASFromEmail"].ToString();
        }

        public static string ASRecipientEmail()
        {
            return ConfigurationManager.AppSettings["ASRecipientEmail"].ToString();
        }


        public static string SubscribeName()
        {
            return ConfigurationManager.AppSettings["subscribename"].ToString();
        }

        public static string IMSubscribeName()
        {
            return ConfigurationManager.AppSettings["IMsubscribename"].ToString();
        }

        public static string SubscribeSubject()
        {
            return ConfigurationManager.AppSettings["subscribesubject"].ToString();
        }

        public static string IMSubscribeSubjectIM()
        {
            return ConfigurationManager.AppSettings["IMsubscribesubject"].ToString();
        }

        // subscribesubjectAssetSale

        public static string SubscribeSubjectAssetSale()
        {
            return ConfigurationManager.AppSettings["subscribesubjectAssetSale"].ToString();
        }

        public static string GetKordaMenthaLink()
        {
            return ConfigurationManager.AppSettings["linkKordaMentha"].ToString();
        }

        public static string GetKM333()
        {
            return ConfigurationManager.AppSettings["linkKM333"].ToString();
        }

        public static string GetAlixPartners()
        {
            return ConfigurationManager.AppSettings["linkAlixPartners"].ToString();
        }
    }
}