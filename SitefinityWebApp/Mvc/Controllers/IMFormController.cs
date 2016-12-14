using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using SitefinityWebApp.Mvc.Models;
using System.Collections.Generic;
using System.Net.Mail;
using SitefinityWebApp.Utilities;
using System.Text;
using System.Globalization;
using System.IO;
using System.Web.UI;

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "IMForm", Title = "IMForm", SectionName = "MVCWidgets"), Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.WidgetDesigners.IMForm.IMFormDesigner))]
    public class IMFormController : Controller
    {       
        /// <summary>
        /// Gets or sets the message. fgfgfgfgf rrr dff
        /// </summary>
        [Category("String Properties")]
        public string Message { get; set; }

        /// <summary>
        /// This is the default Action.
        /// sdfgsdfg sdfgsdfg gsdfgsdf 
        /// </summary>
        [HttpGet]
        public ActionResult Index()
        {
            // This also requires entry in the web.config
            //
            // Get Browser languages.
            //var userLanguages = Request.UserLanguages;
            //CultureInfo ci;
            //if (userLanguages.Count() > 0)
            //{
            //    try
            //    {
            //        ci = new CultureInfo(userLanguages[0]);
            //    }
            //    catch (CultureNotFoundException)
            //    {
            //        ci = CultureInfo.InvariantCulture;
            //    }
            //}
            //else
            //{
            //    ci = CultureInfo.InvariantCulture;
            //}


            HtmlHelper.ClientValidationEnabled = true;
            HtmlHelper.UnobtrusiveJavaScriptEnabled = true;
            
            var model = new IMFormModel();
            if (string.IsNullOrEmpty(this.Message))
            {
                model.Message = "Register your details";
            }
            else
            {
                model.Message = this.Message;
            }

            return View("Default", model);
        }
               

        [HttpPost]
        public ActionResult Index(IMFormModel model)
        {
            HtmlHelper.ClientValidationEnabled = true;
            HtmlHelper.UnobtrusiveJavaScriptEnabled = true;


            if (ModelState.IsValid)
            {
                model.Message = "Enquiry submitted, email has been sent.";
                string lnBrk = "\n";

                StringBuilder m_body = new StringBuilder();
                m_body.AppendLine(@"First Name : " + model.FirstName);
                m_body.AppendLine(@"Last Name : " + model.LastName);
                m_body.AppendLine(@"Email : " + model.EmailAddress);
                m_body.AppendLine(@"Phone Number : " + model.PhoneNumber);
                m_body.AppendLine(@"Referrer : " + model.Referrer);
                m_body.AppendLine(@"Preferred Language : " + model.DisplaySelectedPreferredLanguage);

                string subscribeToPublications = "";

                if (model.SubscribeToPublications)
                {
                    subscribeToPublications += "Yes";
                }
                else
                {
                    subscribeToPublications += "No";
                }

                model.SubscribeToPublicationsMessage = subscribeToPublications;
                m_body.AppendLine(@"Response to I wish to receive information about our products and services : " + subscribeToPublications);
                m_body.AppendLine(lnBrk);
                m_body.AppendLine(@"SitefinityWebApp Public Web Site - www.SitefinityWebApp.com" + System.Environment.NewLine);
                m_body.AppendLine(@"Investment Management Enquiry Submitted" + System.Environment.NewLine);
                m_body.AppendLine(@"Investment Management Page - www.SitefinityWebApp.com/Investment-Management" + System.Environment.NewLine);

                // sender and from shuould be investmentmanagementform@SitefinityWebApp.com

                MailAddress m_sender = new MailAddress(Settings.IMSenderEmail(), Settings.IMSubscribeName());
                MailAddress m_from = new MailAddress(Settings.IMFromEmail(), Settings.IMSubscribeName());

                MailAddress m_recipient_km = new MailAddress(Settings.IMRecipientEmail(), Settings.IMSubscribeName());
                MailAddress m_recipient_client = new MailAddress(model.EmailAddress, model.FirstName + (model.LastName.Trim() == "" ? "" : " " + model.LastName));

                // send Email to internal SIV enquiries
                MailMessage msg_km = new MailMessage();
                msg_km.IsBodyHtml = false;
                msg_km.Subject = Settings.IMSubscribeSubjectIM();
                // msg_km.Priority = MailPriority.High;
                msg_km.Sender = m_sender;
                msg_km.From = m_from;
                msg_km.To.Add(m_recipient_km);
                msg_km.Body = m_body.ToString();

                // build html view with razor for client
                MailMessage msg_client = new MailMessage();
                msg_client.IsBodyHtml = true;
                msg_client.Subject = Settings.IMSubscribeSubjectIM();
                //  msg_client.Priority = MailPriority.High;
                msg_client.Sender = m_sender;
                msg_client.From = m_from;
                msg_client.To.Add(m_recipient_client);


                // Get html template file
                string appRoot = Server.MapPath("~");
                string filePath = appRoot + "sivenquiries.html";

                string strHtml = System.IO.File.ReadAllText(filePath);
                msg_client.Body = strHtml; //m_body.ToString();     //jr pass the view in here as html                            
                msg_client.IsBodyHtml = true;


                // send to client              
                model.Message = Email.SendEmail(msg_client) ? "Your enquiry has been sent" : "Failed to send your enquiry, please try again later";

                // send to km
                model.Message = Email.SendEmail(msg_km) ? "Your enquiry has been sent" : "Failed to send your enquiry, please try again later";

                return View("Confirm", model);
            }
            else
            {
                return View("Default", model);
            }
           
        }

        private static string MakeHtmlEmailBody()
        {
            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                using (HtmlTextWriter tw = new HtmlTextWriter(sw))
                {
                   // vp.RenderControl(tw);

                    //tw.
                }
            }

            return sb.ToString();
        }

      
    }
}