using System;
using System.Collections.Generic;

namespace WebApiDemo.Models;

public partial class Author
{
    public int Id { get; set; }

    public string AuLname { get; set; } = null!;

    public string AuFname { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int? Status { get; set; }

    public virtual ICollection<TitleAuthor> TitleAuthors { get; set; } = new List<TitleAuthor>();
}
