namespace WebApiDemo.DTO
{
    public class TitleDTO
    {
        public int Id { get; set; }

        public string Title1 { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string Type { get; set; } = null!;

        public int PubId { get; set; }

        public int Number { get; set; }

        public decimal Price { get; set; }

        public string? Notes { get; set; }

        public int? Status { get; set; }
    }
}
