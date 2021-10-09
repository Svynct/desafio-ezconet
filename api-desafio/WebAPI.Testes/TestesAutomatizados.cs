using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using WebAPI.Application.Services.Interfaces;
using WebAPI.Controllers;
using WebAPI.Domain.Models;
using Xunit;

namespace WebAPI.Testes
{
    public class TestesAutomatizados : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _httpClient;

        public TestesAutomatizados(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
        }

        [Fact]
        public void RecuperarTodosUsuariosDeveRetornarStatusCode200ENotNull()
        {
            var httpClientRequest = _httpClient.GetAsync("api/usuario").GetAwaiter().GetResult();
            var content = JsonConvert.DeserializeObject<List<Usuario>>(httpClientRequest.Content.ReadAsStringAsync().GetAwaiter().GetResult());

            Assert.Equal(HttpStatusCode.OK, httpClientRequest.StatusCode);
            Assert.NotNull(content);
        }
    }
}
