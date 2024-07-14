using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReceiptGeneratorApp.Models;
using ReciboGeneratorApp.Services;
using ReciboGeneratorApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReciboGeneratorApp.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    [RelayCommand]
    async Task ShowSettingPage()
    {
        await Shell.Current.GoToAsync(nameof(SettingPage), true);
    }

    [RelayCommand]
    async Task ShowNewEditReceiptPage()
    {
        await Shell.Current.GoToAsync(nameof(NewEditReceiptPage), true);
    }

    [RelayCommand]
    async Task ShowFilteredSearchPage()
    {
        await Shell.Current.GoToAsync(nameof(FilteredSearchPage), true);
    }

    [RelayCommand]
    async Task ShowPreviewReceiptPage()
    {
        await Shell.Current.GoToAsync(nameof(PreviewReceiptPage), true);
    }
}
