using ReciboGeneratorApp.ViewModels;

namespace ReciboGeneratorApp.Views;

public partial class FilteredSearchPage : ContentPage
{
	public FilteredSearchPage(FilteredSearchViewModel vm)
	{
		InitializeComponent();

        BindingContext = vm;
    }
}