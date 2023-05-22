namespace Project3.Entity.Dto
{
    public class VQuestionRes
    {
        public long? id { get; set; } 
        public string? nameQuestion { get; set; }   
        public long? ExamId { get; set; }
        public DateTime? createAt { get; set; }
        public DateTime? updateAt { get; set; }
    }
}
