using Microsoft.EntityFrameworkCore;

namespace Article.Infrastructure.EfCore
{
    public class ArticleDbContext : DbContext
    {
        //Migration and DB update
        //dotnet ef migrations add MigrationName -s ../Article.WebApi/ -o EFCore/Migrations --namespace EFCore/Migrations/AricleMig
        //dotnet ef database update -s ../Article.WebApi/

        public ArticleDbContext()
        {
            
        }

        public ArticleDbContext(DbContextOptions<ArticleDbContext> options) : base(options)
        { 

        }

        public DbSet<Domain.Article> Articles { get; set; }

    }

}
