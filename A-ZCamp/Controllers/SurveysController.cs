﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using A_ZCamp.Models;
using System.Net.Mail;

namespace A_ZCamp.Controllers
{
    public class SurveysController : Controller
    {
        private ApplicationDbContext SurveyHandler;

        public SurveysController()
        {
            SurveyHandler = new ApplicationDbContext();
        }

        //Index for Survey
        public ActionResult Index()
        {
            return View();
        }

        //POST for Survey Index page
        public ActionResult IndexUpdate(SurveyLandingViewModel model)
        {
            if (!ModelState.IsValid)
            { 
                return View("Index", model);
            }

            var userAdd = SurveyHandler.SurveyRespondents;
            var update = SurveyHandler.SurveyRespondents.SingleOrDefault(z => z.Email == model.Email);

            if (update != null)
            {
                var checkSurveyTaker = (from x in SurveyHandler.SurveyRespondents
                                        where x.Email == update.Email
                                        select new SurveyLandingViewModel
                                        {
                                            Email = x.Email,
                                            RID = x.RID,
                                            PreCampDone = x.PreCampComplete,
                                            PostCampDone = x.PostCampComplete,
                                            OtherDone = x.OtherComplete
                                        }).ToList();

                foreach (var x in checkSurveyTaker)
                {
                    model.PreCampDone = x.PreCampDone;
                    model.PostCampDone = x.PostCampDone;
                    model.OtherDone = x.OtherDone;
                    model.RID = x.RID;
                }
            }

            else if (update == null)
            {
                Random rand = new Random();
                String temp = rand.Next(1, 999999999).ToString();

                var randCheck = SurveyHandler.SurveyRespondents.SingleOrDefault(z => z.RID == temp);
                Boolean newNumber = true;

                while (newNumber)
                {
                    if (randCheck != null)
                    {
                        temp = rand.Next(1, 999999999).ToString();
                    }
                    else
                    {
                        newNumber = false;
                    }
                }

                var addUser = new SurveyRespondent
                {
                    RID = temp,
                    Email = model.Email,
                    PreCampComplete = false,
                    PostCampComplete = false,
                    OtherComplete = false
                };

                model.PreCampDone = false;
                model.PostCampDone = false;
                model.OtherDone = false;
                model.RID = temp;

                userAdd.Add(addUser);
                SurveyHandler.SaveChanges();
            }

            return RedirectToAction("SurveyLanding", model);
        }

        //GET for Survey Landing page
        public ActionResult SurveyLanding(SurveyLandingViewModel model)
        {
            var RIDcheck = SurveyHandler.SurveyRespondents.SingleOrDefault(z => z.RID == model.RID);

            if (RIDcheck == null || model.RID == null)
            {
                return RedirectToAction("Index", "Surveys");
            }

            var display = (from x in SurveyHandler.SurveyTypes
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

        //GET for Other Survey page
        public ActionResult OtherSurvey(String RID)
        {
            var RIDcheck = SurveyHandler.SurveyRespondents.SingleOrDefault(z => z.RID == RID);

            if (RIDcheck == null || RID == null)
            {
                return RedirectToAction("Index", "Surveys");
            }

            SurveyPageViewModel vms = new SurveyPageViewModel();

            vms.ID = RID;
            vms.SurveyType = Survey.Other;

            var questionsToShow = SurveyHandler.SurveyQuestionOrderings;
            var answersToShow = SurveyHandler.SurveyQuestionSuppliedAnswers;
            var nameToShow = SurveyHandler.SurveyTypes;

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

        //GET for PreSurvey page
        public ActionResult PreSurvey(String RID)
        {
            var RIDcheck = SurveyHandler.SurveyRespondents.SingleOrDefault(z => z.RID == RID);

            if (RIDcheck == null || RID == null)
            {
                return RedirectToAction("Index", "Surveys");
            }

            SurveyPageViewModel vms = new SurveyPageViewModel();

            vms.ID = RID;
            vms.SurveyType = Survey.PreCamp;

            var questionsToShow = SurveyHandler.SurveyQuestionOrderings;
            var answersToShow = SurveyHandler.SurveyQuestionSuppliedAnswers;

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

        //GET for PostSurvey page
        public ActionResult PostSurvey(String RID)
        {
            var RIDcheck = SurveyHandler.SurveyRespondents.SingleOrDefault(z => z.RID == RID);

            if (RIDcheck == null || RID == null)
            {
                return RedirectToAction("Index", "Surveys");
            }

            SurveyPageViewModel vms = new SurveyPageViewModel();

            vms.ID = RID;
            vms.SurveyType = Survey.PostCamp;

            var questionsToShow = SurveyHandler.SurveyQuestionOrderings;
            var answersToShow = SurveyHandler.SurveyQuestionSuppliedAnswers;

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

        //POST for all Survey pages
        public ActionResult AddSurveyResults(SurveyPageViewModel newResults)
        {
            var test = SurveyHandler.SurveyResponses;

            foreach (var x in newResults.QuestionData)
            {
                test.Add(new SurveyResponse
                {
                    SurveyTypeId = x.Sid,
                    SurveyQuestionId = x.Qid,
                    Response = x.UserResponse
                });
            }

            SurveyHandler.SaveChanges();

            var userLookup = SurveyHandler.SurveyRespondents.SingleOrDefault(z => z.RID == newResults.ID);

            if (newResults.SurveyType == Survey.PreCamp)
            {
                userLookup.PreCampComplete = true;
                SurveyHandler.SaveChanges();
            }

            else if (newResults.SurveyType == Survey.PostCamp)
            {
                userLookup.PostCampComplete = true;
                SurveyHandler.SaveChanges();
            }

            else if (newResults.SurveyType == Survey.Other)
            {
                userLookup.OtherComplete = true;
                SurveyHandler.SaveChanges();
            }

            return RedirectToAction("SurveyConfirm", new { RID = newResults.ID});
        }

        //GET for Survey Confirm page
        public ActionResult SurveyConfirm(String RID, String SurveyType)
        {
            var RIDcheck = SurveyHandler.SurveyRespondents.SingleOrDefault(z => z.RID == RID);

            if (RIDcheck == null || RID == null )
            {
                return RedirectToAction("Index", "Surveys");
            }

            if (RIDcheck.PostCampComplete)
            {
                String email = RIDcheck.Email;
                String subject = "Survey Complete: Thanks for completing the Post-Camp Survey!";
                String message = "You completed the Post-Camp Survey! Thanks! Your opinion matters to us.";

                SendEmail(email, subject, message);
            }

            else if (RIDcheck.PreCampComplete)
            {
                String email = RIDcheck.Email;
                String subject = "Survey Complete: Thanks for completing the Pre-Camp Survey!";
                String message = "You completed the Pre-Camp Survey! Thanks! Your opinion matters to us.";

                SendEmail(email, subject, message);

                String email2 = RIDcheck.Email;
                String subject2 = "Survey Reminder: Post-Camp Survey";
                String message2 = "This is a reminder to compete the Post-Camp Survey once camp is over!";

                SendEmail(email2, subject2, message2);
            }

            

            /*
            string x = RIDcheck.Email; //This will be the string that needs to be replaced
            MailMessage mail = new MailMessage();
            mail.To.Add(x);
            //     mail.To.Add("Another Email ID where you wanna send same email");
            mail.From = new MailAddress("mu.az.camp@gmail.com");

            mail.Subject = "Email using Gmail";

            string Body = "Hi, this mail is to test sending mail" +
                          "using Gmail in ASP.NET";
            mail.Body = Body;

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;

            smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
            smtp.Credentials = new System.Net.NetworkCredential
                 ("mu.az.camp@gmail.com", "SURVEYSrule!");
            //Or your Smtp Email ID and Password
            smtp.EnableSsl = true;
            
            smtp.Send(mail);
            */


            return View();

        }

        public void SendEmail(String to, String subject, String message)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            //     mail.To.Add("Another Email ID where you wanna send same email");
            mail.From = new MailAddress("mu.az.camp@gmail.com");
            mail.Subject = subject;

            mail.Body = message;

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;

            smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
            smtp.Credentials = new System.Net.NetworkCredential
                 ("mu.az.camp@gmail.com", "SURVEYSrule!");
            //Or your Smtp Email ID and Password
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(mail);
            }
            catch (Exception e) { }
            
        }

    }
}