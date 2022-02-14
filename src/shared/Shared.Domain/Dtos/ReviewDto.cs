using System;

public class ReviewDto
{
    public Guid Id { get; set; }
    public Guid ArticleId { get; set; }
    public string Reviewer { get; set; }
    public string ReviewContent { get; set; }
}