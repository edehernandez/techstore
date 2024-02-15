using Catalog.Business.Model;
using Newtonsoft.Json;
using System.Text;

namespace Catalog.API.IntegrationTests.Helpers
{
    public static class HttpTestHelper
    {
        public const string EmailTest = "test@test.com";
        public const string PasswordTest = "123456";

        public static async Task AddToken(this HttpClient httpClient)
        {
            var token = await GenerateToken(httpClient);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        private static async Task<string> GenerateToken(HttpClient httpClient)
        {
            var login = new Login { Email = EmailTest, Password = PasswordTest };
            var loginContent = SerializeObject(login);
            var loginResponse = await httpClient.PostAsync("/api/authentication", loginContent);

            var token = await loginResponse.Content.ReadAsStringAsync();
            return token;
        }

        public static StringContent SerializeObject(object? initialProduct)
        {
            return new StringContent(JsonConvert.SerializeObject(initialProduct), Encoding.UTF8, "application/json");
        }
    }
}
