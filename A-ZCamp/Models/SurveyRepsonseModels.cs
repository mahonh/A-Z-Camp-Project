using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A_ZCamp.Models
{
    public class SurveyResponse
    {
        [Key]
        public int ResponseId { get; set; }
        [ForeignKey("SurveyType")]
        public int SurveyTypeId { get; set; }
        public virtual SurveyType SurveyType { get; set; }
        [ForeignKey("SurveyQuestion")]
        public int SurveyQuestionId { get; set; }
        public virtual SurveyQuestion SurveyQuestion { get; set; }
        [Required]
        [StringLength(10000, MinimumLength = 1, ErrorMessage = "This field can not be blank")]
        public String Response { get; set; }
    }
    /*
    public class SurveyRankingResponse
    {
        [Key]
        public int SurveyRankingResponseId { get; set; }
        [ForeignKey("SurveyQuestion")]
        public int SurveyQuestionId { get; set; }
        public virtual SurveyQuestion SurveyQuestion { get; set; }
        [ForeignKey("SurveyRespondent")]
        public int SurveyRespondentId { get; set; }
        public virtual SurveyRespondent SurveyRespondent { get; set; }
        public String Choice { get; set; }
        public String Rank { get; set; }
    }

    public class SurveyShortAnswerResponse
    {
        [Key]
        public int SurveyShortAnswerResponseId { get; set; }
        [ForeignKey("SurveyQuestion")]
        public int SurveyQuestionId { get; set; }
        public virtual SurveyQuestion SurveyQuestion { get; set; }
        [ForeignKey("SurveyRespondent")]
        public int SurveyRespondentId { get; set; }
        public virtual SurveyRespondent SurveyRespondent { get; set; }
        public String Answer { get; set; }
    }

    public class SurveyMCResponse
    {
        [Key]
        public int SurveyMCResponseId { get; set; }
        [ForeignKey("SurveyQuestion")]
        public int SurveyQuestionId { get; set; }
        public virtual SurveyQuestion SurveyQuestion { get; set; }
        [ForeignKey("SurveyRespondent")]
        public int SurveyRespondentId { get; set; }
        public virtual SurveyRespondent SurveyRespondent { get; set; }
        public String Choice { get; set; }
    }
    */
}