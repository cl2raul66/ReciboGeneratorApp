using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReceiptGeneratorApp.Models;
using ReciboGeneratorApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReciboGeneratorApp.ViewModels;

public partial class NewEditReceiptViewModel : ObservableValidator
{
    [ObservableProperty]
    string? nombreCliente;

    [ObservableProperty]
    string? telefonoCliente;

    [ObservableProperty]
    string? marcaVehiculo;

    [ObservableProperty]
    string? modeloVehiculo;

    [ObservableProperty]
    string? anioVehiculo;

    [ObservableProperty]
    ObservableCollection<ServiceDetail>? detallesServicio;

    [RelayCommand]
    void AgregarDetalle()
    {
        DetallesServicio ??= [];
        DetallesServicio.Add(new ServiceDetail());
    }

    [RelayCommand]
    async Task GuardarRecibo()
    {
        ValidateAllProperties();

        if (HasErrors)
        {
            await Task.Delay(4000);
            return;
        }
    }

    [RelayCommand]
    async Task Cancel()
    {
        await Shell.Current.GoToAsync("..", true);
    }
}
