using System;

public class DeleteArticleCommand : IRequest<bool>
{
    public Guid ArticleId { get; set; }
}