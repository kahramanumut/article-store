using FluentValidation;

public class CreateArticleCommand : ICommand<ArticleDto>
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ArticleContent { get; set; }
}

public class CreateArticleValidator : AbstractValidator<CreateArticleCommand>
{
    public CreateArticleValidator()
    {
        RuleFor(cmd => cmd.Author).NotEmpty();
        RuleFor(cmd => cmd.Title).NotEmpty();
        RuleFor(cmd => cmd.ArticleContent).MinimumLength(10);
    }
}