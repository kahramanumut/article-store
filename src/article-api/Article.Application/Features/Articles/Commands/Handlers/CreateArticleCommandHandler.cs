using System.Threading;
using System.Threading.Tasks;
using Article.Domain;
using MediatR;

public class CreateArticleCommandHandler: ICommandHandler<CreateArticleCommand, ArticleDto>
{
    private readonly ArticleDbContext _dbContext;

    public CreateArticleCommandHandler(ArticleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ArticleDto> Handle(CreateArticleCommand command, CancellationToken cancellationToken)
    {
        Article.Domain.Article newArticle = new Article();
        newArticle.Create(command.Title, command.Author, command.ArticleContent);

        await _dbContext.AddAsync(newArticle);
        await _dbContext.SaveChangesAsync();

        return new ArticleDto { Id = newArticle.Id, Title = newArticle.Title, ArticleContent = newArticle.ArticleContent, Author = newArticle.Author, 
                                PublishDate = newArticle.PublishDate, StarCount = newArticle.StarCount };
    }

}