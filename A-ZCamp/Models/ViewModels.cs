using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A_ZCamp.Models
{
    public class SurveyIndexViewModel
    {

    }

    public class SurveyLandingViewModel
    {
        public String Email { get; set; }
        public Boolean PreCampDone { get; set; }
        public Boolean PostCampDone { get; set; }
        public Boolean OtherDone { get; set; }
        public Boolean ShowOther { get; set; }
        public String OtherName { get; set; }
    }
}