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
        public int ID { get; set; }
        public int SurveyQuestionID { get; set; }
        [ForeignKey("SurveyQuestionID")]
        public SurveyQuestion SurveyQuestion { get; set; }
        public int SurveyRespondentID { get; set; }
        [ForeignKey("SurveyRespondentID")]
        public SurveyRespondent SurveyRespondent { get; set; }
        public String Choice { get; set; }
        public String Rank { get; set; }
    }
}