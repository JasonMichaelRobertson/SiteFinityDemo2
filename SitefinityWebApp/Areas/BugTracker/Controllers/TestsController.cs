﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitefinityWebApp.Areas.BugTracker.Controllers
{
    public class TestsController : Controller
    {
        public ActionResult Index()
        {
            return Content("TEST");
        }
    }
}