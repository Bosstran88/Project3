namespace Project3.Entity.Request
{
    public class AddQuestionReq
    {
        public long? Id { get; set; }
        public long? ExamId { get; set; }
        public string? NameQuestion { get; set; }
    }
}
