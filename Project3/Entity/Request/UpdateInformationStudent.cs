namespace Project3.Entity.Request
{
    public class UpdateInformationStudent
    {
        public long Id { get; set; }

        public string? FullName { get; set; }

        public DateTime? DateBirth { get; set; }

        public string? WasBorn { get; set; }

        public string? Email { get; set; }

        public string? IdentityCard { get; set; }
        public DateTime? StartCard { get; set; }

        public DateTime? EndCard { get; set; }

        public string? FromCard { get; set; }

        public string? Status { get; set; }
    }
}
