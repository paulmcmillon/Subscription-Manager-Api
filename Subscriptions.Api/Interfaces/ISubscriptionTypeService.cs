using Subscriptions.Data.Models;

namespace Subscriptions.Api.Interfaces
{
    internal interface ISubscriptionTypeService
    {
        Task<IEnumerable<SubscriptionType>> GetSubscriptionTypes();
    }
}
