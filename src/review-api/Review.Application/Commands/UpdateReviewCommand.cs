using System;
using FluentValidation;

public class UpdateReviewCommand : ICommand<ReviewDto>
{
    public Guid ReviewId { get; set; }
    public string Reviewer { get; set; }
    public string ReviewContent { get; set; }

    public void SetId(Guid id)
    {
        ReviewId = id;
    }
}

public class UpdateReviewValidator : AbstractValidator<UpdateReviewCommand>
{
    public UpdateReviewValidator()
    {
        RuleFor(cmd => cmd.Reviewer).NotEmpty();
        RuleFor(cmd => cmd.ReviewContent).MinimumLength(10);
    }
}