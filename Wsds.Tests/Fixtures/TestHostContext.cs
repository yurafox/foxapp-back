using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Wsds.WebApp;

namespace Wsds.Tests.Fixtures
{
    public class TestHostContext
    {
        private TestServer _server;
        public HttpClient Client { get; private set; }

        public TestHostContext()
        {
            SetUpClient();
        }

        private void SetUpClient()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());

            Client = _server.CreateClient();
        }
    }
}
