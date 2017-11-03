using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace A_ZCamp.Models
{
    public class SMPreCamp
    {
        public int Id { get; set; }
        [DisplayName("Are you looking forward to camp?")]
        [Required(ErrorMessage = "Uh-oh! Looks like you left this one blank.")]
        public String Question1 { get; set; }
        [DisplayName("How much programming experience do you have?")]
        [Required(ErrorMessage = "Uh-oh! Looks like you left this one blank.")]
        public String Question2 { get; set; }
        [DisplayName("List any extra computer related experiences you currently have at your school:")]
        [Required(ErrorMessage = "Uh-oh! Looks like you left this one blank.")]
        public String Question3 { get; set; }
        [DisplayName("What's your favorite language?")]
        [Required(ErrorMessage = "Uh-oh! Looks like you left this one blank.")]
        public String Question4 { get; set; }
        [DisplayName("Do you have any additional comments?")]
        [Required(ErrorMessage = "Uh-oh! Looks like you left this one blank.")]
        public String Question5 { get; set; }
    }

    public class SMPostCamp
    {
        public int Id { get; set; }
        [DisplayName("Did you enjoy camp?")]
        [Required(ErrorMessage = "Uh-oh! Looks like you left this one blank.")]
        public String Question1 { get; set; }
        [DisplayName("Do you think you are more familar with programming now? How so?")]
        [Required(ErrorMessage = "Uh-oh! Looks like you left this one blank.")]
        public String Question2 { get; set; }
        [DisplayName("What was your favorite part of camp?")]
        [Required(ErrorMessage = "Uh-oh! Looks like you left this one blank.")]
        public String Question3 { get; set; }
        [DisplayName("Do you think you'll attend next year?")]
        [Required(ErrorMessage = "Uh-oh! Looks like you left this one blank.")]
        public String Question4 { get; set; }
        [DisplayName("Do you have any additional comments?")]
        [Required(ErrorMessage = "Uh-oh! Looks like you left this one blank.")]
        public String Question5 { get; set; }
    }

    public class SMReporting
    {
        public List<SMPreCamp> PreCampData { get; set; }
        public List<SMPostCamp> PostCampData { get; set; }
    }
}