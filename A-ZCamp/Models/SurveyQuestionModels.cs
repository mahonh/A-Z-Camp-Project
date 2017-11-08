using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace A_ZCamp.Models
{
    public class SurveyType
    {
        [Key]
        public int SurveyTypeId { get; set; }
        public Survey Survey { get; set; }
        public Boolean Active { get; set; }
        public String Name { get; set; }
    }

    public enum Survey
    {
        PreCamp = 1,
        PostCamp
    }

    public class SurveyQuestionOrdering
    {
        [Key]
        [ForeignKey("SurveyType")]
        [Column(Order = 1)]
        public int SurveyTypeId { get; set; }
        public virtual SurveyType SurveyType { get; set; }
        [Key]
        [ForeignKey("SurveyQuestion")]
        [Column(Order = 2)]
        public int SurveyQuestionId { get; set; }
        public virtual SurveyQuestion SurveyQuestion { get; set; }
        public int Order { get; set; }
    }

    public class SurveyQuestion
    {
        [Key]
        public int SurveyQuestionId { get; set; }
        public QuestionType QuestionType { get; set; }
        public string Question { get; set; }
        public virtual ICollection<SurveyQuestionSuppliedAnswer> SuppliedAnswers { get; set; }
        public Boolean Active { get; set; }
    }

    public enum QuestionType
    {
        ShortAnswer = 1,
        MultipleChoice,
        MatrixRanking
    }

    public class SurveyQuestionSuppliedAnswer
    {
        [Key]
        public int SurveyQuestionSuppliedAnswerId { get; set; }
        [ForeignKey("SurveyQuestion")]
        public int SurveyQuestionId { get; set; }
        public virtual SurveyQuestion SurveyQuestion { get; set; }
        public string Answer { get; set; }
    }
    /*
    public class SurveyRespondent
    {
        [Key]
        public int SurveyRespondentId { get; set; }
        [ForeignKey("SurveyType")]
        public int SurveyTypeId { get; set; }
        public virtual SurveyType SurveyType { get; set; }
    }
    */
}