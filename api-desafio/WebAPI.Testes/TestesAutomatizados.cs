using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using System.Net.Http;
using WebAPI.Domain.Models;
using Xunit;

namespace WebAPI.Testes
{
    public class TestesAutomatizados
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _httpClient;

        public TestesAutomatizados(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
        }

        [Fact]
        public async void RecuperarTodosUsuarios()
        {
            var result = await _httpClient.GetAsync("api/usuario");
            Assert.NotNull(result.Content);
            Assert.IsType<List<Usuario>>(result.Content);
        }
    }
}
