using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A_ZCamp.Models
{
    public class SurveyAddViewModel
    {
        public SurveyAddViewModel()
        {
            Surveys = new List<SurveyData>();
        }
        public String SurveyName { get; set; }
        public Survey SurveyType { get; set; }
        public List<SurveyData> Surveys { get; set; }
    }
}