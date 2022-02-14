using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class ReviewService
{
    private readonly HttpClient _httpClient;
    public ReviewService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ReviewClient");
    }

    public async Task<bool> ExistReviewByArticleId(string articleId)
    {
        var result = await _httpClient.GetAsync($"/review-api/review/?$Filter=ArticleId eq {articleId}&$top=1");
        string json = await result.Content.ReadAsStringAsync();
        List<ReviewDto> reviewList = JsonSerializer.Deserialize<List<ReviewDto>>(json);

        return reviewList.Any();
    }
}