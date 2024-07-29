using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ReciboGeneratorApp.Services;
using ReciboGeneratorApp.ViewModels;
using ReciboGeneratorApp.Views;

namespace ReciboGeneratorApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("icofont.ttf", "icofont");
            });

        builder.Services.AddSingleton<ILiteDBService, LiteDBService>();
        builder.Services.AddSingleton<IPDFService, PDFService>();

        builder.Services.AddSingleton<HomePage, HomeViewModel>();
        builder.Services.AddTransient<NewEditReceiptPage, NewEditReceiptViewModel>();
        builder.Services.AddTransient<SettingPage, SettingViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
