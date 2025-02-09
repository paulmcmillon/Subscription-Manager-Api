using Microsoft.AspNetCore.Mvc;
using Subscriptions.Api.Interfaces;
using Subscriptions.Data.Models;

namespace Subscriptions.Api.EndPoints
{
    public static class SubscriptionEndPoints
    {
        public static void MapEndPoints(this IEndpointRouteBuilder app, Action<IEndpointRouteBuilder> mapEndPoints)
        {
            var subscriptionGroup = app.MapGroup("Subscription");

            subscriptionGroup.MapPost("/", async ([FromBody]NewSubscriptionRequest request, 
                ISubscriptionService subscriptionService, ILogger logger, CancellationToken cancellation) =>
            {
                try
                {
                    var subscription = await subscriptionService.CreateSusbcription(request);
                    return Results.Created($"/Subscription/{subscription.Id}", subscription);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.StackTrace);
                    return Results.Problem(ex.Message);
                }
                //var subscription = await subscriptionService.CreateSusbcription(request);                                
                //return Results.Created($"/Subscription/{subscription.Id}", subscription);
            });

        }
    }
}
