using System.Text.Json;

namespace Polaris.External.API
{
    public interface IExternalAPI
    {
        Task<Object?> GetSubscriptionPlansAsync();
    }
}
