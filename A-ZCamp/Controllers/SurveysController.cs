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
  
        private ApplicationDbContext addQuestion;
        private ApplicationDbContext addResults;

        public SurveysController()
        {
            preSurvey = new ApplicationDbContext();
            postSurvey = new ApplicationDbContext();
            addQuestion = new ApplicationDbContext();
            addResults = new ApplicationDbContext();
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

            List<SurveyPageViewModel> vm = new List<SurveyPageViewModel>();

            /*
            vm = preSurvey.SurveyQuestionOrdering.Select(x => new SurveyPageViewModel {
                SurveyQuestionType = x.SurveyQuestion.QuestionType,
                SurveyQuestion = x.SurveyQuestion.Question,
                SuppliedAnswers = x.SurveyQuestion.SuppliedAnswers})
            */

            var SQ = preSurvey.SurveyQuestionOrdering;
            var SA = preSurvey.SurveyQuestionSuppliedAnswer;

            var testQ = (from x in SQ
                         where x.SurveyType.Survey == Survey.PreCamp
                         join y in SA on x.SurveyQuestionId equals y.SurveyQuestionId into ANS
                         select new SurveyPageViewModel
                         {
                             SurveyQuestionType = (from xy in SQ select xy.SurveyQuestion.QuestionType).ToList(), //x.SurveyQuestion.QuestionType
                             SurveyQuestion = (from zy in SQ select zy.SurveyQuestion.Question).ToList(), //x.SurveyQuestion.Question
                             SuppliedAnswers = (from y in ANS select y.Answer).ToList()
                         });


            var preSurveyQuestion3 = from pc in preSurvey.SurveyQuestionOrdering
                                     join px in preSurvey.SurveyQuestionSuppliedAnswer on pc.SurveyQuestionId equals px.SurveyQuestionId
                                     where pc.SurveyQuestion.QuestionType == QuestionType.MultipleChoice
                                     select pc;


            var preSurveyQuestion2 = preSurvey.SurveyQuestionOrdering.Where(x => x.SurveyType.Survey == Survey.PreCamp).OrderByDescending(y => y.Order).ToList();


            /*
            var preSurveyQuestions = from pc in preSurvey.SurveyQuestion
                                     where pc.QuestionType == QuestionType.MultipleChoice
                                     select pc;
            */                                 

            //var questions = preSurvey.SurveyQuestion.Where(x => x.QuestionType == QuestionType.ShortAnswer).ToList();

            return View(testQ);
        }

        public ActionResult PostSurvey()
        {
            return View();
        }

        public ActionResult Add(SurveyQuestionOrdering newQuestion)
        {
            addQuestion.SurveyQuestionOrdering.Add(newQuestion);
            addQuestion.SaveChanges();
            return RedirectToAction("Index", "Surveys");
        }

        public ActionResult AddSurveyResults(SurveyPageViewModel newResults)
        {
            var mc = addResults.SurveyMCResponse;
            var sa = addResults.SurveyShortAnswerResponse;

            /*
            mc.Add(new SurveyMCResponse { Choice = newResults.MCAnswer,
                                          SurveyQuestionId = 2,
                                          SurveyRespondentId = 1 });

            sa.Add(new SurveyShortAnswerResponse { Answer = newResults.ShortAnswer,
                                                   SurveyQuestionId = 4,
                                                   SurveyRespondentId = 1 });
            */
            addResults.SaveChanges();

            return RedirectToAction("Index", "Surveys");
        }
    }
}