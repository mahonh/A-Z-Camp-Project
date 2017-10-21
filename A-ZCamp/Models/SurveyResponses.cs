using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A_ZCamp.Models
{
    public class SurveyResponses
    {
        [Key]
        [ForeignKey("SurveyType")]
        public int SurveyTypeID { get; set; }
        [Key]
        [ForeignKey("SurveyQuestion")]
        public int SurveyQuestionID { get; set; }
        public string Response { get; set; }
    }
}