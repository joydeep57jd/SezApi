using SezApi.Model.Request;
using SezApi.Model.Response;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SezApi.Services
{
    public class CWCservice
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CWCservice(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<ResponseCWCapi> PostInvoiceToCWCAsync(RequestCWCapi request)
        {
            try
            {
                // Get config values
                string url = _configuration["ExternalApi:BaseUrl"];
                string user = _configuration["ExternalApi:UserId"];
                string pwd = _configuration["ExternalApi:Password"];

                string json = JsonSerializer.Serialize(request);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user}:{pwd}"));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var httpResponse = await _httpClient.PostAsync(url, content);
                var responseJson = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception($"CWC API returned error: {httpResponse.StatusCode} - {responseJson}");
                }

                var result = JsonSerializer.Deserialize<ResponseCWCapi>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while calling CWC API: {ex.Message}", ex);
            }
        }
    
}
}
