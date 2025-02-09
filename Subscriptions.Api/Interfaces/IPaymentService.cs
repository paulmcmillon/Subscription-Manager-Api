namespace Subscriptions.Api.Interfaces
{
    internal interface IPaymentService
    {
        Task<bool> MakePaymentAsync(Guid subscriptionId, decimal amount, string paymentMethod);
    }
}
