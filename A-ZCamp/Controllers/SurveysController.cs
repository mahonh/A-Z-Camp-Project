using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using A_ZCamp.Models;

namespace A_ZCamp.Controllers
{
    public class SurveysController : Controller
    {
        private ApplicationDbContext preSurvey;
        private ApplicationDbContext postSurvey;

        public SurveysController()
        {
            preSurvey = new ApplicationDbContext();
            postSurvey = new ApplicationDbContext();
        }

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

        public ActionResult AddSA()
        {
            
            return RedirectToAction("Index", "Surveys");
        }

        public ActionResult AddMC()
        {
            
            return RedirectToAction("Index", "Surveys");
        }

        public ActionResult AddSurveyResultsPRE(SMPreCamp preSurveyResults)
        {
            preSurvey.SMPreCamp.Add(preSurveyResults);
            preSurvey.SaveChanges();

            return RedirectToAction("Index", "Surveys");
        }

        [HttpPost]
        public ActionResult AddSurveyResultsPOST(SMPostCamp postSurveyResults)
        {
           if (ModelState.IsValid)
            {
              postSurvey.SMPostCamp.Add(postSurveyResults);
            postSurvey.SaveChanges();

            return RedirectToAction("Index", "Surveys");  
            }

     
            return View();
        }
    }
}