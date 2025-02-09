using Subscriptions.Data.Models;

namespace Subscriptions.Api.Interfaces
{
    internal interface ISubscriptionService
    {
        Task<Subscription> CreateSusbcription(NewSubscriptionRequest request);
    }
}
