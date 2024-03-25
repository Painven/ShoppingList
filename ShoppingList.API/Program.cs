var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

app.MapPost("/add-from-file", () =>
{
    return Results.Ok();
});
app.MapGet("/shop-list", () =>
{
    return Results.Ok();
});
app.MapGet("/set-item-state", () =>
{
    return Results.Ok();
});

app.Run();