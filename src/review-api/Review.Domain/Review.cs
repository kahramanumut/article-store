using System;

namespace Domain.Review
{
    public class Review : Entity
    {
        public Guid ArticleId { get; private set; }
        public string Reviewer { get; private set; }
        public string ReviewContent { get; private set; }

        public void Create(Guid articleId, string reviewer, string reviewContent)
        {
            ArticleId = articleId;
            Reviewer = reviewer;
            ReviewContent = reviewContent;
        }

        public void Update(string reviewer, string reviewContent)
        {
            Reviewer = reviewer;
            ReviewContent = reviewContent;
        }
    }
}