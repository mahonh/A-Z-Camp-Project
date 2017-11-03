using A_ZCamp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace A_ZCamp.Controllers
{
    public class SurveyReportingController : Controller
    {
        private ApplicationDbContext reporting;

        [Authorize] //Roles = "Administrator"
        public ActionResult Index()
        {
            reporting = new ApplicationDbContext();

            var pre = reporting.SMPreCamp.ToList();
            var post = reporting.SMPostCamp.ToList();

            SMReporting reporter = new SMReporting();

            reporter.PreCampData = pre;
            reporter.PostCampData = post;

            return View(reporter);
        }
    }
}