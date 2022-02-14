using System;

public class DeleteArticleCommand : ICommand<bool>
{
    public Guid ArticleId { get; set; }
}