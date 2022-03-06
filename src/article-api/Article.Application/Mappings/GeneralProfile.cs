using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Article.Domain.Article, ArticleDto>().ReverseMap();
            CreateMap<CreateArticleCommand, Article.Domain.Article>();
        }
    }
}
