using Microsoft.AspNetCore.Mvc;
using ShoppingList.API;
using ShoppingList.API.DataAccess;
using ShoppingList.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IShoppingListRepository, InMemoryShoppingListRepository>();
builder.Services.Configure<ApiAccessToken>(builder.Configuration.GetSection(nameof(ApiAccessToken)));

builder.Services.AddHttpLogging(o => { });

var app = builder.Build();

app.UseHttpLogging();

app.UseMiddleware<CustomAuthMiddleware>();

app.MapPost("/add-from-file", async ([FromServices] IShoppingListRepository repo, [FromBody] ShoppingListFileTransferData file) =>
{
    await repo.AddNewListFromFile(file);
    return Results.Ok();
});
app.MapGet("/shop-list", async ([FromServices] IShoppingListRepository repo) =>
{
    var data = await repo.GetLastShoppingList();
    if (data is null)
    {
        return Results.Ok(new ShoppingListFileTransferData());
    }
    return Results.Ok(data);
});
app.MapPut("/set-item-state/{fileId}", async ([FromServices] IShoppingListRepository repo, [FromRoute] string fileId, [FromBody] ShoppingListItem item) =>
{
    await repo.SetItemState(fileId, item.Name, item.IsComplete);
    return Results.Ok();
});

app.Run();