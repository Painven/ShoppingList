using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using ShoppingList.Core;
using ShoppingListMobile.ViewModels;
using System.Reflection;

namespace ShoppingListMobile;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru-RU");

        var builder = MauiApp.CreateBuilder();

        builder.AddJsonSettings();
        builder.Services.Configure<ApiEndpointConfiguration>(builder.Configuration.GetSection(ApiEndpointConfiguration.SectionName));
        builder.Services.AddHttpClient();
        builder.Services.AddSingleton<ShoppingListApiClient>();
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<MainPage>(x => new MainPage(x.GetService<MainPageViewModel>()));

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    private static MauiAppBuilder AddJsonSettings(this MauiAppBuilder builder)
    {
        var assembly = typeof(App).GetTypeInfo().Assembly;

        var config = new ConfigurationBuilder()
            .AddJsonFile(new EmbeddedFileProvider(assembly), "appsettings.json", optional: false, false)
            .Build();

        builder.Configuration.AddConfiguration(config);

        return builder;
    }
}
