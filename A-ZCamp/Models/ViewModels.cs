using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;
using System.Web;

namespace A_ZCamp.Models
{
    //View Model used with the Survey Landing Page
    public class SurveyLandingViewModel
    {
        [EmailAddress]
        public String Email { get; set; }
        public String RID { get; set; }
        public Boolean PreCampDone { get; set; }
        public Boolean PostCampDone { get; set; }
        public Boolean OtherDone { get; set; }
        public Boolean ShowOther { get; set; }
        public String OtherName { get; set; }
    }

    //View Model used for the Edit Questions page and Specific Questions page
    public class QuestionOverallViewModel
    {
        public QuestionOverallViewModel()
        {
            allQuestions = new List<QuestionData>();
        }
        public List<QuestionData> allQuestions { get; set; }
    }

    //View Model for Add Survey Question page
    public class SurveyAddQuestionViewModel
    {
        public SurveyAddQuestionViewModel()
        {
            Surveys = new List<SurveyData>();
            SuppliedAnswersQuestionToSumbit = new List<String>();

            for (int i = 0; i < 3; i++)
            {
                SuppliedAnswersQuestionToSumbit.Add("");
            }

        }
        public List<SurveyData> Surveys { get; set; }
        public String QuestionToSubmit { get; set; }
        public QuestionType QuestionTypeToSubmit { get; set; }
        public int SurveyID { get; set; }
        public List<String> SuppliedAnswersQuestionToSumbit { get; set; }
    }

    //Object for various view models
    public class SurveyData
    {
        public String SurveyName { get; set; }
        public Survey SurveyType { get; set; }
        public int Sid { get; set; }
        public Boolean Active { get; set; }
    }

    //View Model for Add Survey Page
    public class SurveyAddViewModel
    {
        public SurveyAddViewModel()
        {
            Surveys = new List<SurveyData>();
        }
        public String SurveyName { get; set; }
        public Survey SurveyType { get; set; }
        public List<SurveyData> Surveys { get; set; }
    }

    //View Model for Survey Options page
    public class SurveyOptionsViewModel
    {
        public SurveyOptionsViewModel()
        {
            AllSurveys = new List<SurveyData>();
        }
        public List<SurveyData> AllSurveys { get; set; }
    }

    //View Model for managing questions to survey relations
    public class SurveyQuestionAssignment
    {
        public SurveyQuestionAssignment()
        {
            QuestionData = new List<QuestionAssignmentData>();
        }
        public List<QuestionAssignmentData> QuestionData { get; set; }
    }

    //Object for SurveyQuestionAssignment View Model
    public class QuestionAssignmentData
    {
        public String QuestionName { get; set; }
        public QuestionType QuestionType { get; set; }
        public Boolean ChangeAssignment { get; set; }
        public int SurveyID { get; set; }
        public int QuestionID { get; set; }
    }

    //View Model for All Survey pages
    public class SurveyPageViewModel
    {
        public SurveyPageViewModel()
        {
            QuestionData = new List<QuestionData>();
        }
        public List<QuestionData> QuestionData { get; set; }
        public String SurveyName { get; set; }
        public String ID { get; set; }
        public Survey SurveyType { get; set; }
        public Boolean SurveySubmitted { get; set; }
    }

    //Object used for Survey Pages and other view models
    public class QuestionData
    {
        public int Sid { get; set; }
        public int Qid { get; set; }
        public String Question { get; set; }
        public QuestionType QType { get; set; }
        public List<String> QSupAnswers { get; set; }
        public String UserResponse { get; set; }
    }

    //View model for Questions Options page
    public class SurveyQuestionOptionsViewModel
    {
        public SurveyQuestionOptionsViewModel()
        {
            AllSurveyQuestions = new List<OptionsData>();
        }
        public List<OptionsData> AllSurveyQuestions { get; set; }
    }

    //Object for Questions options
    public class OptionsData
    {
        public int Qid { get; set; }
        public int Sid { get; set; }
        public String SurveyName { get; set; }
        public String Question { get; set; }
        public QuestionType QuestionType { get; set; }
        public Boolean Active { get; set; }
        public int Ordering { get; set; }
    }

    public class ReportDropDown
    {
        public Survey SurveyType { get; set; }
        public int SurveyID { get; set; }
        public QuestionType QuestionType { get; set; }
        public int QuestionID { get; set; }
    }

    public class ChartMaker
    {
        public ChartMaker()
        {
            ChartData = new List<ChartMakerData>();
        }
        public List<ChartMakerData> ChartData { get; set; }
    }

    public class ChartMakerData
    {
        public ChartMakerData()
        {
            yValues = new List<int>();
            xValues = new List<string>();
        }
        public String QuestionName { get; set; }
        public ChartType ChartType { get; set; }
        public List<int> yValues { get; set; }
        public List<String> xValues { get; set; }
    }

    public class SurveyReportsViewModel
    {
        public SurveyReportsViewModel()
        {
            Surveys = new List<SelectListItem>();
            DataToRun = new List<SurveyReportsData>();
        }
        public String SurveyChoice { get; set; }
        public IEnumerable<SelectListItem> Surveys { get; set; }
        public List<SurveyReportsData> DataToRun { get; set; }
    }

    public class SurveyReportsData
    {
        public int SurveyID { get; set; }
        public int QuestionID { get; set; }
        public QuestionType QuestionType { get; set; }
        public String QuestionName { get; set; }
        public Boolean Include { get; set; }
        public ChartType ChartType { get; set; }
    }

    public enum ChartType
    {
        BarChart = 0,
        PieChart,
        Table
    }
}