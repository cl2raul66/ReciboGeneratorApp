using ReciboGeneratorApp.ViewModels;

namespace ReciboGeneratorApp.Views;

public partial class PreviewReceiptPage : ContentPage
{
	public PreviewReceiptPage(PreviewReceiptViewModel vm)
	{
		InitializeComponent();

        BindingContext = vm;
    }
}