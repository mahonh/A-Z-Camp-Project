using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A_ZCamp.Models
{
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

    public class SurveyData
    {
        public String SurveyName { get; set; }
        public Survey SurveyType { get; set; }
        public int Sid { get; set; }
    }
}