using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using A_ZCamp.Models;
using System.Web.Mvc;

namespace A_ZCamp.Controllers
{
    public class PreSurveyController : Controller
    {
        private ApplicationDbContext _dbContextPre;
    
        public PreSurveyController()
        {
            _dbContextPre = new ApplicationDbContext();
        }

        // GET: PreSurvey
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(PreSurvey presurvey)
        {
            _dbContextPre.PreSurvey.Add(presurvey);
            _dbContextPre.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}