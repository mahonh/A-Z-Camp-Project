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
        public int SurveyTypeID { get; set; }
        [Key]
        [ForeignKey("SurveyQuestions")]
        public int SurveyQuestionID { get; set; }
        public int Order { get; set; }
    }
}