using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ReceiptGeneratorApp.Models;
using ReciboGeneratorApp.Services;
using ReciboGeneratorApp.Views;
using System.Collections.ObjectModel;

namespace ReciboGeneratorApp.ViewModels;

public partial class HomeViewModel : ObservableRecipient
{
    readonly ILiteDBService liteDBServ;
    readonly IPDFService pfDFServ;

    public HomeViewModel(ILiteDBService liteDBService, IPDFService pDFService)
    {
        HaveWorkshopInfo = Preferences.Get("WorkshopInfoSetting", false);
        liteDBServ = liteDBService;
        pfDFServ = pDFService;
        Documents = new(liteDBServ.GetAll());
    }

    [ObservableProperty]
    bool haveWorkshopInfo;

    [ObservableProperty]
    ObservableCollection<Receipt>? documents;

    [ObservableProperty]
    Receipt? selectedDocument;

    [RelayCommand]
    async Task ShowSettingPage()
    {
        IsActive = true;
        await Shell.Current.GoToAsync(nameof(SettingPage), true);
    }

    [RelayCommand]
    async Task ShowNewReceiptPage()
    {
        IsActive = true;
        await Shell.Current.GoToAsync(nameof(NewEditReceiptPage), true);
    }

    [RelayCommand]
    async Task ShowEditReceiptPage()
    {
        IsActive = true;
        Dictionary<string, object> sendObject = new()
        {
            { "CurrentReceipt", SelectedDocument!}
        };
        await Shell.Current.GoToAsync(nameof(NewEditReceiptPage), true, sendObject);
    }

    [RelayCommand]
    async Task ShowShareDocumenPage()
    {
        var result = await pfDFServ.GeneratedPDF(SelectedDocument!);
        if (!string.IsNullOrEmpty(result))
        {
            await Share.Default.RequestAsync(new ShareFileRequest
            {
                Title = $"Compartir documento como PDF",
                File = new ShareFile(result)
            });
        }
    }

    protected override void OnActivated()
    {
        base.OnActivated();

        WeakReferenceMessenger.Default.Register<HomeViewModel, string, string>(this, "HaveWorkshopInfo", (r, m) =>
        {
            bool result = bool.Parse(m);
            if (result)
            {
                HaveWorkshopInfo = Preferences.Get("WorkshopInfoSetting", false);
            }

            IsActive = false;
        });

        WeakReferenceMessenger.Default.Register<HomeViewModel, Receipt, string>(this, "AddDocument", (r, m) =>
        {
            bool result = liteDBServ.Insert(m);
            if (result)
            {                
                r.Documents!.Insert(0, m);
            }

            IsActive = false;
        });

        WeakReferenceMessenger.Default.Register<HomeViewModel, Receipt, string>(this, "EditDocument", (r, m) =>
        {
            bool result = liteDBServ.Update(m);
            if (result)
            {       
                int idx = r.Documents!.IndexOf(SelectedDocument!);
                r.Documents![idx] = m;
                SelectedDocument = null;
            }

            IsActive = false;
        });
    }

    #region EXTRA
    #endregion
}
