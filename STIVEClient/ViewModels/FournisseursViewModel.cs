using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace STIVEClient.ViewModels;

public partial class  FournisseursViewModel:ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<string> _dropdownItems = new ObservableCollection<string>
    {
        "Ajouter",
        "Rechercher",
    };

    [ObservableProperty]
    private string? _selectedDropdownItem;

    [ObservableProperty]
    private bool _isRecherching; // <-  "Rechercher"

    [ObservableProperty]
    private bool _isAdding; // <- "Ajouter"

    partial void OnSelectedDropdownItemChanged(string? value)
    {
        IsAdding = value == "Ajouter";
        IsRecherching = value == "Rechercher";
    }
}