using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace A_ZCamp.Models
{
    public class REPORTVIEWMODEL
    {
        public REPORTVIEWMODEL()
        {
            reportData = new List<REPORTDATA>();
        }
        public List<REPORTDATA> reportData;
    }

    public class REPORTDATA
    {
        public String Survey { get; set; }
        public String Question { get; set; }
        public String Response { get; set; }
    }
}