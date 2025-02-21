using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using STIVEClient.Services;
using LibrarySTIVE.Models;

namespace STIVEClient.ViewModels;

public partial class ClientsViewModel : ViewModelBase
{
    private readonly APIserviceClient _apiService = new APIserviceClient();

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

    [ObservableProperty]
    private ObservableCollection<ClientSTIVEJSON> _clients = new();

    [ObservableProperty]
    private ClientSTIVEJSON _newClient = new();

    partial void OnSelectedDropdownItemChanged(string? value)
    {
        IsAdding = value == "Ajouter";
        IsRecherching = value == "Rechercher";
    }

    [RelayCommand]
    public async Task LoadClientsAsync()
    {
        var clients = await APIserviceClient.GetALLAsync();
        _clients.Clear();
        foreach (var client in clients)
        {
            _clients.Add(client);
        }
    }
    

}