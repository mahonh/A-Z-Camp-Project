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
      
        private ApplicationDbContext addResults;

        public SurveysController()
        {
            addResults = new ApplicationDbContext();
            preSurvey = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PreSurvey()
        {
            SurveyPageViewModel vms = new SurveyPageViewModel();

            var questionsToShow = preSurvey.SurveyQuestionOrderings;
            var answersToShow = preSurvey.SurveyQuestionSuppliedAnswers;

            var surveyData = (from x in questionsToShow
                             where x.SurveyType.Survey == Survey.PreCamp
                             join y in answersToShow on x.SurveyQuestionId equals y.SurveyQuestionId into GOOD
                             select new QuestionData
                             {
                                 Qid = x.SurveyQuestionId,
                                 QType = x.SurveyQuestion.QuestionType,
                                 Question = x.SurveyQuestion.Question,
                                 QSupAnswers = (from y in GOOD select y.Answer).ToList()
                             }).ToList();

            foreach (var p in surveyData)
            {
                vms.QuestionData.Add(p);
            }

           // vms.QuestionData.AddRange(surveyData);

            /*
            var preSurveyQuestion = from pc in preSurvey.SurveyQuestionOrdering
                                    where pc.SurveyType.Survey == Survey.PreCamp
                                    select pc;
                                    */

            
            /*
            var stuff = preSurvey.SurveyQuestionOrderings.Where(x => x.SurveyType.Survey == Survey.PreCamp)
                                                   .Select(x => new QuestionData
                                                   {
                                                       Question = x.SurveyQuestion.Question,
                                                       QType = x.SurveyQuestion.QuestionType,
                                                       Qid = x.SurveyQuestion.SurveyQuestionId
                                                       vms.QuestionData.Add()
                                                   });
                                                   */


            /*
            vm = preSurvey.SurveyQuestionOrdering.Select(x => new SurveyPageViewModel {
                SurveyQuestionType = x.SurveyQuestion.QuestionType,
                SurveyQuestion = x.SurveyQuestion.Question,
                SuppliedAnswers = x.SurveyQuestion.SuppliedAnswers})
            */

            /*
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
            */

            /*
            var preSurveyQuestions = from pc in preSurvey.SurveyQuestion
                                     where pc.QuestionType == QuestionType.MultipleChoice
                                     select pc;
            */

            //var questions = preSurvey.SurveyQuestion.Where(x => x.QuestionType == QuestionType.ShortAnswer).ToList();

            return View(vms);
        }

        public ActionResult PostSurvey()
        {
            return View();
        }

        public ActionResult Add(SurveyQuestionOrdering newQuestion)
        {
            /*
            addQuestion.SurveyQuestionOrdering.Add(newQuestion);
            addQuestion.SaveChanges();
            return RedirectToAction("Index", "Surveys");
            */

            return View();
        }

        public ActionResult AddSurveyResults(SurveyPageViewModel newResults)
        {
            var test = addResults.SurveyResponses;

            foreach (var x in newResults.QuestionData)
            {
                test.Add(new SurveyResponse
                {
                    SurveyQuestionId = x.Qid,
                    Response = x.UserResponse
                });
            }

            addResults.SaveChanges();

            

            /*
            var mc = addResults.SurveyMCResponse;
            var sa = addResults.SurveyShortAnswerResponse;

            
            mc.Add(new SurveyMCResponse { Choice = newResults.MCAnswer,
                                          SurveyQuestionId = 2,
                                          SurveyRespondentId = 1 });

            sa.Add(new SurveyShortAnswerResponse { Answer = newResults.ShortAnswer,
                                                   SurveyQuestionId = 4,
                                                   SurveyRespondentId = 1 });
            
            addResults.SaveChanges();

            return RedirectToAction("Index", "Surveys");
            */

            return RedirectToAction("Index", "Surveys");
        }
    }
}