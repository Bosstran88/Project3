namespace Project3.Entity.Request
{
    public class CourseSearchReq
    {
        public string? title { get; set; }
        public int? isSale { get; set; }    
        public DateTime? fromTime { get; set; }
        public DateTime? toTime { get; set; }   
        public int? pageSize { get; set; }
        public int? pageNumber { get; set; } = 0;
    }
}
