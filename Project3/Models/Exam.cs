namespace Project3.Models
{
    public partial class Exam
    {
        public long Id { get; set; }
        public string? NameExam { get; set; }
        public int? LimitTime { get; set; }
        public int? IsDelete { get; set; }
        public DateTime? CreateAt { get; set; }
        public long? CreateId { get; set; }
        public DateTime? UpdateAt { get; set; }
        public long? UpdateId { get; set; }  
        public virtual User? Users { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
