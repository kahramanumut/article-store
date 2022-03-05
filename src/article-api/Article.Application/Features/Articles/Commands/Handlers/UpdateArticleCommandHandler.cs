using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Article;
using Microsoft.EntityFrameworkCore;

public class UpdateArticleCommandHandler: ICommandHandler<UpdateArticleCommand, ArticleDto>
{
    private readonly ArticleDbContext _dbContext;

    public UpdateArticleCommandHandler(ArticleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ArticleDto> Handle(UpdateArticleCommand command, CancellationToken cancellationToken)
    {
        Article article = await _dbContext.Articles.Where(x => x.Id == command.Id).FirstOrDefaultAsync(cancellationToken);
        if(article == null)
            throw new NullReferenceException($"Article could not found, ArticleId: {command.Id}");
        
        article.Update(command.Title, command.Author, command.ArticleContent);
        await _dbContext.SaveChangesAsync();

        return new ArticleDto { Id = article.Id, Title = article.Title, ArticleContent = article.ArticleContent, Author = article.Author, 
                                PublishDate = article.PublishDate, StarCount = article.StarCount };
    }

}