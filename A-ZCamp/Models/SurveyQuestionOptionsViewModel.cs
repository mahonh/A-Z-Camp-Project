using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A_ZCamp.Models
{
    public class SurveyQuestionOptionsViewModel
    {
        public SurveyQuestionOptionsViewModel()
        {
            AllSurveyQuestions = new List<OptionsData>();
        }
        public List<OptionsData> AllSurveyQuestions { get; set; }
    }

    public class OptionsData
    {
        public int Qid { get; set; }
        public String SurveyName { get; set; }
        public String Question { get; set; }
        public QuestionType QuestionType { get; set; }
        public Boolean Active { get; set; }
        public int Ordering { get; set; }
    }
}