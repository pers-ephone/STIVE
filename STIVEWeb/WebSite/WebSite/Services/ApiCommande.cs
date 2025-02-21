using System.Text.Json;
using LibrarySTIVE.Models;
namespace WebSite.Services;

public class ApiCommande
{
    public static HttpClient Client { get; set; } = new HttpClient
    {
        BaseAddress = new Uri("http://localhost:5206/")
    };

    public static async Task<ModelCommandeJson> GetById(int id)
    {
        if (id <= 0)
        {
            Console.WriteLine("Invalid ID provided.");
            return null;
        }
        try
        {
            var response = await Client.GetAsync($"/api/Commande/GetById/{id}");
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

            var commande = JsonSerializer.Deserialize<ModelCommandeJson>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (commande == null)
            {
                Console.WriteLine("Deserialization failed.");
                return null;
            }

            Console.WriteLine($"Commande found: ");
            return commande;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception during API call: {ex.Message}");
            return null;
        }
    }
}