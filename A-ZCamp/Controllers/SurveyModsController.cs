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
        private ApplicationDbContext SurveyModHandler;

        public SurveyModsController()
        {
            SurveyModHandler = new ApplicationDbContext();
        }

        //Index View for SurveyMods
        public ActionResult Index()
        {
            return View();
        }

        //GET for the Add Question page
        public ActionResult QuestionAdd()
        {
            SurveyAddQuestionViewModel addAQuestion = new SurveyAddQuestionViewModel();

            var Surveys = (from x in SurveyModHandler.SurveyTypes
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

        //GET for the Question Options page
        public ActionResult QuestionOptions()
        {
            SurveyQuestionOptionsViewModel QuestionOptions = new SurveyQuestionOptionsViewModel();

            var Options = (from x in SurveyModHandler.SurveyQuestionOrderings
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

        //GET for the Survey Options page
        public ActionResult SurveyOptions()
        {
            SurveyOptionsViewModel SurveyOptions = new SurveyOptionsViewModel();

            var SurveysToSee = (from x in SurveyModHandler.SurveyTypes
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

        //POST for the Survey Options page
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
                    var mod = SurveyModHandler.SurveyTypes.SingleOrDefault(z => z.SurveyTypeId == x.Sid);
                    mod.Active = x.Active;
                }

                SurveyModHandler.SaveChanges();

                return RedirectToAction("SurveyOptions", "SurveyMods");
            }
        }

        //POST for the Survey Options / Update Question page
        public ActionResult SurveyQuestionUpdate(int? surveyID)
        {
            if (surveyID == null)
            {
                return RedirectToAction("SurveyOptions", "SurveyMods");
            }

            int SurID = surveyID.Value;

            SurveyQuestionAssignment questionsUpdate = new SurveyQuestionAssignment();

            var attachments = (from x in SurveyModHandler.SurveyQuestionOrderings
                               where x.SurveyTypeId == surveyID
                               select x).ToList();

            var questions = (from x in SurveyModHandler.SurveyQuestions
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

        //POST for Survey Options / Question Update page
        public ActionResult SurveyQuestionsAssignment(SurveyQuestionAssignment model)
        {
            foreach (var x in model.QuestionData)
            {
                if (x.ChangeAssignment.Equals(true))
                {
                    var update = SurveyModHandler.SurveyQuestionOrderings.SingleOrDefault(z => z.SurveyTypeId == x.SurveyID && z.SurveyQuestionId == x.QuestionID);

                    if (update == null)
                    {
                        var AddEntry = new SurveyQuestionOrdering
                        {
                            SurveyTypeId = x.SurveyID,
                            SurveyQuestionId = x.QuestionID,
                            Order = 1
                        };

                        SurveyModHandler.SurveyQuestionOrderings.Add(AddEntry);
                    }

                    else
                    {
                    }
                }

                else if (x.ChangeAssignment.Equals(false))
                {
                    var update = SurveyModHandler.SurveyQuestionOrderings.SingleOrDefault(z => z.SurveyTypeId == x.SurveyID && z.SurveyQuestionId == x.QuestionID);

                    if (update == null)
                    {
                    }

                    else
                    {
                        SurveyModHandler.SurveyQuestionOrderings.Remove(update);
                    }
                }
                
            }

            SurveyModHandler.SaveChanges();

            return RedirectToAction("SurveyOptions", "SurveyMods");
        }

        //GET for Add Survey page
        public ActionResult SurveyAdd()
        {
            SurveyAddViewModel addSurvey = new SurveyAddViewModel();

            var Surveys = (from x in SurveyModHandler.SurveyTypes
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

        //POST for Add Survey page
        public ActionResult SurveyCreate(SurveyAddViewModel model)
        {
            var nameCheck = (from x in SurveyModHandler.SurveyTypes
                             select x.Name).ToList();

            foreach (var x in nameCheck)
            {
                if (x == model.SurveyName)
                {
                    ModelState.AddModelError("Duplicate Survey Name", "There already exists a Survey with that name. Pick a new name.");

                    return View("SurveyAdd", model);
                }
            }


            var newSurvey = SurveyModHandler.SurveyTypes;

            var AddSurvey = new SurveyType
            {
                Name = model.SurveyName,
                Survey = model.SurveyType,
                Active = false
            };

            newSurvey.Add(AddSurvey);
            SurveyModHandler.SaveChanges();

            return RedirectToAction("SurveyAdd", "SurveyMods");
        }

        //POST for Survey Options page
        public ActionResult UpdateQuestions(SurveyQuestionOptionsViewModel options)
        {

            foreach (var x in options.AllSurveyQuestions)
            {
                var update = SurveyModHandler.SurveyQuestionOrderings.FirstOrDefault(z => z.SurveyQuestion.SurveyQuestionId == x.Qid && z.SurveyTypeId == x.Sid);
                update.Order = x.Ordering;
                update.SurveyQuestion.Active = x.Active;
            }

            SurveyModHandler.SaveChanges();

            return RedirectToAction("Index", "SurveyMods");
        }

        //POST for Add Question page
        public ActionResult AddNewQuestion(SurveyAddQuestionViewModel QModel)
        {
            var questionCreate = SurveyModHandler.SurveyQuestions;
            var questionOrdering = SurveyModHandler.SurveyQuestionOrderings;
            var answersCreate = SurveyModHandler.SurveyQuestionSuppliedAnswers;

            var SurveyQuestion = new SurveyQuestion
            {
                Question = QModel.QuestionToSubmit,
                QuestionType = QModel.QuestionTypeToSubmit,
                Active = true,
            };

            questionCreate.Add(SurveyQuestion);
            SurveyModHandler.SaveChanges();

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

            SurveyModHandler.SaveChanges();

            return RedirectToAction("Index", "SurveyMods");
        }

        //GET for Question Edit page
        public ActionResult QuestionEdit()
        {
            QuestionOverallViewModel all = new QuestionOverallViewModel();

            var questions = (from x in SurveyModHandler.SurveyQuestions
                             select new QuestionData
                             {
                                 Qid = x.SurveyQuestionId,
                                 Question = x.Question,
                                 QType = x.QuestionType
                             }).ToList();

            foreach (var x in questions)
            {
                all.allQuestions.Add(x);
            }

            return View(all);
        }

        //GET for Question Edit Specific page
        public ActionResult QuestionSpecificEdit(int? questionID)
        {
            if (questionID == null)
            {
                return RedirectToAction("Index", "SurveyMods");
            }

            QuestionData data = new QuestionData();

            var questionEdit = (from x in SurveyModHandler.SurveyQuestions
                                where x.SurveyQuestionId == questionID
                                join y in SurveyModHandler.SurveyQuestionSuppliedAnswers on x.SurveyQuestionId equals y.SurveyQuestionId into GOOD
                                select new QuestionData
                                {
                                    Qid = x.SurveyQuestionId,
                                    QType = x.QuestionType,
                                    Question = x.Question,
                                    QSupAnswers = (from y in GOOD select y.Answer).ToList()
                                }).ToList();

            if (questionEdit == null)
            {
                return RedirectToAction("Index", "SurveyMods");
            }

            foreach (var x in questionEdit)
            {
                data = x;
            }

            return View(data);
        }


        //POST for Question Edit Specific page
        public ActionResult QuestionSpecificUpdate(QuestionData model)
        {
            var question = SurveyModHandler.SurveyQuestions.FirstOrDefault(z => z.SurveyQuestionId == model.Qid);

            question.Question = model.Question;

            if (model.QSupAnswers == null)
            {
                SurveyModHandler.SaveChanges();
                return RedirectToAction("QuestionEdit", "SurveyMods");
            }

            else
            {
                var suppliedAnswers = (from x in SurveyModHandler.SurveyQuestionSuppliedAnswers
                                       where x.SurveyQuestionId == model.Qid
                                       select x).ToList();

                int z = 0;

                foreach (var x in suppliedAnswers)
                {
                    x.Answer = model.QSupAnswers[z];
                    z++;
                }

                SurveyModHandler.SaveChanges();
                return RedirectToAction("QuestionEdit", "SurveyMods");
            }
        }

        //GET for Survey Options / Edit Survey Name page
        public ActionResult SurveyEdit(int? surveyID)
        {
            if (surveyID == null)
            {
                return RedirectToAction("Index", "SurveyMods");
            }

            var SurveyEdit = SurveyModHandler.SurveyTypes.FirstOrDefault(z => z.SurveyTypeId == surveyID);

            if (SurveyEdit == null)
            {
                return RedirectToAction("Index", "SurveyMods");
            }

            SurveyData SurveyData = new SurveyData
            {
                SurveyName = SurveyEdit.Name,
                SurveyType = SurveyEdit.Survey,
                Sid = SurveyEdit.SurveyTypeId
            };

            return View(SurveyData);
        }

        //POST for Survey Options / Edit Survey Name page
        public ActionResult SurveyEditSubmit(SurveyData model)
        {
            var survey = SurveyModHandler.SurveyTypes.FirstOrDefault(z => z.SurveyTypeId == model.Sid);

            survey.Name = model.SurveyName;

            SurveyModHandler.SaveChanges();

            return RedirectToAction("SurveyOptions", "SurveyMods");
        }
    }
}