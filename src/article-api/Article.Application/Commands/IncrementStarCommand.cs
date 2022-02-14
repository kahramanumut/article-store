using System;

public class IncrementStarCommand : ICommand<int>
{
    public Guid ArticleId { get; set; }
}