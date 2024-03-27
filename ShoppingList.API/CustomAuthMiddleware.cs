using Microsoft.Extensions.Options;

namespace ShoppingList.API;

public class CustomAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ApiAccessToken serverToken;

    public CustomAuthMiddleware(RequestDelegate next, IOptions<ApiAccessToken> accessToken)
    {
        _next = next;
        serverToken = accessToken.Value;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("Authorization", out var userAuthHeader))
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)StatusCodes.Status401Unauthorized;
            return;
        }

        string? userToken = userAuthHeader.ToString();

        if (!userToken.Contains(serverToken.Value))
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)StatusCodes.Status403Forbidden;
            return;
        }

        await _next(context);
    }
}
