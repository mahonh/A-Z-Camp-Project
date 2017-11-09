using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using A_ZCamp.Models;

namespace A_ZCamp.Controllers
{
    public class SurveyModsController : Controller
    {
        private ApplicationDbContext AddQuestion;
        private ApplicationDbContext NewQuestion;
        private ApplicationDbContext OptionsQuestions;

        public SurveyModsController()
        {
            AddQuestion = new ApplicationDbContext();
            NewQuestion = new ApplicationDbContext();
            OptionsQuestions = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult QuestionAdd()
        {
            SurveyAddQuestionViewModel addAQuestion = new SurveyAddQuestionViewModel();

            var Surveys = (from x in AddQuestion.SurveyTypes
                           select new SurveyData
                           {
                               SurveyName = x.Name,
                               SurveyType = x.Survey,
                               Sid = x.SurveyTypeId 
                           }).ToList();

            foreach (var x in Surveys)
            {
                addAQuestion.Surveys.Add(x);
            }

            return View(addAQuestion);
        }

        public ActionResult QuestionOptions()
        {
            SurveyQuestionOptionsViewModel QuestionOptions = new SurveyQuestionOptionsViewModel();

            var Options = (from x in OptionsQuestions.SurveyQuestionOrderings
                           select new OptionsData
                           {
                               Active = x.SurveyQuestion.Active,
                               Question = x.SurveyQuestion.Question,
                               QuestionType = x.SurveyQuestion.QuestionType,
                               Ordering = x.Order,
                               SurveyName = x.SurveyType.Name,
                               Qid = x.SurveyQuestion.SurveyQuestionId
                           }).ToList();

            foreach (var x in Options)
            {
                QuestionOptions.AllSurveyQuestions.Add(x);
            }

            return View(QuestionOptions);
        }

        public ActionResult SurveyOptions()
        {
            return View();
        }

        public ActionResult UpdateQuestions(SurveyQuestionOptionsViewModel options)
        {

            foreach (var x in options.AllSurveyQuestions)
            {
                var update = OptionsQuestions.SurveyQuestionOrderings.SingleOrDefault(z => z.SurveyQuestion.SurveyQuestionId == x.Qid);
                update.Order = x.Ordering;
                update.SurveyQuestion.Active = x.Active;
            }

            OptionsQuestions.SaveChanges();

            return RedirectToAction("Index", "SurveyMods");
        }

        public ActionResult AddNewQuestion(SurveyAddQuestionViewModel QModel)
        {
            var questionCreate = NewQuestion.SurveyQuestions;
            var questionOrdering = NewQuestion.SurveyQuestionOrderings;
            var answersCreate = NewQuestion.SurveyQuestionSuppliedAnswers;

            var SurveyQuestion = new SurveyQuestion
            {
                Question = QModel.QuestionToSubmit,
                QuestionType = QModel.QuestionTypeToSubmit,
                Active = true,
            };

            questionCreate.Add(SurveyQuestion);
            NewQuestion.SaveChanges();

            int Qid = SurveyQuestion.SurveyQuestionId;

            if (!(QModel.QuestionTypeToSubmit == QuestionType.ShortAnswer))
            {
                foreach (var x in QModel.SuppliedAnswersQuestionToSumbit)
                {
                    answersCreate.Add(new SurveyQuestionSuppliedAnswer
                    {
                        Answer = x,
                        SurveyQuestionId = Qid
                    });
                }
            }

            questionOrdering.Add(new SurveyQuestionOrdering
            {
                Order = 1,
                SurveyQuestionId = Qid,
                SurveyTypeId = QModel.SurveyID
            });

            NewQuestion.SaveChanges();

            return RedirectToAction("Index", "SurveyMods");
        }
    }
}