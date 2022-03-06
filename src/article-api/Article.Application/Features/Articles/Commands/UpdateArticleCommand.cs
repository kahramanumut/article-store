using System;
using System.Threading;
using System.Threading.Tasks;
using Article.Application.Features;
using Article.Application.Interfaces;
using AutoMapper;
using FluentValidation;

public class UpdateArticleCommand : IRequest<ArticleDto>
{
    public Guid Id { get; private set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ArticleContent { get; set; }

    public void SetId(Guid id)
    {
        Id = id;
    }

    public class UpdateArticleCommandHandler : Handler<Article.Domain.Article, UpdateArticleCommand, ArticleDto>
    {
        public UpdateArticleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public override async Task<ArticleDto> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _repository.GetByIdAsync(request.Id);
            if (article == null)
                throw new NullReferenceException($"Article could not found, ArticleId: {request.Id}");
            await _repository.UpdateAsync(article);
            await _unitOfWork.Commit(cancellationToken);
            return _mapper.Map<ArticleDto>(article);
        }
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