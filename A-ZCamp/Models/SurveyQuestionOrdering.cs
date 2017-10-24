using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A_ZCamp.Models
{
    public class SurveyQuestionOrdering
    {
        [Key]
        [ForeignKey("SurveyType")]
        [Column(Order = 1)]
        public int SurveyTypeId { get; set; }
        public SurveyType SurveyType { get; set; }
        [Key]
        [ForeignKey("SurveyQuestion")]
        [Column(Order = 2)]
        public int SurveyQuestionId { get; set; }
        public SurveyQuestion SurveyQuestion { get; set; }
        public int Order { get; set; }
    }
}