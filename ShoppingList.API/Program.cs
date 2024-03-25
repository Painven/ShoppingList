using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.API.DataAccess;
using ShoppingList.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging(logging =>
 {
     logging.LoggingFields = HttpLoggingFields.All;
 });

builder.Services.AddSingleton<IShoppingListRepository, InMemoryShoppingListRepository>();

var app = builder.Build();
app.UseHttpLogging();

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
        return Results.NoContent();
    }
    return Results.Ok(data);
});
app.MapPut("/set-item-state/{fileId}", async ([FromServices] IShoppingListRepository repo, [FromRoute] string fileId, [FromBody] ShoppingListItem item) =>
{
    await repo.SetItemState(fileId, item.Name, item.IsComplete);
    return Results.Ok();
});

app.Run("http://localhost:8847");