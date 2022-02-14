using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Review;
using Microsoft.EntityFrameworkCore;

public class DeleteReviewCommandHandler: ICommandHandler<DeleteReviewCommand, bool>
{
    private readonly ReviewDbContext _dbContext;

    public DeleteReviewCommandHandler(ReviewDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeleteReviewCommand command, CancellationToken cancellationToken)
    {
        Review review = await _dbContext.Reviews.Where(x => x.Id == command.ReviewId).FirstOrDefaultAsync(cancellationToken);
        if(review == null)
            throw new NullReferenceException($"Review could not found, Review: {command.ReviewId}");
        
        _dbContext.Reviews.Remove(review);
        await _dbContext.SaveChangesAsync();

        return true;
    }

}