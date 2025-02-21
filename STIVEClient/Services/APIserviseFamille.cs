using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using LibrarySTIVE.Models;
namespace STIVEClient.Services;

public class APIserviseFamille
{
    private static readonly HttpClient _httpClient = new HttpClient()
    {
        BaseAddress = new Uri("http://localhost:5206/api/Famille/")
    };

    public static async Task<List<ModelFamilleModelJson>> GetAllFamille()
    {
        try
        {
            var response = await _httpClient.GetAsync("GetAll");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"[DEBUG] Response JSON: {jsonResponse}");
            return JsonSerializer.Deserialize<List<ModelFamilleModelJson>>(jsonResponse,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ModelFamilleModelJson>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] GetAllFamille failed: {ex.Message}");
            return new List<ModelFamilleModelJson>();
        }
    }

}