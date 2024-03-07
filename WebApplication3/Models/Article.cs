using System;
using System.Collections.Generic;

namespace WebApplication3.Models;

public partial class Article
{
    public int ArticleId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public int? AuthorId { get; set; }

    public int? CategoryId { get; set; }

    public virtual User? Author { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
}
