using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ReceiptGeneratorApp.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ReciboGeneratorApp.ViewModels;

[QueryProperty(nameof(CurrentReceipt), nameof(CurrentReceipt))]
public partial class NewEditReceiptViewModel : ObservableValidator
{
    readonly WorkshopInfo workshopInfo;

    public NewEditReceiptViewModel()
    {
        workshopInfo = new()
        {
            Name = Preferences.Get("Name", string.Empty),
            Phone = Preferences.Get("Phone", string.Empty),
            Email = Preferences.Get("Email", string.Empty),
            Address = Preferences.Get("Address", string.Empty),
            PathLogo = Preferences.Get("PathLogo", string.Empty),
        };
    }

    [ObservableProperty]
    Receipt? currentReceipt;

    [ObservableProperty]
    string? title;

    [ObservableProperty]
    string? titleButton;

    [ObservableProperty]
    string? infoText;

    #region RECEIPTINFO
    [ObservableProperty]
    DateTime issueDate = DateTime.Now;

    [ObservableProperty]
    string? fileNumber;

    [ObservableProperty]
    string? totalPrice;
    #endregion

    #region CLIENTINFO
    [ObservableProperty]
    [Required]
    [MinLength(3)]
    string? name;

    [ObservableProperty]
    string? phone;

    [ObservableProperty]
    string? vehicle;
    #endregion

    #region SERVICE DETAIL
    [ObservableProperty]
    string? description;

    [ObservableProperty]
    string? price;

    [ObservableProperty]
    ObservableCollection<ServiceDetail>? details;

    [ObservableProperty]
    ServiceDetail? selectedDetail;
    #endregion

    [ObservableProperty]
    bool isEnableAgregarDetalle;

    [RelayCommand]
    async Task AddDetail()
    {
        if (!double.TryParse(Price, out double thePrice))
        {
            InfoText = "Ingrese un valor numérico.";
            await Task.Delay(4000);
            InfoText = null;
            return;
        }
        Details ??= [];
        Details.Add(new ServiceDetail() { Description = Description!.Trim().ToUpper(), Price = thePrice });
        Description = null;
        Price = null;
    }

    [RelayCommand]
    void RemoveDetail()
    {
        int idx = Details!.IndexOf(SelectedDetail!);
        Details!.RemoveAt(idx);
        SelectedDetail = null;
    }

    [RelayCommand]
    async Task SaveReceipt()
    {
        ValidateAllProperties();

        if (HasErrors && (Description is null || Description.Length == 0))
        {
            InfoText = "Ingrese todos los requeridos (*).";
            await Task.Delay(4000);
            InfoText = null;
            return;
        }

        Receipt receipt = new()
        {
            Id = CurrentReceipt is null ? null : CurrentReceipt.Id,
            WorkshopDetails = workshopInfo,
            ClientDetails = new() { Name = Name!.Trim().ToUpper(), Phone = Phone, Vehicle = Vehicle!.Trim().ToUpper() },
            ReceiptDetails = new() { IssueDate = IssueDate, FileNumber = FileNumber, TotalPrice = Details!.Sum(x => x.Price) },
            ServiceDetails = [.. Details]
        };

        var resul = CurrentReceipt is null 
            ? WeakReferenceMessenger.Default.Send(receipt, "AddDocument") 
            : WeakReferenceMessenger.Default.Send(receipt, "EditDocument");

        if (resul is not null)
        {
            await Shell.Current.GoToAsync("..", true);
        }
    }

    [RelayCommand]
    async Task Cancel()
    {
        await Shell.Current.GoToAsync("..", true);
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName == nameof(CurrentReceipt))
        {
            if (CurrentReceipt is not null)
            {
                Title = "Editar recibo";
                TitleButton = "Editar";

                IssueDate = CurrentReceipt.ReceiptDetails!.IssueDate;
                FileNumber = CurrentReceipt.ReceiptDetails!.FileNumber;
                TotalPrice = CurrentReceipt.ReceiptDetails!.TotalPrice?.ToString("C") ?? "0";

                Name = CurrentReceipt.ClientDetails!.Name;
                Phone = CurrentReceipt.ClientDetails!.Phone;
                Vehicle = CurrentReceipt.ClientDetails!.Vehicle;

                Details = [.. CurrentReceipt.ServiceDetails!];
            }
        }

        if (e.PropertyName == nameof(Description))
        {
            IsEnableAgregarDetalle = !string.IsNullOrEmpty(Description) && !string.IsNullOrEmpty(Price);
        }

        if (e.PropertyName == nameof(Price))
        {
            IsEnableAgregarDetalle = !string.IsNullOrEmpty(Description) && !string.IsNullOrEmpty(Price);
        }
    }
}
