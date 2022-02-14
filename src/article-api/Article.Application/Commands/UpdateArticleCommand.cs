using System;
using FluentValidation;

public class UpdateArticleCommand : ICommand<ArticleDto>
{
    public Guid Id { get; private set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ArticleContent { get; set; }

    public void SetId(Guid id)
    {
        Id = id;
    }
}

public class UpdateArticleValidator : AbstractValidator<UpdateArticleCommand>
{
    public UpdateArticleValidator()
    {
        RuleFor(cmd => cmd.Author).NotEmpty();
        RuleFor(cmd => cmd.Title).NotEmpty();
        RuleFor(cmd => cmd.ArticleContent).MinimumLength(10);
    }
}