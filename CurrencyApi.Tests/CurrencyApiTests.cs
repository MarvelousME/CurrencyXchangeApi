using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net;
using Xunit;

namespace CurrencyApi.Tests
{
    public class CurrencyApiTests : IDisposable
    {
        protected TestServer _testServer;
        public CurrencyApiTests()
        {
            var webBuilder = new WebHostBuilder();
            webBuilder.UseStartup<Startup>();

            _testServer = new TestServer(webBuilder);
        }
        

        [Fact]
        public async Task Test_External_API_Method()
        {
            var response = await _testServer.CreateRequest("/api/v1/GetExternalApiCurrencyRatesAsync/ZAR").SendAsync("GET");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

          
        }

        public void Dispose()
        {
            _testServer.Dispose();
        }
    }
}