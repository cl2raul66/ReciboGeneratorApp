using ReciboGeneratorApp.ViewModels;

namespace ReciboGeneratorApp.Views;

public partial class SettingPage : ContentPage
{
	public SettingPage(SettingViewModel vm)
	{
		InitializeComponent();

        BindingContext = vm;
    }
}