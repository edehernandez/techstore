using System.Net;
using Newtonsoft.Json;
using FluentAssertions;
using HttpMethod = System.Net.Http.HttpMethod;
using Catalog.API.IntegrationTests.Helpers;
using Catalog.Business.Model;

namespace Catalog.API.Integration.Tests.Controllers
{
    [TestFixture]
    public class CatalogControllerIntegrationTests
    {
        private CustomWebApplicationFactory<Program> webAppfactory;
        private HttpClient httpClient;

        [SetUp]
        public void Setup()
        {
            webAppfactory = new CustomWebApplicationFactory<Program>();
            httpClient = webAppfactory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            httpClient.Dispose();
            webAppfactory.Dispose();
        }

        [Test]
        [TestCase("/api/catalog", "GET")]
        [TestCase("/api/catalog/3fa85f64-5717-4562-b3fc-2c963f66afa6", "GET")]
        [TestCase("/api/catalog", "POST")]
        [TestCase("/api/catalog/3fa85f64-5717-4562-b3fc-2c963f66afa6", "PUT")]
        [TestCase("/api/catalog/3fa85f64-5717-4562-b3fc-2c963f66afa6", "DELETE")]
        public async Task All_Endpoints_Require_Authorization(string url, string httpMethod)
        {
            // Act
            var request = new HttpRequestMessage(new HttpMethod(httpMethod), url);
            HttpResponseMessage response = await httpClient.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task When_Getting_Product_By_Id_Returns_Success_Status_Code_And_Correct_Product()
        {
            // Arrange
            // Add the expected product
            await httpClient.AddToken();

            var expectedProduct = new Product
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Brand = "Apple",
                Name = "Apple Watch Series 7",
                Description = "Advanced smartwatch with health monitoring features.",
                Price = 399.99M,
                QuantityInStock = 150
            };

            var productContent = HttpTestHelper.SerializeObject(expectedProduct);
            await httpClient.PostAsync("/api/catalog", productContent);

            // Act
            // Get the product
            var getProductResponse = await httpClient.GetAsync($"/api/catalog/{expectedProduct.Id}");
            var responseBody = await getProductResponse.Content.ReadAsStringAsync();
            var actualProduct = JsonConvert.DeserializeObject<Product>(responseBody);

            // Assert
            // Verify the actual product matches the expected product and then that the status code is OK
            actualProduct.Should().BeEquivalentTo(expectedProduct);
            getProductResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task When_Updating_Product_Returns_Success_Status_Code_And_Updated_Product()
        {
            // Arrange
            await httpClient.AddToken();

            var initialProduct = new Product
            {
                Id = new Guid("C7199E8C-A6A5-4175-A40F-6E541BAC6BC0"),
                Brand = "Apple",
                Name = "Apple Watch Series 7",
                Description = "Advanced smartwatch with health monitoring features.",
                Price = 399.99M,
                QuantityInStock = 150
            };
            StringContent initialProductContent = HttpTestHelper.SerializeObject(initialProduct);
            await httpClient.PostAsync("/api/catalog", initialProductContent);

            // Updated product
            var updatedProduct = new Product
            {
                Id = initialProduct.Id,
                Brand = "Apple",
                Name = "Apple Watch Series 7 - Updated",
                Description = "Advanced smartwatch with enhanced health monitoring features.",
                Price = 449.99M,
                QuantityInStock = 200
            };

            // Update the product
            var updatedProductContent = HttpTestHelper.SerializeObject(updatedProduct);
            await httpClient.PutAsync($"/api/catalog/{initialProduct.Id}", updatedProductContent);

            // Act - Get the updated product
            var updatedProductResponse = await httpClient.GetAsync($"/api/catalog/{initialProduct.Id}");
            var updatedResponseBody = await updatedProductResponse.Content.ReadAsStringAsync();
            var actualUpdatedProduct = JsonConvert.DeserializeObject<Product>(updatedResponseBody);

            // Assert
            actualUpdatedProduct.Should().BeEquivalentTo(updatedProduct);
            updatedProductResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task When_Deleting_Product_Returns_Success_Status_Code_And_Product_Not_Found()
        {
            // Arrange
            await httpClient.AddToken();

            var productToDelete = new Product
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Brand = "Apple",
                Name = "Apple Watch Series 7",
                Description = "Advanced smartwatch with health monitoring features.",
                Price = 399.99M,
                QuantityInStock = 150
            };

            // Add the product to be deleted
            var productContent = HttpTestHelper.SerializeObject(productToDelete);
            await httpClient.PostAsync("/api/catalog", productContent);

            // Act - Delete the product
            var deleteResponse = await httpClient.DeleteAsync($"/api/catalog/{productToDelete.Id}");

            // Assert
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            // Act - Get the deleted product
            var getProductResponse = await httpClient.GetAsync($"/api/catalog/{productToDelete.Id}");

            // Assert
            getProductResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
