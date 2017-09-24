using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A_ZCamp.Models
{
    public class PreSurvey
    {
        public int ID { get; set; }
        public String Email { get; set; }
        public String WhatDoYouExpect { get; set; }
        public String AreYouLookingForward { get; set; }
    }
}