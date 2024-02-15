using Catalog.API.IntegrationTests.Helpers;
using Catalog.Business.Model;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Catalog.API.Integration.Tests.Controllers
{
    [TestFixture]
    public class AuthenticationControllerTests
    {

        private CustomWebApplicationFactory<Program> webAppfactory;
        private HttpClient httpClient;

        [SetUp]
        public void Setup()
        {
            webAppfactory = new CustomWebApplicationFactory<Program>();
            httpClient = webAppfactory.CreateDefaultClient();
        }

        [TearDown]
        public void TearDown()
        {
            httpClient.Dispose();
            webAppfactory.Dispose();
        }


        [Test]
        public async Task Post_Invalid_Login_Returns_Unauthorized()
        {
            // Arrange
            var login = new Login { Email = "invalid@gmail.com", Password = "invalidpassword" };
            var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PostAsync("/api/authentication", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task Post_Valid_Login_Returns_Token()
        {
            // Arrange
            var login = new Login { Email = HttpTestHelper.EmailTest, Password = HttpTestHelper.PasswordTest };
            var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PostAsync("/api/authentication", content);

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            var token = await response.Content.ReadAsStringAsync();
            token.Should().NotBeNullOrWhiteSpace();
        }
    }
}
