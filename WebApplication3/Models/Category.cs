using System;
using System.Collections.Generic;

namespace WebApplication3.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public int? FacultyId { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual Faculty? Faculty { get; set; }
}
