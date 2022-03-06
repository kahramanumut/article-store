using Article.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Infrastructure.EfCore.Repositories
{
    public class ArticleRepositoryAsync : GenericRepositoryAsync<Article.Domain.Article>,IArticleRepositoryAsync
    {
        public ArticleRepositoryAsync(ArticleDbContext dbContext) : base(dbContext)
        {

        }
    }
}
