using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace A_ZCamp.Controllers
{
    public class EditQuestionController : Controller
    {
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Edit()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete()
        {
            return View();
        }
    }
}