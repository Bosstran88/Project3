namespace Project3.Entity.Request
{
    public class AddHistoryExamReq
    {
        public long? Id { get; set; }
        public long? studentId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
