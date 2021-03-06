﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace A_ZCamp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "This page is for the Marshall University Adventure Zone Camp";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Do you have questions or concerns about the camp? Feel free to contact us!";

            return View();
        }
    }
}