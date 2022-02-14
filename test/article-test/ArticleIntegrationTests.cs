using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Article;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

public class ArticleIntegrationTests
{
    public IServiceProvider _serviceProvider { get; set; }
    public ArticleDbContext _dbContext { get; set; }
    public static IConfigurationRoot GetIConfigurationRoot()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
    }

    [SetUp]
    public void Setup()
    {
        IConfiguration configuration = GetIConfigurationRoot();
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddDbContext<ArticleDbContext>(options => options.UseInMemoryDatabase("ArticleDb"));

        serviceCollection.Register(configuration);

        _serviceProvider = serviceCollection.BuildServiceProvider();

        _dbContext = _serviceProvider.GetService<ArticleDbContext>();
        SeedData();
    }

    private void SeedData()
    {
        List<Article> articleFakeData = new List<Article>();
        for (int i = 0; i < 10; i++)
        {
            Article newArticle = new Article();
            newArticle.Create(Faker.Lorem.Sentence(3), Faker.Name.FullName(), Faker.Lorem.Paragraph(2));
            articleFakeData.Add(newArticle);
        }
        _dbContext.Articles.AddRange(articleFakeData);
        _dbContext.SaveChanges();
    }

    [Test]
    public async Task CreateNewAricle_MustCreate()
    {
        //Arange
        ArticleDto fakeData = new ArticleDto
        {
            Author = Faker.Name.FullName(),
            ArticleContent = Faker.Lorem.Sentence(3),
            Title = Faker.Lorem.Sentence(1).ToString()
        };

        CreateArticleCommand command = new CreateArticleCommand { Author = fakeData.Author, ArticleContent = fakeData.ArticleContent, Title = fakeData.Title };
        CreateArticleCommandHandler handler = new CreateArticleCommandHandler(_dbContext);

        //Act
        ArticleDto returnData = await handler.Handle(command, new System.Threading.CancellationToken());

        //Asert
        Assert.Multiple(() => {
            Assert.AreEqual(fakeData.ArticleContent, returnData.ArticleContent);
            Assert.AreEqual(fakeData.Author, returnData.Author);
            Assert.AreEqual(fakeData.Title, returnData.Title);
            Assert.AreNotEqual(returnData.Id, Guid.Empty);
        });
    }

    [Test]
    public async Task UpdateArticle_MustUpdate()
    {
        //Arange
        ArticleDto newData = new ArticleDto
        {
            Author = Faker.Name.FullName() + "randomStr", //Maybe generate same data, so I added postfix "randomStr"
            ArticleContent = Faker.Lorem.Sentence(3) + "randomStr",
            Title = Faker.Lorem.Sentence(1).ToString() + "randomStr"
        };
        Article existArticle = await _dbContext.Articles.AsNoTracking().FirstAsync();

        UpdateArticleCommand command = new UpdateArticleCommand {  Author = newData.Author, ArticleContent = newData.ArticleContent, Title = newData.Title };
        command.SetId(existArticle.Id);
        UpdateArticleCommandHandler handler = new UpdateArticleCommandHandler(_dbContext);

        //Act
        ArticleDto returnData = await handler.Handle(command, new System.Threading.CancellationToken());

        //Asert
        Assert.Multiple(() => {
            Assert.AreNotEqual(newData.ArticleContent, existArticle.ArticleContent);
            Assert.AreNotEqual(newData.Author, existArticle.Author);
            Assert.AreNotEqual(newData.Title, existArticle.Title);
        });
    }

    [Test]
    public async Task IncrementStarCount_MustIncrementCount()
    {
        //Arange
        Article existArticle = await _dbContext.Articles.AsNoTracking().FirstAsync();

        IncrementStarCommand command = new IncrementStarCommand { ArticleId = existArticle.Id };
        IncrementArticleStarCommandHandler handler = new IncrementArticleStarCommandHandler(_dbContext);

        //Act
        int newStarCount = await handler.Handle(command, new System.Threading.CancellationToken());

        //Asert
        Assert.AreEqual(existArticle.StarCount + 1, newStarCount);
    }
}