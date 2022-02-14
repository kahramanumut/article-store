using System;

namespace Domain.Article
{
    public class Article : Entity
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string ArticleContent { get; private set; }
        public DateTime PublishDate { get; private set; }
        public int StarCount { get; private set; }

        public void Create(string title, string author, string articleContent)
        {
            Id = Guid.NewGuid();
            Title = title;
            Author = author;
            ArticleContent = articleContent;
            PublishDate = DateTime.UtcNow;
        }

        public void Update(string title, string author, string articleContent)
        {
            Title = title;
            Author = author;
            ArticleContent = articleContent;
        }

        public void IncrementStartCount()
        {
            StarCount++;
        }

    }
}