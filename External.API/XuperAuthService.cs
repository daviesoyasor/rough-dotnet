
using System.Text.Json;

namespace Polaris.External.API
{
    public class XuperAuthService : IExternalAPI
    {
        private readonly IHttpClientFactory _factory;

        public XuperAuthService(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<Object?> GetSubscriptionPlansAsync()
        {
            var client = _factory.CreateClient();

            client.BaseAddress = new Uri("https://app.xuperauth.com");

            var result = await client.GetFromJsonAsync<Object>("/api/v1/admin/plan/get");


            return result;
        }


    }
}
