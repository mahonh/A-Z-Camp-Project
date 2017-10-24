using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A_ZCamp.Models
{
    public class SurveyShortAnswerResponse
    {
        public int Id { get; set; }
        [ForeignKey("SurveyQuestion")]
        public int SurveyQuestionId { get; set; }
        public SurveyQuestion SurveyQuestion { get; set; }
        [ForeignKey("SurveyRespondent")]
        public int SurveyRespondentId { get; set; }
        public SurveyRespondent SurveyRespondent { get; set; }
        public String Answer { get; set; }
    }
}