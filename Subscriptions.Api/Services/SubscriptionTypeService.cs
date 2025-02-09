using Microsoft.EntityFrameworkCore;
using Subscriptions.Api.Interfaces;
using Subscriptions.Data;
using Subscriptions.Data.Models;

namespace Subscriptions.Api.Services
{
    internal sealed class SubscriptionTypeService(SubscriptionsContext dbContext) : ISubscriptionTypeService
    {
        public async Task<IEnumerable<SubscriptionType>> GetSubscriptionTypes()
        {
            //Creating Scenario to throw Unhandled Exception
            //int x = 10, y = 0;
            //int result = x / y;

            return await dbContext.SubscriptionTypes.Include(st => st.Features)
                .AsNoTracking()
                .ToListAsync(CancellationToken.None);
        }
    }
}
