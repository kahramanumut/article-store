using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using MediatR;

namespace ArticleTest
{
    public class ValitationTest
    {
        private CreateArticleValidator _createArticleValidator { get; set; }
        [SetUp]
        public void Setup()
        {
            _createArticleValidator = new CreateArticleValidator();
        }

        [Test]
        public void CheckArticleValidator_ValidData_ShouldReturnTrue()
        {
            //Arange
            CreateArticleCommand fakeCommand = new CreateArticleCommand
            {
                Author = Faker.Name.FullName(),             
                ArticleContent = Faker.Lorem.Sentences(3).ToString(),
                Title = Faker.Lorem.Sentence(1).ToString()
            };

            //Act
            bool valid = _createArticleValidator.Validate(fakeCommand).IsValid;

            //Asert
            Assert.IsTrue(valid);
        }

        [Test]
        public void CheckArticleValidator_UnvalidData_ShouldReturnUnvalid()
        {
            //Arange
            CreateArticleCommand fakeCommand = new CreateArticleCommand
            {
                Author = Faker.Lorem.GetFirstWord().Substring(0,3),             
                ArticleContent = Faker.Lorem.GetFirstWord().ToString().Substring(0,5),
                Title = Faker.Lorem.Sentence(1).ToString()
            };

            //Act
            bool unvalid = _createArticleValidator.Validate(fakeCommand).IsValid;

            //Asert
            Assert.IsFalse(unvalid);
        }


    }
}