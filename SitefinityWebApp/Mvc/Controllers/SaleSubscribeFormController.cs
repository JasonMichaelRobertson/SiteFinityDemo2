using System;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using SitefinityWebApp.Mvc.Models;
using System.Net.Mail;
using SitefinityWebApp.Utilities;

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "SaleSubscribeForm", Title = "SaleSubscribeForm", SectionName = "MVCWidgets"), Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.WidgetDesigners.SaleSubscribeForm.SaleSubscribeFormDesigner))]
    public class SaleSubscribeFormController : Controller
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        [Category("String Properties")]
        public string Message { get; set; }

        /// <summary>
        /// This is the default Action.
        /// </summary>
        [HttpGet]
        public ActionResult Index()
        {
            var model = new SaleSubscribeFormModel();
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
        public ActionResult Index(SaleSubscribeFormModel model)
        {

            HtmlHelper.ClientValidationEnabled = true;
            HtmlHelper.UnobtrusiveJavaScriptEnabled = true;
            
            if (ModelState.IsValid)
            {
                model.Message = "Enquiry submitted, email has been sent.";

                StringBuilder m_body = new StringBuilder();
                m_body.AppendLine(@"First Name : " + model.FirstName);
                m_body.AppendLine(@"Last Name : " + model.LastName);
                m_body.AppendLine(@"Email : " + model.EmailAddress);

                // loop through model and select it into the subject those selected by Attribute name
                // could use Member Info and Display Attribute but reflection is slow just loop and use string values

                string lnBrk = "\n\r";
                string industries = lnBrk;

                if (model.All)
                {
                    industries += " All" + lnBrk;
                }
                
                if (model.Agribusiness)
                {
                    industries += " Agribusiness" + lnBrk;
                }

                if (model.Aviation)
                {
                    industries += " Aviation" + lnBrk;
                }

                if (model.BuildingConstruction)
                {
                    industries += " Building & Construction" + lnBrk;
                }
                if (model.EducationTraining)
                {
                    industries += " Education & Training" + lnBrk;
                }
                if (model.FinanceBusinessServices)
                {
                    industries += " Finance & Business Services" + lnBrk;
                }
                if (model.FoodBeverage)
                {
                    industries += " Food & Beverage" + lnBrk;
                }
                if (model.HealthAgedCare)
                {
                    industries += " Health & Aged Care" + lnBrk;
                }
                if (model.HospitalityLeisure)
                {
                    industries += " Hospitality & Leisure" + lnBrk;
                }
                if (model.InformationTechnology)
                {
                    industries += " Information Technology" + lnBrk;
                }
                if (model.InfrastructureUtilities)
                {
                    industries += " Infrastructure & Utilities" + lnBrk;
                }
                if (model.Manufacturing)
                {
                    industries += " Manufacturing" + lnBrk;
                }
                if (model.Media)
                {
                    industries += " Media" + lnBrk;
                }
                if (model.MiningResources)
                {
                    industries += " Minin & Resources" + lnBrk;
                }
                if (model.MotorVehicle)
                {
                    industries += " Motor Vehicle" + lnBrk;
                }
                if (model.PrinitngPublishing)
                {
                    industries += " Prinitng & Publishing" + lnBrk;
                }
                if (model.RealEstate)
                {
                    industries += " RealEstate" + lnBrk;
                }
                if (model.RetailConsumer)
                {
                    industries += " Retail & Consumer" + lnBrk;
                }
                if (model.TextileClothingFootwear)
                {
                    industries += " Textile, Clothing & Footwear" + lnBrk;
                }
                if (model.TransportLogistics)
                {
                    industries += " Transport & Logistics" + lnBrk;
                }

                model.ConfirmSelectedIndustries = industries;

                m_body.AppendLine(@"Request for business sale notifications : " + industries);

                MailAddress m_sender = new MailAddress(Settings.ASSenderEmail(), Settings.SubscribeName());
                MailAddress m_from = new MailAddress(Settings.ASFromEmail(), Settings.SubscribeName());

                MailAddress m_recipient_km = new MailAddress(Settings.ASRecipientEmail(), Settings.SubscribeName());
                MailAddress m_recipient_client = new MailAddress(model.EmailAddress, model.FirstName + (model.LastName.Trim() == "" ? "" : " " + model.LastName));

                MailMessage m_message = new MailMessage();
                m_message.Subject = Settings.SubscribeSubjectAssetSale();
                m_message.Priority = MailPriority.High;
                m_message.Sender = m_sender;
                m_message.From = m_from;
                m_message.To.Add(m_recipient_client);
                m_message.Bcc.Add(m_recipient_km);
                m_message.Body = m_body.ToString();
                m_message.IsBodyHtml = false;

                // model.Message = Email.SendEmail(m_message) ? "Your enquiry has been sent" : "Failed to send your enquiry, please try again later";

              return View("Confirm", model);
            }
            else
            {
                return View("Default", model);
            }
        }
    }
}