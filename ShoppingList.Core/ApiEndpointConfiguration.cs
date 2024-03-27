namespace ShoppingList.Core;

public class ApiEndpointConfiguration
{
    public static readonly string SectionName = "ApiEndpointConfiguration";

    public string Host { get; set; }
    public string Token { get; set; }
}
