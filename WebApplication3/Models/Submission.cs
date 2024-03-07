using System;
using System.Collections.Generic;

namespace WebApplication3.Models;

public partial class Submission
{
    public int SubmissionId { get; set; }

    public int? UserId { get; set; }

    public DateTime? SubmissionDate { get; set; }

    public string? Status { get; set; }

    public string? CommentSubContent { get; set; }

    public DateTime? CommentSubDate { get; set; }

    public string? CommentFeedbackContent { get; set; }

    public DateTime? CommentFeedbackDate { get; set; }

    public string? FileSubName { get; set; }

    public DateTime? FileUploadDate { get; set; }

    public virtual User? User { get; set; }
}
