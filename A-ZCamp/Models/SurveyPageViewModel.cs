using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace A_ZCamp.Models
{
    public class SurveyPageViewModel
    {
        public SurveyPageViewModel()
        {
            QuestionData = new List<QuestionData>();
        }
        public List<QuestionData> QuestionData { get; set; }
        public String SurveyName { get; set; }
        /*
        public String ShortAnswerResponse { get; set; }
        public String MCAnswerReponse { get; set; }
        public String RankingResponse { get; set; }
        */
    }

    public class QuestionData
    {
        public int Sid { get; set; }
        public int Qid { get; set; }
        public String Question { get; set; }
        public QuestionType QType { get; set; }
        public List<String> QSupAnswers { get; set; }
        public String UserResponse { get; set; }
    }
    /*
    public class ResponseData
    {
        public String UserResponse { get; set; }
    }
    */
}