using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using gestionproduit.Models;

public class ProductApiService
{
    private readonly HttpClient _httpClient;

    public ProductApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Productt>> GetProductsAsync()
    {
        var response = await _httpClient.GetStringAsync("https://fakestoreapi.com/products");
        return JsonConvert.DeserializeObject<List<Productt>>(response);
    }

    public async Task<Productt> GetProductByIdAsync(int id)
    {
        var response = await _httpClient.GetStringAsync($"https://fakestoreapi.com/products/{id}");
        return JsonConvert.DeserializeObject<Productt>(response);
    }
}
