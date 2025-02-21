using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LibrarySTIVE.Models;
namespace STIVEClient.Services;

public class APIserviceClient
{
    private static readonly HttpClient _httpClient = new HttpClient()
    {
        BaseAddress = new Uri("http://localhost:5206/api/ClientSTIVE/")
    };

    public static async Task<List<ClientSTIVEJSON>> GetALLAsync()
    {
        var response = await _httpClient.GetAsync("GetAll");
        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"[DEBUG] Response JSON: {jsonResponse}");
        return JsonSerializer.Deserialize<List<ClientSTIVEJSON>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<ClientSTIVEJSON>();
    }

    public async Task<ClientSTIVEJSON?> GetByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"GetById/{id}");
        if (!response.IsSuccessStatusCode)
            return null;
        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ClientSTIVEJSON>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    public async Task<bool> CreateAsync(ClientSTIVEJSON client)
    {
        var content = new StringContent(JsonSerializer.Serialize(client), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("Create", content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsync(int id, ClientSTIVEJSON client)
    {
        var content = new StringContent(JsonSerializer.Serialize(client), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"Update/{id}", content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"Delete/{id}");
        return response.IsSuccessStatusCode;
    }
}
