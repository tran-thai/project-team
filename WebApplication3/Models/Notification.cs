using System;
using System.Collections.Generic;

namespace WebApplication3.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public string? Content { get; set; }

    public DateTime? SentDate { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
