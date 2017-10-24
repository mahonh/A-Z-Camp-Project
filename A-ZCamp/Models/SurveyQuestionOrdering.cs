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
        
        public int SurveyTypeID { get; set; }
        [Key]
        [ForeignKey("SurveyTypeID")]
        public SurveyType SurveyType { get; set; }
        
        public int SurveyQuestionID { get; set; }
        [Key]
        [ForeignKey("SurveyQuestionID")]
        public SurveyQuestion SurveyQuestion { get; set; }
        public int Order { get; set; }
    }
}