using Article.Application.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Article.Application.Features.Articles.Queries
{
    public class GetArticleByIdQuery : Request<ArticleDto>
    {
        public Guid Id { get; set; }

        public class GetArticleByIdQueryHandler : Handler<Article.Domain.Article, GetArticleByIdQuery, ArticleDto>
        {
            public GetArticleByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
            {
            }

            public override async Task<ArticleDto> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
            {
                var article = await _repository.GetByIdAsync(request.Id);
                if (article == null)
                    throw new NullReferenceException($"Article could not found, ArticleId: {request.Id}");
                return _mapper.Map<ArticleDto>(article); 
            }
        }
    }
}
