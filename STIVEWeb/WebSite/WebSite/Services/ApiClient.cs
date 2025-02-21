using System.Text.Json;
using LibrarySTIVE.Models;
namespace WebSite.Services;

public class ApiClient
{
    public static HttpClient Client { get; set; } = new HttpClient
    {
        BaseAddress = new Uri("http://localhost:5206/")
    };

    public static async Task<ModelArticleJson> GetArticleBy(int id)
    {
        if (id <= 0)
        {
            Console.WriteLine("Invalid ID provided.");
            return null;
        }
        try
        {
            var response = await Client.GetAsync($"/api/Article/GetById/{id}");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"API call failed: {response.StatusCode} - {response.ReasonPhrase}");
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(content))
            {
                Console.WriteLine("API returned empty content.");
                return null;
            }

            var article = JsonSerializer.Deserialize<ModelArticleJson>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (article == null)
            {
                Console.WriteLine("Deserialization failed.");
                return null;
            }

            Console.WriteLine($"Article found: {article.Nom}");
            return article;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception during API call: {ex.Message}");
            return null;
        }
    }

    public static async Task<List<ModelArticleJson>> GetAllArcticle()
    {
        var response = await Client.GetAsync("api/Article/GetAll");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var articles = JsonSerializer.Deserialize<List<ModelArticleJson>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (string.IsNullOrEmpty(content))
        {
            Console.WriteLine("API returned no content.");
            return new List<ModelArticleJson>();
        }

        foreach (ModelArticleJson a in articles)
        {
            Console.WriteLine(a.Nom);
        }
        return articles;
    }



}