using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace A_ZCamp.Controllers
{
    public class EditQuestionController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Add()
        {
            return View();
        }

        [Authorize]
        public ActionResult Edit()
        {
            return View();
        }

        [Authorize]
        public ActionResult Delete()
        {
            return View();
        }
    }
}