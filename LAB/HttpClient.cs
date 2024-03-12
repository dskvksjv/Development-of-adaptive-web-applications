using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoCompareAPI
{
    public class HttpClient
    {
        private readonly System.Net.Http.HttpClient _client;

        public HttpClient()
        {
            _client = new System.Net.Http.HttpClient();
            _client.BaseAddress = new Uri("https://min-api.cryptocompare.com/data/");
        }

        public async Task<string> Get(string endpoint)
        {
            var response = await _client.GetAsync(endpoint);
            var content = await response.Content.ReadAsStringAsync();
            DisplayResponseInfo(response);
            return content;
        }

        public async Task<string> Post(string endpoint, object data)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(endpoint, content);
            var responseData = await response.Content.ReadAsStringAsync();
            DisplayResponseInfo(response);
            return responseData;
        }

        private void DisplayResponseInfo(HttpResponseMessage response)
        {
            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Date: {DateTime.UtcNow}");
            Console.WriteLine($"Message: {response.ReasonPhrase}");
            Console.WriteLine();
        }
    }
}
