using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Toro.API.Domain.Models;
using Toro.API.Domain.Resources.Result;
using Toro.API.Test.Config;
using Toro.API.Test.Helpers;
using Xunit;

namespace Toro.API.Test.UnitTest
{

    [Collection("MyTestCollection")]
    public class PersonTestIntegration
    {
        private readonly HttpClient client;
        public PersonTestIntegration(TestFixture fixture)
        {
            client = fixture.CreateClient();
        }

        [Fact]
        public async Task Create_OK()
        {
            //Arr
            string jsonContent = "{\"name\": \"Novo Usu�rio\",\"email\": \"novo@test.com\",\"password\": \"12345678\",\"cpf\": \"66490365014\",\"birth\": \"1993-08-01T20:19:16.355Z\",\"wallet\": { \"balance\": 0, \"assets\": [   {     \"code\": \"string\",     \"name\": \"string\",     \"amount\": 0   } ]  }}";
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/person", content).ConfigureAwait(false);

            //Act
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Create_CPF_NOK()
        {
            //Arr
            string jsonContent = "{\"name\": \"Novo Usu�rio\",\"email\": \"novo@test.com\",\"password\": \"12345678\",\"cpf\": \"98103732010\",\"birth\": \"1993-08-01T20:19:16.355Z\"}";
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/person", content).ConfigureAwait(false);

            //Act
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Create_Email_NOK()
        {
            //Arr
            string jsonContent = "{\"name\": \"Novo Usu�rio\",\"email\": \"geovana.nocera@gmail.com\",\"password\": \"12345678\",\"cpf\": \"66490365014\",\"birth\": \"1993-08-01T20:19:16.355Z\"}";
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/person", content).ConfigureAwait(false);

            //Act
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetAsync_OK()
        {
            //Arr
            var contentAuth = new StringContent("{\"email\":\"geovana.nocera@gmail.com\",\"password\":\"12345678\"}", System.Text.Encoding.UTF8, "application/json");
            var responseAuth = await client.PostAsync("/api/user/auth", contentAuth).ConfigureAwait(false);
            var result = responseAuth.Content.ReadFromJsonAsync<OkRequestResult>().Result;
            var token = JsonSerializer.Serialize(result!.Data);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));

            // Act
            var response = await client.GetAsync("/api/person/wallet").ConfigureAwait(false);
            var resultQuery = response.Content.ReadFromJsonAsync<OkRequestResult>().Result;

            //Act
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(resultQuery.Data);
        }
    }
}