using Subscriptions.Api.Interfaces;
using Subscriptions.Data;
using Subscriptions.Data.Models;

namespace Subscriptions.Api.Services
{
    internal sealed class PaymentService(SubscriptionsContext dbContext) : IPaymentService
    {
        public async Task<bool> MakePaymentAsync(Guid subscriptionId, decimal amount, string paymentMethod)
        {
            Payment payment = new()
            {
                SubscriptionId = subscriptionId,
                Amount = amount,
                PaymentMethod = paymentMethod
            };
            await dbContext.Payments.AddAsync(payment);
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
