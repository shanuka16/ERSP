//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuizApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_setExam
    {
        public int examId { get; set; }
        public Nullable<System.DateTime> exam_date { get; set; }
        public Nullable<int> exam_fk_stu { get; set; }
        public string exam_name { get; set; }
        public Nullable<int> std_score { get; set; }
        public Nullable<int> exam_fk_cat__id { get; set; }
    
        public virtual student student { get; set; }
    }
}
