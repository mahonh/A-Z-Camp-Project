using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A_ZCamp.Models
{
    public class SurveyType
    {
        public int ID { get; set; }
        public Survey Survey { get; set; }
        public Boolean Active { get; set; }
    }

    public enum Survey
    {
        PreCamp = 1,
        PostCamp
    }
}