using System;

public class IncrementStarCommand : IRequest<int>
{
    public Guid ArticleId { get; set; }
}