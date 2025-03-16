using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace GroceryManSystem.Services
{
    public class PaypalService
    {
        // Private static instance
        private static readonly Lazy<PaypalService> _instance =
            new Lazy<PaypalService>(() => new PaypalService());

        // Replace these with your PayPal credentials
        private static readonly string ClientId = "AWV_bseM9P3LFnGNgXZCdXoUjZyYAMOstXjjgnfpmEVb8y-zTlkIp3UpNrJjG5mGxsPClAD1rkiXEXp2"; // Your PayPal Client ID
        private static readonly string ClientSecret = "EI7U-fZOKkzQGxdRhir4CkeLS2tBWyOUH3X26KHYDNbflpn0PB1UWJHUs91T4nvg1U7lDfrzjVuv0dnO"; // Your PayPal Secret
        private static readonly string TokenEndpoint = "https://api-m.sandbox.paypal.com/v1/oauth2/token"; // Use 'api-m.paypal.com' for production
        private static readonly string ApiBaseUrl = "https://api-m.sandbox.paypal.com"; // Base URL for PayPal APIs

        // Private constructor to prevent direct instantiation
        private PaypalService()
        {
        }

        // Public static property to get the single instance
        public static PaypalService Instance => _instance.Value;


        /// Retrieves an access token from PayPal.
        /// <returns>A string containing the access token, or null if an error occurs.</returns>
        public async Task<string> GetAccessTokenAsync()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(30); // Set timeout

                    // Set basic authentication for client ID and secret
                    var authString = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{ClientId}:{ClientSecret}"));
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);

                    // Prepare the request payload
                    var content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

                    // Send the request
                    HttpResponseMessage response = await httpClient.PostAsync(TokenEndpoint, content);

                    // Ensure the response is successful
                    response.EnsureSuccessStatusCode();

                    // Parse the response to extract the access token
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var tokenObj = JObject.Parse(jsonResponse);

                    return tokenObj["access_token"]?.ToString();
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"HTTP error during PayPal token request: {ex.Message}");
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"General error during PayPal token request: {ex.Message}");
                    return null;
                }
            }
        }
    }
}
