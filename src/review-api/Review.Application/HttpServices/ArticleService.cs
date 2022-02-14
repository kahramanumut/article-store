using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class ArticleService
{
    private readonly HttpClient _httpClient;
    public ArticleService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ArticleClient");
    }

    public async Task<bool> ExistArticleByArticleId(string articleId)
    {
        var result = await _httpClient.GetAsync($"/article-api/article/{articleId}");
        string json = await result.Content.ReadAsStringAsync();
        List<ArticleDto> articleList = JsonSerializer.Deserialize<List<ArticleDto>>(json);

        return articleList.Any();
    }
}