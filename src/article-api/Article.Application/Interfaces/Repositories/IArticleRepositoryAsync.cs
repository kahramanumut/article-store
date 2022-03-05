using Article.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Article.Application.Interfaces.Repositories
{
    public interface IArticleRepositoryAsync : IGenericRepositoryAsync<Article>
    {
    }
}