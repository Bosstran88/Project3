namespace Project3.Entity.Request
{
    public class CategoryBlogReq
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public int? pageSize { get; set; }
        public int? pageNumber { get; set; } = 0;
    }
}
