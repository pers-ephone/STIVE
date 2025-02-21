using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace STIVEClient.ViewModels;

public partial class CommandesViewModel: ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<string> _dropdownItems = new ObservableCollection<string>
    {
        "Passer une commande",
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
        IsAdding = value == "Passer une commande";
        IsRecherching = value == "Rechercher";
    }
}