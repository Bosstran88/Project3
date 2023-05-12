namespace Project3.Models
{
    public partial class Question
    {
        public long Id { get; set; }
        public long? ExamId { get; set; }
        public string? NameQuestion { get; set; }
        public int? IsDelete { get; set; }
        public DateTime? UpdateAt{get;set;}
        public virtual Exam? Exams { get; set; }
        public virtual ICollection<AnswerQuestion> Answers { get; set; } = new List<AnswerQuestion>();
    }
}
