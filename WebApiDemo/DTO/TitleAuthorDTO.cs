namespace WebApiDemo.DTO
{
    public class TitleAuthorDTO
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public int TitleId { get; set; }

        public DateTime PubDate { get; set; }

        public int? Royaltyper { get; set; }

        public byte? Sort { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? Status { get; set; }
    }
}
