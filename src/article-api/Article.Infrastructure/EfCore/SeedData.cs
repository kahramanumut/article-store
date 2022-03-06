using System;
using System.Linq;
using Article.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static void SeedSampleData(ArticleDbContext dbContext)
    {
        dbContext.Database.Migrate();
        
        if (!dbContext.Articles.Any())
        {
            Article.Domain.Article article1 = new Article.Domain.Article();
            article1.Create("4 Satır Kod ile Net Core’da OData Web Api Oluşturma", "Umut Kahraman", "ODATA");
            dbContext.Articles.Add(article1);

            Article.Domain.Article article2 = new Article.Domain.Article();
            article2.Create("C# Entity Framework 6.0’da Linq İle Tüyolar", "Bora Kaşmer", "LINQ");
            dbContext.Articles.Add(article2);

            Article.Domain.Article article3 = new Article.Domain.Article();
            article3.Create("Upgrading a 20 year old University Project to .NET 6 with dotnet-upgrade-assistant", "Scott Hanselman", ".NET 6 Migration");
            dbContext.Articles.Add(article3);

            Article.Domain.Article article4 = new Article.Domain.Article();
            article4.Create("AWS Serverless Web Sayfası Yayınlama ve Dinamik İletişim Formu Oluşturma", "Serkan Bingöl", "AWS Serverless");
            dbContext.Articles.Add(article4);

            dbContext.SaveChanges();
        }
    }
}
