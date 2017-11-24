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
        public String Response { get; set; }
    }
}