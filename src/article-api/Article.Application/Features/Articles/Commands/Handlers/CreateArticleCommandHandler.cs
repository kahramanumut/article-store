using System.Threading;
using System.Threading.Tasks;
using Article.Application.Interfaces;
using AutoMapper;
using MediatR;

public class CreateArticleCommandHandler: IHandler<CreateArticleCommand, ArticleDto>
{
#if true
    protected readonly IUnitOfWork _unitOfWork;
    internal readonly IMapper _mapper;
    public CreateArticleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ArticleDto> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var article = _mapper.Map<Article.Domain.Article>(request);

        await _unitOfWork.Repository<Article.Domain.Article>().AddAsync(article);
        await _unitOfWork.Commit(cancellationToken);

        var articleDto = _mapper.Map<ArticleDto>(article);
        return articleDto;
            //new ArticleDto();
    } 
#else
    private readonly ArticleDbContext _dbContext;

    public CreateArticleCommandHandler(ArticleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ArticleDto> Handle(CreateArticleCommand command, CancellationToken cancellationToken)
    {
        Domain.Article.Article newArticle = new Domain.Article.Article();
        newArticle.Create(command.Title, command.Author, command.ArticleContent);

        await _dbContext.AddAsync(newArticle);
        await _dbContext.SaveChangesAsync();

        return new ArticleDto { 
            Id = newArticle.Id, 
            Title = newArticle.Title, 
            ArticleContent = newArticle.ArticleContent,
            Author = newArticle.Author, 
            PublishDate = newArticle.PublishDate, 
            StarCount = newArticle.StarCount 
        };
    }

#endif
}