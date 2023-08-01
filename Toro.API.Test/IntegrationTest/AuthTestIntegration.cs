using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Toro.API.Domain.Resources.Result;
using Toro.API.Test.Config;
using Toro.API.Test.Helpers;
using Xunit;

namespace Toro.API.Test.UnitTest
{

    [Collection("MyTestCollection")]
    public class AuthTestIntegration
    {
        private readonly HttpClient client;
        public AuthTestIntegration(TestFixture fixture)
        {
            client = fixture.CreateClient();
        }


        [Fact]
        public async Task AuthAsync_OK()
        {
            //Arr
            string jsonContent = "{\"email\":\"geovana.nocera@gmail.com\",\"password\":\"12345678\"}";
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/user/auth", content).ConfigureAwait(false);

            //Act
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task AuthAsync_NOK()
        {
            //Arr
            string jsonContent = "{\"email\":\"geovana.sembraski@gmail.com\",\"password\":\"12345678\"}";
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/user/auth", content).ConfigureAwait(false);
            var result = response.Content.ReadFromJsonAsync<BadRequestResult>().Result;

            //Act
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Email ou senha incorretos.", result!.Errors);
        }
    }
}