using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Article;
using Microsoft.EntityFrameworkCore;

public class IncrementArticleStarCommandHandler: ICommandHandler<IncrementStarCommand, int>
{
    private readonly ArticleDbContext _dbContext;

    public IncrementArticleStarCommandHandler(ArticleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(IncrementStarCommand command, CancellationToken cancellationToken)
    {
        Article article = await _dbContext.Articles.Where(x => x.Id == command.ArticleId).FirstOrDefaultAsync(cancellationToken);
        if(article == null)
            throw new NullReferenceException($"Article could not found, ArticleId: {command.ArticleId}");
        
        article.IncrementStartCount();
        await _dbContext.SaveChangesAsync();

        return article.StarCount;
    }

}