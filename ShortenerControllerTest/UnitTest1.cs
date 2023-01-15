using Xunit;
using Inforce;
using Microsoft.AspNetCore.Mvc.Testing;
using Inforce.Controllers;
using System.Threading.Tasks;
using System.Net.Http;

namespace ShortenerControllerTest
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Inforce.Controllers.ShortenerController>>
    {
        private readonly WebApplicationFactory<ShortenerController> _factory;
        private readonly HttpClient httpClient;
        public UnitTest1(WebApplicationFactory<ShortenerController> factory)
        {
            _factory = factory;
            httpClient = _factory.CreateClient();
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Home/Index")]
        [InlineData("/Home/Privacy")]
        [InlineData("/Shortener/Index")]
        [InlineData("/Shortener/Create")]
        [InlineData("/Shortener/Edit")]
        [InlineData("/Shortener/Details")]
        [InlineData("/Shortener/Delete")]
        public async Task AllPagesLoadTest(string URL)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(URL);
            int code = (int)response.StatusCode;

            Assert.Equal(200, code);
        }

        [Theory]
        [InlineData("https://inforce.digital/")]
        public async Task InforceWebsiteTest(string InforceURL)
        {
            var response = await httpClient.GetAsync("/Shortener/Index");

            var content = await response.Content.ReadAsStringAsync();

            var contentString = content.ToString();

            Assert.Contains(InforceURL, contentString);

        }





    }
}