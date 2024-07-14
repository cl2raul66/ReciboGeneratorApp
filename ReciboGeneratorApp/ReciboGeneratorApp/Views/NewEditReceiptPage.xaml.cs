using ReciboGeneratorApp.ViewModels;

namespace ReciboGeneratorApp.Views;

public partial class NewEditReceiptPage : ContentPage
{
	public NewEditReceiptPage(NewEditReceiptViewModel vm)
	{
		InitializeComponent();

        BindingContext = vm;
    }
}