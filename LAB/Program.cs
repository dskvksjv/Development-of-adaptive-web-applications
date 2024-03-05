using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CryptoCompareAPI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var httpClient = new HttpClient();

                // GET 
                var responseData = await GetTopCoins(httpClient);
                Console.WriteLine($"GET Response: {responseData}");

                // POST 
                var postData = new { param1 = "value1", param2 = "value2" };
                var postDataResponse = await PostData(httpClient, postData);
                Console.WriteLine($"POST Response: {postDataResponse}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Environment.ExitCode = (int)HttpStatusCode.InternalServerError;
            }
        }

        static async Task<string> GetTopCoins(HttpClient httpClient)
        {
            var response = await httpClient.Get("top/totalvolfull?limit=10&tsym=USD");
            return response;
        }

        static async Task<string> PostData(HttpClient httpClient, object data)
        {
            var response = await httpClient.Post("endpoint", data);
            return response;
        }
    }
}

