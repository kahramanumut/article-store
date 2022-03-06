using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Article.Application.Features;
using Article.Application.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class DeleteArticleCommandHandler : Handler<Article.Domain.Article,DeleteArticleCommand, bool>
{
#if false
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
        if (article == null)
            throw new NullReferenceException($"Article could not found, ArticleId: {command.ArticleId}");

        bool existReview = await _reviewService.ExistReviewByArticleId(command.ArticleId.ToString());
        if (existReview)
            throw new Exception("There are one or more review, Article cannot delete");


        _dbContext.Articles.Remove(article);
        await _dbContext.SaveChangesAsync();

        return true;
    } 
#else
    public DeleteArticleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    public override async Task<bool> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _repository.GetByIdAsync(request.ArticleId);
        if (article == null)
            throw new NullReferenceException($"Article could not found, ArticleId: {request.ArticleId}");
        await _repository.DeleteAsync(article);
        await _unitOfWork.Commit(cancellationToken);
        return true;
    }
#endif
}