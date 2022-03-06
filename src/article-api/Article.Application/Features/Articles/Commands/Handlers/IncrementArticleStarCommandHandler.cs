using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Article.Application.Features;
using Article.Application.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class IncrementArticleStarCommandHandler : Handler<Article.Domain.Article,IncrementStarCommand, int>
{
#if true
    public IncrementArticleStarCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    public override async Task<int> Handle(IncrementStarCommand request, CancellationToken cancellationToken)
    {
        var article = await _repository.GetByIdAsync(request.ArticleId);
        if (article == null)
            throw new NullReferenceException($"Article could not found, ArticleId: {request.ArticleId}");
        article.IncrementStarCount();
        await _unitOfWork.Commit(cancellationToken);
        return article.StarCount;
    }

#else
    private readonly ArticleDbContext _dbContext;

    public IncrementArticleStarCommandHandler(ArticleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(IncrementStarCommand command, CancellationToken cancellationToken)
    {
        Article article = await _dbContext.Articles.Where(x => x.Id == command.ArticleId).FirstOrDefaultAsync(cancellationToken);
        if (article == null)
            throw new NullReferenceException($"Article could not found, ArticleId: {command.ArticleId}");

        article.IncrementStartCount();
        await _dbContext.SaveChangesAsync();

        return article.StarCount;
    } 
#endif
}