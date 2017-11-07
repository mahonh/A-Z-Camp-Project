using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A_ZCamp.Models
{
    public class SurveyPageViewModel
    {
        public SurveyPageViewModel()
        {
            List<String> fruits = new List<String> { "Orange", "Apple", "Chocolate" };
            List<String> games = new List<String> { "Mario", "Zelda", "Donkey Kong" };

            QuestionData = new List<QuestionData> {
                new QuestionData() { Question = "How are you?", Qid = 1, QType = QuestionType.ShortAnswer},
                new QuestionData() { Question = "What do you think so far?", Qid = 2, QType = QuestionType.ShortAnswer},
                new QuestionData() { Question = "Does it work?", Qid = 3, QType = QuestionType.ShortAnswer},
                new QuestionData() { Question = "What's your favorite fruit?", QSupAnswers = fruits, QType = QuestionType.MultipleChoice},
                new QuestionData() { Question = "What's your favorite game?", QSupAnswers = games, QType = QuestionType.MultipleChoice} };
        }
        public List<QuestionData> QuestionData { get; set; }
        /*
        public String ShortAnswerResponse { get; set; }
        public String MCAnswerReponse { get; set; }
        public String RankingResponse { get; set; }
        */
    }

    public class QuestionData
    {
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