record NewSubscriptionRequest
(
    int TypeId,
    string BillingInterval,
    string AdminEmail,
    string AdminName,
    decimal PaymentAmount,
    string PaymentMethod,
    string PaymentTransactionId
);