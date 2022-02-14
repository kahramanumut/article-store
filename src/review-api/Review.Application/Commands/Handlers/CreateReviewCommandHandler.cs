using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Review;

public class CreateReviewCommandHandler: ICommandHandler<CreateReviewCommand, ReviewDto>
{
    private readonly ReviewDbContext _dbContext;
    private readonly ArticleService _articleService;

    public CreateReviewCommandHandler(ReviewDbContext dbContext, ArticleService articleService)
    {
        _dbContext = dbContext;
        _articleService = articleService;
    }

    public async Task<ReviewDto> Handle(CreateReviewCommand command, CancellationToken cancellationToken)
    {
        bool article = await _articleService.ExistArticleByArticleId(command.ArticleId.ToString());

        if(!article)
            throw new Exception($"There isn't any article, articleId = {command.ArticleId}");

        Review newReview = new Review();
        newReview.Create(command.ArticleId, command.Reviewer, command.ReviewContent);

        await _dbContext.Reviews.AddAsync(newReview);
        await _dbContext.SaveChangesAsync();

        return new ReviewDto { Id = newReview.Id, ArticleId = command.ArticleId, Reviewer = command.Reviewer, ReviewContent = command.ReviewContent };
    }

}