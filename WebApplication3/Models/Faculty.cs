using System;
using System.Collections.Generic;

namespace WebApplication3.Models;

public partial class Faculty
{
    public int FacultyId { get; set; }

    public string? FacultyName { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual User? User { get; set; }
}
