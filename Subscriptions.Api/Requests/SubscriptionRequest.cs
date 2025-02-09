record NewSubscriptionRequest
(
    int subscriptionType,
    string billingInterval,
    string adminEmail,
    string adminName,
    decimal paymentAmount,
    string paymentMethod
);