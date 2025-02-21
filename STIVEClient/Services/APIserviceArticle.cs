using LibrarySTIVE.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace STIVEClient.Services
{
    public static class APIserviceArticle
    {
        private static readonly HttpClient _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:5206/api/Article/")
        };

        public static async Task<List<ModelArticleJson>> GetAllArticles()
        {
            try
            {
                var response = await _httpClient.GetAsync("GetAll");
                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[DEBUG] Response JSON: {jsonResponse}");
                return JsonSerializer.Deserialize<List<ModelArticleJson>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ModelArticleJson>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] GetAllArticles failed: {ex.Message}");
                return new List<ModelArticleJson>();
            }
        }

        public static async Task<bool> CreateArticle(ModelArticleJson article)
        {
            var newArticle = new
            {
                id = 0,
                nom = article.Nom ?? "Default Name",
                contenance = article.Contenance > 0 ? article.Contenance : 1,
                familleId = article.FamilleId > 0 ? article.FamilleId : 1,
                fournisseurId = article.FournisseurId > 0 ? article.FournisseurId : 1,
                @ref = !string.IsNullOrEmpty(article.Ref) ? article.Ref : "NoRef",
                prix = article.Prix > 0 ? article.Prix : 10,
                age = article.Age != default ? article.Age : DateTime.UtcNow,
                artQuantity = article.ArtQuantity >= 0 ? article.ArtQuantity : 0
            };

            var jsonContent = JsonSerializer.Serialize(newArticle, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            Console.WriteLine($"[DEBUG] JSON: {jsonContent}");

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Create", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[ERROR] CreateArticle failed: {response.StatusCode}, {error}");
            }

            return response.IsSuccessStatusCode;
        }
        public static async Task<bool> UpdateArticle(int id, ModelArticleJson article)
        {
            var updatedArticle = new
            {
                nom = article.Nom ?? "Default Name",
                contenance = article.Contenance > 0 ? article.Contenance : 1,
                familleId = article.FamilleId > 0 ? article.FamilleId : 1,
                famille = new
                {
                    name = "Default Famille",
                    articles = new List<string>()
                },
                fournisseurId = article.FournisseurId > 0 ? article.FournisseurId : 1,
                fournisseur = new
                {
                    id = article.FournisseurId,
                    name = "Default Fournisseur",
                    adresse = "Unknown Address"
                },
                @ref = !string.IsNullOrEmpty(article.Ref) ? article.Ref : "NoRef",
                prix = article.Prix > 0 ? article.Prix : 10,
                age = article.Age != default ? article.Age : DateTime.UtcNow,
                artQuantity = article.ArtQuantity >= 0 ? article.ArtQuantity : 0
            };


            var jsonContent = JsonSerializer.Serialize(updatedArticle, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            Console.WriteLine($"[DEBUG] JSON: {jsonContent}");

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"Update/{id}", content);

            return response.IsSuccessStatusCode;
        }

        public static async Task<bool> DeleteArticle(int id)
        {
            var response = await _httpClient.DeleteAsync($"Delete/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
