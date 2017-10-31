using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A_ZCamp.Models
{
    public class SurveyPageViewModel
    {
        public List<QuestionType> SurveyQuestionType { get; set; }
        public List<String> SurveyQuestion { get; set; }
        public List<String> SuppliedAnswers { get; set; }
        public List<String> ShortAnswer { get; set; }
        public List<String> MCAnswer { get; set; }

        /*
        public String ShortAnswerResponse { get; set; }
        public String MCAnswerReponse { get; set; }
        public String RankingResponse { get; set; }
        */
    }
}