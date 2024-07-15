using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReciboGeneratorApp.ViewModels;

public partial class SettingViewModel : ObservableValidator
{
    public SettingViewModel()
    {
        LoadPreferences();
    }

    #region WORKSHOPINFO
    FileResult? imageLoaded = null;

    [ObservableProperty]
    bool isVisibleInfoForWorkshopInfo;

    [ObservableProperty]
    [Required]
    [MinLength(3)]
    string? name;

    [ObservableProperty]
    [Phone]
    string? phone;

    [ObservableProperty]
    [EmailAddress]
    string? email;

    [ObservableProperty]
    [Required]
    [MinLength(3)]
    string? address;

    [ObservableProperty]
    ImageSource? logoImageSource;

    [RelayCommand]
    async Task LoadLogo()
    {
        imageLoaded = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Images,
            PickerTitle = "Seleccione una imagen como logo"
        });

        if (imageLoaded is null)
        {
            return;
        }

        LogoImageSource = ImageSource.FromFile(imageLoaded.FullPath);
    }

    [RelayCommand]
    async Task SavePreferences()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            IsVisibleInfoForWorkshopInfo = true;
            await Task.Delay(4000);
            IsVisibleInfoForWorkshopInfo = false;
            return;
        }

        Preferences.Set(nameof(Name), Name);
        Preferences.Set(nameof(Phone), Phone);
        Preferences.Set(nameof(Email), Email);
        Preferences.Set(nameof(Address), Address);

        Preferences.Set("WorkshopInfoSetting", true);

        if (imageLoaded is not null)
        {
            var destinationPath = Path.Combine(FileSystem.AppDataDirectory, Path.GetFileName(imageLoaded!.FullPath));
            using (var sourceStream = await imageLoaded.OpenReadAsync())
            using (var destinationStream = File.Create(destinationPath))
            {
                await sourceStream.CopyToAsync(destinationStream);
            }
            Preferences.Set(nameof(LogoImageSource), destinationPath);
        }
    }
    #endregion

    [RelayCommand]
    async Task GoToBack()
    {
        await Shell.Current.GoToAsync("..", true);
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
    }

    #region EXTRA
    void LoadPreferences()
    {
        Name = Preferences.Get(nameof(Name), string.Empty);
        Phone = Preferences.Get(nameof(Phone), string.Empty);
        Email = Preferences.Get(nameof(Email), string.Empty);
        Address = Preferences.Get(nameof(Address), string.Empty);

        var logoPath = Preferences.Get(nameof(LogoImageSource), string.Empty);
        if (!string.IsNullOrEmpty(logoPath))
        {
            LogoImageSource = ImageSource.FromFile(logoPath);
        }
    }
    #endregion
}
