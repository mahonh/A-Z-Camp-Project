using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace A_ZCamp.Controllers
{
    public class SurveyModsController : Controller
    {
        // GET: Edit
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QuestionAdd()
        {
            return View();
        }

        public ActionResult QuestionOptions()
        {
            return View();
        }

        public ActionResult SurveyOptions()
        {
            return View();
        }
    }
}