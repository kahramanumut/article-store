using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Article;
using Microsoft.EntityFrameworkCore;

public class DeleteArticleCommandHandler: ICommandHandler<DeleteArticleCommand, bool>
{
    private readonly ArticleDbContext _dbContext;
    private readonly ReviewService _reviewService;

    public DeleteArticleCommandHandler(ArticleDbContext dbContext, ReviewService reviewService)
    {
        _dbContext = dbContext;
        _reviewService = reviewService;
    }

    public async Task<bool> Handle(DeleteArticleCommand command, CancellationToken cancellationToken)
    {
        Article article = await _dbContext.Articles.Where(x => x.Id == command.ArticleId).FirstOrDefaultAsync(cancellationToken);
        if(article == null)
            throw new NullReferenceException($"Article could not found, ArticleId: {command.ArticleId}");
        
        bool existReview = await _reviewService.ExistReviewByArticleId(command.ArticleId.ToString());
        if(existReview)
            throw new Exception("There are one or more review, Article cannot delete");
        
        
        _dbContext.Articles.Remove(article);
        await _dbContext.SaveChangesAsync();

        return true;
    }

}