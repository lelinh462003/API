using System;
using System.Collections.Generic;

namespace WebApiDemo.Models;

public partial class Title
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

    public virtual Publisher Pub { get; set; } = null!;

    public virtual ICollection<TitleAuthor> TitleAuthors { get; set; } = new List<TitleAuthor>();
}
