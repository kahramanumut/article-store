using System;
using FluentValidation;

public class CreateReviewCommand : ICommand<ReviewDto>
{
    public Guid ArticleId { get; set; }
    public string Reviewer { get; set; }
    public string ReviewContent { get; set; }
}

public class CreateReviewValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewValidator()
    {
        RuleFor(cmd => cmd.Reviewer).NotEmpty();
        RuleFor(cmd => cmd.ReviewContent).MinimumLength(10);
    }
}