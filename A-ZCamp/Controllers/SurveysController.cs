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
            /*
            var preSurveyQuestion = from pc in preSurvey.SurveyQuestionOrdering
                                    where pc.SurveyType.Survey == Survey.PreCamp
                                    select pc;
                                    */

            var preSurveyQuestion2 = preSurvey.SurveyQuestionOrdering.Where(x => x.SurveyType.Survey == Survey.PreCamp).OrderByDescending(y => y.Order).ToList();

            /*
            var preSurveyQuestions = from pc in preSurvey.SurveyQuestion
                                     where pc.QuestionType == QuestionType.MultipleChoice
                                     select pc;
            */                                 

            //var questions = preSurvey.SurveyQuestion.Where(x => x.QuestionType == QuestionType.ShortAnswer).ToList();

            return View(preSurveyQuestion2);
        }

        public ActionResult PostSurvey()
        {
            return View();
        }
    }
}