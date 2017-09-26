using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using A_ZCamp.Models;
using System.Web.Mvc;

namespace A_ZCamp.Controllers
{
    public class SurveyReportsController : Controller
    {
        private ApplicationDbContext _dbContext;

        public SurveyReportsController()
        {
            _dbContext = new ApplicationDbContext();
        }

        // GET: SurveyReports
        public ActionResult Index()
        {
            var PreSurveys = _dbContext.PreSurvey.ToList();
            var PostSurveys = _dbContext.PostSurvey.ToList();

            SurveyReports view = new SurveyReports();

            view.PreSurveys = PreSurveys;
            view.PostSurveys = PostSurveys;

            return View(view);
        }
    }
}