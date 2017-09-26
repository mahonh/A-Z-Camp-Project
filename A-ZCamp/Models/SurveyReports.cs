using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A_ZCamp.Models
{
    public class SurveyReports
    {
        public List<PreSurvey> PreSurveys { get; set; }
        public List<PostSurvey> PostSurveys { get; set; }
    }
}