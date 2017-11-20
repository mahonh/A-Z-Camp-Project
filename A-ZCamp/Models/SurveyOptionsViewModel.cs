using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A_ZCamp.Models
{
    public class SurveyOptionsViewModel
    {
        public SurveyOptionsViewModel()
        {
            AllSurveys = new List<SurveyData>();
        }
        public List<SurveyData> AllSurveys { get; set; }
    }

    public class SurveyQuestionAssignment
    {
        public SurveyQuestionAssignment()
        {
            QuestionData = new List<QuestionAssignmentData>();
        }
        public List<QuestionAssignmentData> QuestionData { get; set; }
    }

    public class QuestionAssignmentData
    {
        public String QuestionName { get; set; }
        public QuestionType QuestionType { get; set; }
        public Boolean ChangeAssignment { get; set; }
        public int SurveyID { get; set; }
        public int QuestionID { get; set; }
    }
}