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
        public int Id { get; set; }
        [ForeignKey("SurveyType")]
        public int SurveyTypeId { get; set; }
        public virtual SurveyType SurveyType { get; set; }
    }
}