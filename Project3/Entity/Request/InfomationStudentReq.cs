namespace Project3.Entity.Request
{
    public class InfomationStudentReq
    {
        public string? fullName { get; set; }
        public string? idCardStudent { get; set; }
        public string? status { get; set; }
        public DateTime? created_From { get; set; }
        public DateTime? Created_To { get; set; } = DateTime.Now;
        public int? pageSize { get; set; }
        public int? pageNumber { get; set; } = 0;
    }
}
