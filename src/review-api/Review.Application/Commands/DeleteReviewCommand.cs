using System;

public class DeleteReviewCommand : ICommand<bool>
{
    public Guid ReviewId { get; set; }
}