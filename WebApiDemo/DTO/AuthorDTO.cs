namespace WebApiDemo.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }

        public string AuLname { get; set; } = null!;

        public string AuFname { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Address { get; set; } = null!;

        public int? Status { get; set; }
    }
}
