using System.Text;
using System.Text.Json;
using DogsService.Dtos;

namespace DogsService.SyncDataServices.Http
{
    public class HttpPhotoDataClient : IPhotoDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpPhotoDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task SendDogToPhoto(DogReadDto dog)
        {
           var httpContent = new StringContent(
            JsonSerializer.Serialize(dog),
            Encoding.UTF8,
            "application/json");

            var response = await _httpClient.PostAsync($"{_configuration["PhotoService"]}" , httpContent);

            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to PhotoService was OK");
            }
            else
            {
                Console.WriteLine("--> Sync POST to PhotoService was ERROR");
            }
        }
    }
}