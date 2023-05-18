namespace Project3.Entity.Dto
{
    public class VHistoryExamOne
    {
        public long? Id { get; set; }
        public long? ExamId { get; set; }

        public long? InformationStudentsId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
