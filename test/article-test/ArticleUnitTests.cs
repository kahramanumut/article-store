using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using MediatR;

namespace ArticleTest
{
    public class ArticleUnitTest
    {
        private Mock<ArticleDbContext> _dbContext;
        private Mock<IMediator> _mediator;


        [SetUp]
        public void Setup()
        {
            _dbContext = new Mock<ArticleDbContext>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task CreateArticleCommand_MustReturn_CreateArticleDto()
        {
            //Arange
            ArticleDto fakeData = new ArticleDto
            {
                Author = Faker.Name.FullName(),             
                ArticleContent = Faker.Lorem.Sentences(3).ToString(),
                Title = Faker.Lorem.Sentence(1).ToString()
            };
            
            CreateArticleCommand command = new CreateArticleCommand { Author = fakeData.Author, ArticleContent = fakeData.ArticleContent, Title = fakeData.Title };
            CreateArticleCommandHandler handler = new CreateArticleCommandHandler(_dbContext.Object);

            //Act
            ArticleDto returnData = await handler.Handle(command, new System.Threading.CancellationToken());

            //Asert
            Assert.IsInstanceOf<ArticleDto>(returnData);
        }


    }
}