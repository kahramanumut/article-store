using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Article.Application.Features;
using Article.Application.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class UpdateArticleCommandHandler : Handler<Article.Domain.Article,UpdateArticleCommand, ArticleDto>
{
#if true
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

#else
    private readonly ArticleDbContext _dbContext;

    public UpdateArticleCommandHandler(ArticleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ArticleDto> Handle(UpdateArticleCommand command, CancellationToken cancellationToken)
    {
        Article.Domain.Article article = await _dbContext.Articles.Where(x => x.Id == command.Id).FirstOrDefaultAsync(cancellationToken);
        if(article == null)
            throw new NullReferenceException($"Article could not found, ArticleId: {command.Id}");
        
        article.Update(command.Title, command.Author, command.ArticleContent);
        await _dbContext.SaveChangesAsync();

        return new ArticleDto { Id = article.Id, Title = article.Title, ArticleContent = article.ArticleContent, Author = article.Author, 
                                PublishDate = article.PublishDate, StarCount = article.StarCount };
    }
#endif
}