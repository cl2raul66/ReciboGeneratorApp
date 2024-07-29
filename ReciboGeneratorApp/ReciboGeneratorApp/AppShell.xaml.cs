using ReciboGeneratorApp.Views;

namespace ReciboGeneratorApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(NewEditReceiptPage), typeof(NewEditReceiptPage));
        Routing.RegisterRoute(nameof(SettingPage), typeof(SettingPage));
    }
}
