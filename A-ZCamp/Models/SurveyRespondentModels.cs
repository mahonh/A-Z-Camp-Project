using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace A_ZCamp.Models
{
    public class SurveyRespondent
    {
        [Key]
        public int RespondentId { get; set; }
        public String Email { get; set; }
        public String RID { get; set; }
        public Boolean PreCampComplete { get; set; }
        public Boolean PostCampComplete { get; set; }
        public Boolean OtherComplete { get; set; }
    }
}