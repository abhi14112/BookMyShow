using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using Azure.Core;
namespace BookMyShow.Services
{
    public class PayPalService
    {
        private readonly HttpClient _httpClient;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _paypalUrl;

        public PayPalService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _clientId = configuration["PaypalSettings:ClientId"]!;
            _clientSecret = configuration["PaypalSettings:Secret"]!;
            _paypalUrl = configuration["PaypalSettings:Url"]!;
        }

        public async Task<string> GetAccessToken()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_paypalUrl}/v1/oauth2/token");
            string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_clientId}:{_clientSecret}"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);
            request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var jsonResponse = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            return jsonResponse.RootElement.GetProperty("access_token").GetString()!;
        }

        public async Task<JsonNode?> CreateOrder(decimal amount, string currency, string returnUrl, string cancelUrl)
        {
            string accessToken = await GetAccessToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var orderData = new
            {
                intent = "CAPTURE",
                purchase_units = new[]
                {
                    new
                    {
                        amount = new
                        {
                            currency_code = currency,
                            value = amount.ToString("F2")
                        }
                    }
                },
                application_context = new
                {
                    return_url = returnUrl,
                    cancel_url = cancelUrl
                }
            };
            var json = JsonSerializer.Serialize(orderData, new JsonSerializerOptions
            {   
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            var requestContext = new StringContent(json,Encoding.UTF8,"application/json");
            var response = await _httpClient.PostAsync($"{_paypalUrl}/v2/checkout/orders", requestContext);
            response.EnsureSuccessStatusCode();
            return JsonNode.Parse(await response.Content.ReadAsStringAsync());
        }
        public async Task<JsonNode?> CaptureOrder(string orderId)
        {
            string accessToken = await GetAccessToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var request = new HttpRequestMessage(HttpMethod.Post, $"{_paypalUrl}/v2/checkout/orders/{orderId}/capture")
            {
                Content = new StringContent("{}", Encoding.UTF8, "application/json") 
            };
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return JsonNode.Parse(await response.Content.ReadAsStringAsync());
        }
    }
}