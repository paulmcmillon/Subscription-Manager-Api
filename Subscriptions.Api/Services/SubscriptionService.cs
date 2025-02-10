using Subscriptions.Api.Interfaces;
using Subscriptions.Data;
using Subscriptions.Data.Models;

namespace Subscriptions.Api.Services
{
    internal sealed class SubscriptionService(SubscriptionsContext dbContext) : ISubscriptionService
    {
        ///--------------------------------------------------------------------------
        /// <summary>
        /// Creates a new subscription and associated payment record.
        /// </summary>
        /// <param name="request">
        /// C# record containing parameters for new subscription and payment records.
        /// </param>
        /// <returns>Newly created instance of subscription class</returns>
        ///--------------------------------------------------------------------------
        public async Task<Subscription> CreateSusbcription(NewSubscriptionRequest request)
        {
            var subscription = new Subscription
            {
                SubscriptionTypeId = request.subscriptionType,
                BillingInterval = request.billingInterval,
                Email = request.adminEmail,
                AdministratorName = request.adminName,
                IsLocked = false,
                LockReason = "Unlocked",
                IsSoftDeleted = false
            };
            var payment = new Payment
            {
                SubscriptionId = subscription.Id,
                Amount = request.paymentAmount,
                PaymentMethod = request.paymentMethod
            };
            await dbContext.Subscriptions.AddAsync(subscription);
            await dbContext.Payments.AddAsync(payment);
            await dbContext.SaveChangesAsync();
            return await Task.FromResult(subscription);
        }
    }
}
