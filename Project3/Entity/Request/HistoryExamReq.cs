namespace Project3.Entity.Request
{
    public class HistoryExamReq
    {
        public long? ExamId { get; set; }

        public long? InformationStudentsId { get; set; }

        public DateTime? StartTime { get; set; }
        
        public DateTime? EndTime { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
