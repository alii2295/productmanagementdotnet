using gestionproduit.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace gestionproduit.Services
{
    public class GameApiService
    {
        private readonly HttpClient _httpClient;
        private const string API_URL = "https://api.rawg.io/api/games?key=d28a70fedbd540ebaed84e962bc261d4";

        public GameApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Game>> GetGamesAsync()
        {
            var response = await _httpClient.GetStringAsync(API_URL);
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response);
            return apiResponse?.Results ?? new List<Game>();
        }
    }
}
