using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A_ZCamp.Models
{
    public class SurveyQuestionSuppliedAnswer
    {
        public int ID { get; set; }
        [ForeignKey("SurveyQuestion")]
        public int SurveyQuestionID { get; set; }
        public string Answer { get; set; }
    }
}