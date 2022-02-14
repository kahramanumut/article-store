using System;

public class ArticleDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ArticleContent { get; set; }
    public int StarCount { get; set; }
    public DateTime PublishDate { get; set; }
}

