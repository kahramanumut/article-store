using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Review;
using Microsoft.EntityFrameworkCore;

public class UpdateReviewCommandHandler: ICommandHandler<UpdateReviewCommand, ReviewDto>
{
    private readonly ReviewDbContext _dbContext;

    public UpdateReviewCommandHandler(ReviewDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ReviewDto> Handle(UpdateReviewCommand command, CancellationToken cancellationToken)
    {
        Review review = await _dbContext.Reviews.Where(x => x.Id == command.ReviewId).FirstOrDefaultAsync(cancellationToken);
        if(review == null)
            throw new NullReferenceException($"Review could not found, ArticleId: {command.ReviewId}");
        
        review.Update(command.Reviewer, command.ReviewContent);
        await _dbContext.SaveChangesAsync();

        return new ReviewDto { Id = review.Id, ArticleId = review.ArticleId, Reviewer = review.Reviewer, ReviewContent = review.ReviewContent };
    }

}