using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A_ZCamp.Models
{
    public class SurveyRankingResponse
    {
        public int Id { get; set; }
        [ForeignKey("SurveyQuestion")]
        public int SurveyQuestionId { get; set; }
        [ForeignKey("SurveyRespondent")]
        public SurveyQuestion SurveyQuestion { get; set; }
        public int SurveyRespondentId { get; set; }
        public SurveyRespondent SurveyRespondent { get; set; }
        public String Choice { get; set; }
        public String Rank { get; set; }
    }
}