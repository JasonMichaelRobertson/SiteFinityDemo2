using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Frontend;
using MyFirstMvcWidget.Mvc.Models;

namespace MyFirstMvcWidget.Mvc.Controllers

{

    [ControllerToolboxItem(Name = "MessageWidget", Title = "MessageWidget", SectionName = "MvcWidgets")]
    public class MessageWidgetController : Controller
    {

        /// <summary> 
        /// Gets or sets the message. 
        /// </summary>

       // [Category("String Properties")]
        public string Message { get; set; }

        /// <summary> 
        /// This is the default Action. 
        /// </summary> 

        public ActionResult Index()
        {

            var model = new MessageWidgetModel();

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
