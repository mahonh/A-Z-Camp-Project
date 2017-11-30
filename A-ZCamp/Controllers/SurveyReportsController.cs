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
        private ApplicationDbContext reportHandler = new ApplicationDbContext();

        public SurveyReportsController()
        {
            reportHandler = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            SurveyReportsViewModel model = new SurveyReportsViewModel();

            IEnumerable<SelectListItem> items = reportHandler.SurveyTypes.Select
                (x => new SelectListItem
                {
                    Value = x.SurveyTypeId.ToString(),
                    Text = x.Name
                });

            model.Surveys = items;

            return View(model);
        }

        public ActionResult ViewQuestion(SurveyReportsViewModel model)
        {
            //Maybe remove this and use Hiddenfor on view?
            IEnumerable<SelectListItem> items = reportHandler.SurveyTypes.Select
                (x => new SelectListItem
                {
                    Value = x.SurveyTypeId.ToString(),
                    Text = x.Name
                });

            model.Surveys = items;

            int choice = Int32.Parse(model.SurveyChoice);

            var questions = (from x in reportHandler.SurveyQuestions
                             join y in reportHandler.SurveyResponses on x.SurveyQuestionId equals y.SurveyQuestionId
                             where y.SurveyTypeId == choice
                             orderby y.SurveyQuestion.QuestionType descending
                             select new SurveyReportsData
                             {
                                 QuestionID = x.SurveyQuestionId,
                                 QuestionName = x.Question,
                                 QuestionType = x.QuestionType,
                                 SurveyID = y.SurveyTypeId
                             }).ToList();

            var uniqueQuestion = questions.GroupBy(x => x.QuestionID).Select(y => y.First());

            foreach (var x in uniqueQuestion)
            {
                model.DataToRun.Add(x);
            }

            return View("Index", model);
        }

        public ActionResult RunReport(SurveyReportsViewModel model)
        {
            ChartMaker charts = new ChartMaker();

            foreach (var z in model.DataToRun)
            {
                if (z.Include && z.ChartType != ChartType.Table)
                {
                    var results = (from x in reportHandler.SurveyResponses
                                   where x.SurveyQuestionId == z.QuestionID && x.SurveyTypeId == z.SurveyID
                                   select x).ToList();

                    var checkAgainst = (from x in reportHandler.SurveyQuestionSuppliedAnswers
                                        where x.SurveyQuestionId == z.QuestionID
                                        select x).ToList();

                    List<int> yVal = new List<int>();
                    List<String> xVal = new List<string>();

                    foreach (var x in checkAgainst)
                    {
                        xVal.Add(x.Answer);
                    }

                    int c = 0;

                    foreach (var x in xVal)
                    {
                        foreach (var y in results)
                        {
                            if (x == y.Response)
                            {
                                c++;
                            }
                        }
                        yVal.Add(c);
                        c = 0;
                    }

                    ChartMakerData data = new ChartMakerData();
                    data.xValues = xVal;
                    data.yValues = yVal;
                    data.QuestionName = z.QuestionName;
                    data.ChartType = z.ChartType;
                    charts.ChartData.Add(data);
                }

                else if (z.Include && z.ChartType == ChartType.Table)
                {
                    var results = (from x in reportHandler.SurveyResponses
                                   where x.SurveyQuestionId == z.QuestionID && x.SurveyTypeId == z.SurveyID
                                   select x).ToList();

                    List<String> xVal = new List<string>();

                    foreach (var u in results)
                    {
                        xVal.Add(u.Response);
                    }

                    ChartMakerData data = new ChartMakerData();
                    data.xValues = xVal;
                    data.QuestionName = z.QuestionName;
                    data.ChartType = z.ChartType;
                    charts.ChartData.Add(data);
                }
            }

            /*
            var results = (from x in reportHandler.SurveyResponses
                           where x.SurveyQuestionId == model.QuestionID && x.SurveyTypeId == model.SurveyID
                           select x).ToList();

            var checkAgainst = (from x in reportHandler.SurveyQuestionSuppliedAnswers
                                where x.SurveyQuestionId == model.QuestionID
                                select x).ToList();

            List<int> yVal = new List<int>();
            List<String> xVal = new List<string>();

            foreach (var x in checkAgainst)
            {
                xVal.Add(x.Answer);
            }

            int c = 0;

            foreach (var x in xVal)
            {
                foreach (var y in results)
                {
                    if (x == y.Response)
                    {
                        c++;
                    }
                }
                yVal.Add(c);
                c = 0;
            }

            ChartMaker chart = new ChartMaker();

            foreach (var x in xVal)
            {
                chart.xValues.Add(x);
            }
            foreach (var y in yVal)
            {
                chart.yValues.Add(y);
            }

            return View("BarChart", chart);
            */
            return View("ReportResults", charts);
        }

        public ActionResult ReportResults(ChartMaker charts)
        {
            return View(charts);
        }

        public ActionResult ExportCSV(ChartMaker charts)
        {
            return View();
            /*
            List<String> csv = new List<string>();

            foreach (var x in charts.ChartData)
            {
                csv.Add(x.QuestionName);
            }

            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "Report.csv");
            */
        }
    }
}