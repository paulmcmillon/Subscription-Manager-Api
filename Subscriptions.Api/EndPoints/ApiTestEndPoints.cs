namespace Subscriptions.Api.EndPoints
{
    public static class ApiTestEndPoints
    {
        public static void MapEndPoints(this IEndpointRouteBuilder app, Action<IEndpointRouteBuilder> mapEndPoints)
        {
            var apiTestGroup = app.MapGroup("ApiTest");

            apiTestGroup.MapGet("/", () => "Api is up and running")
                .WithName("ApiTest")
                .WithSummary("API Test")
                .WithDescription("Returns a simple string to confirm the API is up and running.");
        }
    }
}
