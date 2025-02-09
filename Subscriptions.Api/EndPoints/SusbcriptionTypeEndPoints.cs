using Subscriptions.Api.Interfaces;
using Subscriptions.Data.Models;

namespace Subscriptions.Api.Public.EndPoints
{
    public static class SusbcriptionTypeEndPoints
    {
        public static void MapEndPoints(this IEndpointRouteBuilder app, Action<IEndpointRouteBuilder> mapEndPoints)
        {
            var subscriptionTypeGroup = app.MapGroup("SubscriptionTypes");

            ///--------------------------------------------------------------------------
            /// <summary>
            /// Return a list of the subscription types and their related features.
            /// </summary>
            ///--------------------------------------------------------------------------
            subscriptionTypeGroup.MapGet("/", async (ISubscriptionTypeService subscriptionTypeService, 
                ILogger<SubscriptionType> logger, CancellationToken cancellationToken) =>
            {
                try
                {
                    var subscriptionTypes = await subscriptionTypeService.GetSubscriptionTypes();
                    return Results.Ok(subscriptionTypes);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.StackTrace);
                    return Results.Problem(ex.Message);
                }
            })
            .Produces<List<SubscriptionType>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get all subsription types")
            .WithDescription("Returns a list of all susbcription types in the system.");
        }
    }
}
