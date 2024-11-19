namespace WebApiDemo.DTO
{
    public class PublisherDTO
    {
        public int Id { get; set; }

        public string PubName { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Address { get; set; } = null!;

        public int? Status { get; set; }
    }
}
