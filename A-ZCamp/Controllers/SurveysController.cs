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

        public ActionResult IndexUpdate(SurveyLandingViewModel model)
        {
            if (!ModelState.IsValid)
            { 
                return View("Index", model);
            }

            var userAdd = addResults.SurveyRespondents;
            var update = addResults.SurveyRespondents.SingleOrDefault(z => z.Email == model.Email);

            if (update != null)
            {
                var checkSurveyTaker = (from x in addResults.SurveyRespondents
                                        where x.Email == update.Email
                                        select new SurveyLandingViewModel
                                        {
                                            Email = x.Email,
                                            PreCampDone = x.PreCampComplete,
                                            PostCampDone = x.PostCampComplete,
                                            OtherDone = x.OtherComplete
                                        }).ToList();

                foreach (var x in checkSurveyTaker)
                {
                    model.PreCampDone = x.PreCampDone;
                    model.PostCampDone = x.PostCampDone;
                    model.OtherDone = x.OtherDone;
                }
            }

            else if (update == null)
            {
                var addUser = new SurveyRespondent
                {
                    Email = model.Email,
                    PreCampComplete = false,
                    PostCampComplete = false,
                    OtherComplete = false
                };

                model.PreCampDone = false;
                model.PostCampDone = false;
                model.OtherDone = false;

                userAdd.Add(addUser);
                addResults.SaveChanges();
            }

            return View("SurveyLanding", model);
        }

        public ActionResult SurveyLanding(SurveyLandingViewModel model)
        {
            if (model.Email == null)
            {
                return RedirectToAction("Index", "Surveys");
            }

            var display = (from x in preSurvey.SurveyTypes
                           where x.Survey == Survey.Other && x.Active == true
                           select new SurveyLandingViewModel
                           {
                               ShowOther = x.Active,
                               OtherName = x.Name
                           }).ToList();

            foreach (var x in display)
            {
                model.OtherName = x.OtherName;
                model.ShowOther = x.ShowOther;
            }

            return View(model);
        }

        public ActionResult OtherSurvey(String ID)
        {
            SurveyPageViewModel vms = new SurveyPageViewModel();

            var questionsToShow = preSurvey.SurveyQuestionOrderings;
            var answersToShow = preSurvey.SurveyQuestionSuppliedAnswers;
            var nameToShow = preSurvey.SurveyTypes;

            var surveyData = (from x in questionsToShow
                              where x.SurveyType.Survey == Survey.Other &&
                                    x.SurveyType.Active == true &&
                                    x.SurveyQuestion.Active == true
                              join y in answersToShow on x.SurveyQuestionId equals y.SurveyQuestionId into GOOD
                              orderby x.Order ascending
                              select new QuestionData
                              {
                                  Sid = x.SurveyTypeId,
                                  Qid = x.SurveyQuestionId,
                                  QType = x.SurveyQuestion.QuestionType,
                                  Question = x.SurveyQuestion.Question,
                                  QSupAnswers = (from y in GOOD select y.Answer).ToList()
                              }).ToList();

            var name = (from x in nameToShow
                        where x.Survey == Survey.Other && x.Active == true
                        select x.Name).ToList();

            foreach (var x in name)
            {
                vms.SurveyName = x;
            }

            foreach (var p in surveyData)
            {
                vms.QuestionData.Add(p);
            }
            return View(vms);
        }

        public ActionResult PreSurvey(String ID)
        {
            SurveyPageViewModel vms = new SurveyPageViewModel();

            var questionsToShow = preSurvey.SurveyQuestionOrderings;
            var answersToShow = preSurvey.SurveyQuestionSuppliedAnswers;

            var surveyData = (from x in questionsToShow
                             where x.SurveyType.Survey == Survey.PreCamp &&
                                   x.SurveyType.Active == true &&
                                   x.SurveyQuestion.Active == true
                             join y in answersToShow on x.SurveyQuestionId equals y.SurveyQuestionId into GOOD
                             orderby x.Order ascending
                             select new QuestionData
                             {
                                 Sid = x.SurveyTypeId,
                                 Qid = x.SurveyQuestionId,
                                 QType = x.SurveyQuestion.QuestionType,
                                 Question = x.SurveyQuestion.Question,
                                 QSupAnswers = (from y in GOOD select y.Answer).ToList()
                             }).ToList();

            foreach (var p in surveyData)
            {
                vms.QuestionData.Add(p);
            }

            return View(vms);
        }

        public ActionResult PostSurvey(String ID)
        {
            SurveyPageViewModel vms = new SurveyPageViewModel();

            var questionsToShow = preSurvey.SurveyQuestionOrderings;
            var answersToShow = preSurvey.SurveyQuestionSuppliedAnswers;

            var surveyData = (from x in questionsToShow
                              where x.SurveyType.Survey == Survey.PostCamp &&
                                    x.SurveyType.Active == true &&
                                    x.SurveyQuestion.Active == true
                              join y in answersToShow on x.SurveyQuestionId equals y.SurveyQuestionId into GOOD
                              orderby x.Order ascending
                              select new QuestionData
                              {
                                  Sid = x.SurveyTypeId,
                                  Qid = x.SurveyQuestionId,
                                  QType = x.SurveyQuestion.QuestionType,
                                  Question = x.SurveyQuestion.Question,
                                  QSupAnswers = (from y in GOOD select y.Answer).ToList()
                              }).ToList();

            foreach (var p in surveyData)
            {
                vms.QuestionData.Add(p);
            }
            return View(vms);
        }

        public ActionResult AddSurveyResults(SurveyPageViewModel newResults)
        {
            var test = addResults.SurveyResponses;

            foreach (var x in newResults.QuestionData)
            {
                test.Add(new SurveyResponse
                {
                    SurveyTypeId = x.Sid,
                    SurveyQuestionId = x.Qid,
                    Response = x.UserResponse
                });
            }

            addResults.SaveChanges();

            return RedirectToAction("Index", "Surveys");
        }
    }
}