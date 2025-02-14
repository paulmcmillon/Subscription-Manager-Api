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
                ISubscriptionService subscriptionService) =>
            {
                try
                {
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine($"HTTP REQUEST Object - {request}");
                    Console.WriteLine("---------------------------------------------");

                    var subscription = await subscriptionService.CreateSusbcription(request);
                    return Results.Created($"/Subscription/{subscription.Id}", subscription);
                }
                catch (Exception ex)
                {
                    //logger.LogError(ex, ex.StackTrace);
                    return Results.Problem(ex.Message);
                }
            })
            .Produces<List<SubscriptionType>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Create a new subscription")
            .WithDescription("Creates a subscription record and associated payment record.");
        }
    }
}
