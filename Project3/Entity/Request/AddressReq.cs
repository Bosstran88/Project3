namespace Project3.Entity.Request
{
    public class AddressReq
    {
        public string? Name { get; set; }
        public string? Addresses { get; set; }
        public int? pageSize { get; set; }
        public int? pageNumber { get; set; } = 0;

    }
}
