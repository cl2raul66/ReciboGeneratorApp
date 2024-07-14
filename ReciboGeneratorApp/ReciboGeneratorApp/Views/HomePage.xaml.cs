using ReciboGeneratorApp.ViewModels;

namespace ReciboGeneratorApp.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomeViewModel vm)
	{
		InitializeComponent();

        BindingContext = vm;
    }
}