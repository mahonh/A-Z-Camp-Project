using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A_ZCamp.Models
{
    public class SurveyRespondent
    {
        public int ID { get; set; }
        
        public int SurveyTypeID { get; set; }
        [ForeignKey("SurveyTypeID")]
        public SurveyType SurveyType { get; set; }
    }
}