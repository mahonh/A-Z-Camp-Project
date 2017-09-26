using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace A_ZCamp.Controllers
{
    public class SurveysController : Controller
    {
        // GET: Surveys
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PreSurvey()
        {
            return View();
        }

        public ActionResult PostSurvey()
        {
            return View();
        }
    }
}