﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A_ZCamp.Models
{
    public class SurveyQuestion
    {
        public int ID { get; set; }
        public string Question { get; set; }
        public Boolean Active { get; set; }
    }
}