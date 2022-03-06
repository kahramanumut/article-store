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
    public class GetAllArticlesQuery : Request<IEnumerable<ArticleDto>>
    {
        public class GetAllArticlesQueryHandler : Handler<Article.Domain.Article, GetAllArticlesQuery, IEnumerable<ArticleDto>>
        {
            public GetAllArticlesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
            {
            }

            public override async Task<IEnumerable<ArticleDto>> Handle(GetAllArticlesQuery request, CancellationToken cancellationToken)
            {
                var articles = await _repository.GetAllAsync();
                var articlesViewModel = _mapper.Map<IEnumerable<ArticleDto>>(articles);
                return articlesViewModel;
            }
        }
    }
}
