using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using A_ZCamp.Models;

namespace A_ZCamp.Controllers
{
    [Authorize]
    public class SurveyReportsController : Controller
    {
        private ApplicationDbContext report = new ApplicationDbContext();

        public ActionResult Index()
        {
            REPORTVIEWMODEL reporter = new REPORTVIEWMODEL();

            var results = from x in report.SurveyResponses
                          select new REPORTDATA
                          {
                              Question = x.SurveyQuestion.Question,
                              Response = x.Response,
                              Survey = x.SurveyType.Name
                          };

            foreach (var x in results)
            {
                reporter.reportData.Add(x);
            }

            return View(reporter);
        }
    }
}