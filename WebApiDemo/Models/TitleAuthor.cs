using System;
using System.Collections.Generic;

namespace WebApiDemo.Models;

public partial class TitleAuthor
{
    public int Id { get; set; }

    public int AuthorId { get; set; }

    public int TitleId { get; set; }

    public DateTime PubDate { get; set; }

    public int? Royaltyper { get; set; }

    public byte? Sort { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? Status { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual Title Title { get; set; } = null!;
}
