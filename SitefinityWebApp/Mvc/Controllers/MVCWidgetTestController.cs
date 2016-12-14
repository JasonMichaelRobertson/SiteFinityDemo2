using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Telerik.Sitefinity.Mvc;
using SitefinityWebApp.Mvc.Models;

namespace SitefinityWebApp.Mvc.Controllers
{
    [ControllerToolboxItem(Name = "MVCWidgetTest", Title = "MVCWidgetTest", SectionName = "MVCWidgets")]
    [Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesigner(typeof(SitefinityWebApp.WidgetDesigners.MVCWidgetTest.MVCWidgetTestDesigner))]
    public class MVCWidgetTestController : Controller
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        [Category("String Properties")]
        public string Message { get; set; }

        [Category("General")]
        public string HeaderText
        {

            get;

            set;

        }

        /// <summary>
        /// This is the default Action.
        /// </summary>
        public ActionResult Index()
        {
            ViewData["HeaderText"] = this.HeaderText;
            
            var model = new MVCWidgetTestModel();
            if (string.IsNullOrEmpty(this.Message))
            {
                model.Message = "Hello, World!";
            }
            else
            {
                model.Message = this.Message;
            }

            return View("Default", model);
        }
    }
}