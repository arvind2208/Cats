using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Cats;
using Xunit;

namespace FunctionalTests
{
    public class CatsControllerTests
    {
        private IWebHostBuilder GetCommonTestWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder<Startup>(null)
                .UseSolutionRelativeContentRoot(@"./Cats");
        }

        [Fact]
        public void Http200_GetCats()
        {
            var builder = GetCommonTestWebHostBuilder();
            var testServer = new TestServer(builder);

            var client = testServer.CreateClient();

            var response = client.GetAsync("/api/cats").Result;

            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsStringAsync().Result;
            Assert.Equal("{\"catsByOwnersGenders\":[{\"ownerGender\":\"Male\",\"cats\":[\"Garfield\",\"Jim\",\"Max\",\"Tom\"]},{\"ownerGender\":\"Female\",\"cats\":[\"Garfield\",\"Tabby\",\"Simba\"]}]}", result);
        }

        [Fact]
        public void Http200_GetCatsByOwnersGender()
        {
            var builder = GetCommonTestWebHostBuilder();
            var testServer = new TestServer(builder);

            var client = testServer.CreateClient();

            var response = client.GetAsync("/api/cats?ownersgender=male").Result;

            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsStringAsync().Result;
            Assert.Equal("{\"catsByOwnersGenders\":[{\"ownerGender\":\"Male\",\"cats\":[\"Garfield\",\"Jim\",\"Max\",\"Tom\"]}]}", result);
        }
    }
}
