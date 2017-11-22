using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using A_ZCamp.Models;

namespace A_ZCamp.Controllers
{
    [Authorize]
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
                           orderby x.Survey ascending, x.Name
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
                           where x.SurveyType.Active
                           select new OptionsData
                           {
                               Active = x.SurveyQuestion.Active,
                               Question = x.SurveyQuestion.Question,
                               QuestionType = x.SurveyQuestion.QuestionType,
                               Ordering = x.Order,
                               SurveyName = x.SurveyType.Name,
                               Qid = x.SurveyQuestion.SurveyQuestionId,
                               Sid = x.SurveyTypeId
                           }).ToList();

            foreach (var x in Options)
            {
                QuestionOptions.AllSurveyQuestions.Add(x);
            }

            return View(QuestionOptions);
        }

        public ActionResult SurveyOptions()
        {
            SurveyOptionsViewModel SurveyOptions = new SurveyOptionsViewModel();

            var SurveysToSee = (from x in OptionsQuestions.SurveyTypes
                                select new SurveyData
                                {
                                    Sid = x.SurveyTypeId,
                                    SurveyName = x.Name,
                                    SurveyType = x.Survey,
                                    Active = x.Active
                                }).ToList();

            foreach (var x in SurveysToSee)
            {
                SurveyOptions.AllSurveys.Add(x);
            }

            return View(SurveyOptions);
        }

        public ActionResult SurveyOptionsUpdate(SurveyOptionsViewModel model)
        {
            int a = 0;
            int b = 0;
            int c = 0;

            foreach (var x in model.AllSurveys)
            {
                if (x.Active == true && x.SurveyType == Survey.PreCamp)
                {
                    a++;
                }

                else if (x.Active == true && x.SurveyType == Survey.PostCamp)
                {
                    b++;
                }

                else if (x.Active == true && x.SurveyType == Survey.Other)
                {
                    c++;
                }
            }

            if (a > 1 || b > 1 || c > 1)
            {
                ModelState.AddModelError("Assignments", "Only 0 or 1 of each Survey Type can be active at a time.");

                return View("SurveyOptions", model);
            }

            else
            { 

                foreach (var x in model.AllSurveys)
                {
                    var mod = AddQuestion.SurveyTypes.SingleOrDefault(z => z.SurveyTypeId == x.Sid);
                    mod.Active = x.Active;
                }

                AddQuestion.SaveChanges();

                return RedirectToAction("SurveyOptions", "SurveyMods");
            }
        }

        public ActionResult SurveyQuestionUpdate(int? surveyID)
        {
            if (surveyID == null)
            {
                return RedirectToAction("SurveyOptions", "SurveyMods");
            }

            int SurID = surveyID.Value;

            SurveyQuestionAssignment questionsUpdate = new SurveyQuestionAssignment();

            var attachments = (from x in AddQuestion.SurveyQuestionOrderings
                               where x.SurveyTypeId == surveyID
                               select x).ToList();

            var questions = (from x in AddQuestion.SurveyQuestions
                             select new QuestionAssignmentData
                             {
                                 QuestionID = x.SurveyQuestionId,
                                 QuestionName = x.Question,
                                 QuestionType = x.QuestionType,
                                 SurveyID = SurID
                             }).ToList();

            foreach (var x in questions)
            {
                foreach (var y in attachments)
                {
                    if (y.SurveyQuestionId == x.QuestionID)
                    {
                        x.ChangeAssignment = (y.SurveyTypeId == surveyID);
                    }
                }
            }

            foreach (var x in questions)
            {
                questionsUpdate.QuestionData.Add(x);
            }

            return View(questionsUpdate);
        }

        public ActionResult SurveyQuestionsAssignment(SurveyQuestionAssignment model)
        {
            foreach (var x in model.QuestionData)
            {
                if (x.ChangeAssignment.Equals(true))
                {
                    var update = OptionsQuestions.SurveyQuestionOrderings.SingleOrDefault(z => z.SurveyTypeId == x.SurveyID && z.SurveyQuestionId == x.QuestionID);

                    if (update == null)
                    {
                        var AddEntry = new SurveyQuestionOrdering
                        {
                            SurveyTypeId = x.SurveyID,
                            SurveyQuestionId = x.QuestionID,
                            Order = 1
                        };

                        OptionsQuestions.SurveyQuestionOrderings.Add(AddEntry);
                    }

                    else
                    {
                    }
                }

                else if (x.ChangeAssignment.Equals(false))
                {
                    var update = OptionsQuestions.SurveyQuestionOrderings.SingleOrDefault(z => z.SurveyTypeId == x.SurveyID && z.SurveyQuestionId == x.QuestionID);

                    if (update == null)
                    {
                    }

                    else
                    {
                        OptionsQuestions.SurveyQuestionOrderings.Remove(update);
                    }
                }
                
            }

            OptionsQuestions.SaveChanges();

            return RedirectToAction("SurveyOptions", "SurveyMods");
        }

        public ActionResult SurveyAdd()
        {
            SurveyAddViewModel addSurvey = new SurveyAddViewModel();

            var Surveys = (from x in AddQuestion.SurveyTypes
                           orderby x.Survey ascending, x.Name
                           select new SurveyData
                           {
                               SurveyName = x.Name,
                               SurveyType = x.Survey
                           }).ToList();

            foreach (var x in Surveys)
            {
                addSurvey.Surveys.Add(x);
            }

            return View(addSurvey);
        }

        public ActionResult SurveyCreate(SurveyAddViewModel model)
        {
            var nameCheck = (from x in AddQuestion.SurveyTypes
                             select x.Name).ToList();

            foreach (var x in nameCheck)
            {
                if (x == model.SurveyName)
                {
                    ModelState.AddModelError("Duplicate Survey Name", "There already exists a Survey with that name. Pick a new name.");

                    return View("SurveyAdd", model);
                }
            }


            var newSurvey = AddQuestion.SurveyTypes;

            var AddSurvey = new SurveyType
            {
                Name = model.SurveyName,
                Survey = model.SurveyType,
                Active = false
            };

            newSurvey.Add(AddSurvey);
            AddQuestion.SaveChanges();

            return RedirectToAction("SurveyAdd", "SurveyMods");
        }

        public ActionResult UpdateQuestions(SurveyQuestionOptionsViewModel options)
        {

            foreach (var x in options.AllSurveyQuestions)
            {
                var update = OptionsQuestions.SurveyQuestionOrderings.FirstOrDefault(z => z.SurveyQuestion.SurveyQuestionId == x.Qid && z.SurveyTypeId == x.Sid);
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