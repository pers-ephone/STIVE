using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using STIVEClient.Services;

namespace STIVEClient.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private string _loginInput;

    [ObservableProperty] private string _passwordInput;
    
    [ObservableProperty]
    private bool _isPaneOpen = false;

    [ObservableProperty]
    private ViewModelBase _currentPage = new ArticlesViewModel();

    [ObservableProperty]
    private ListItemTemplate? _selectedListItem;

    [ObservableProperty]
    private bool _isVisibleGrid = true;

    [ObservableProperty]
    private ObservableCollection<string> _dropdownItems = new ObservableCollection<string>
    {
        "ajouter un produit",
        "rechercher un produit",
    };

    [ObservableProperty]
    private string? _selectedDropdownItem;

    partial void OnSelectedDropdownItemChanged(string? value)
    {
        if (value != null)
        {
            Console.WriteLine($"Selected Dropdown Item: {value}");
        }
    }
    
    [RelayCommand]
    public void Login()
    {
        string adminUsername = "admin"; 
        string hashPassword = "$2a$11$MrgAz2RSbFBkTGOLZ00oSelGvkkgOfdYWfeB3t5sbBGmjyt.X5XZ2";
        if (LoginInput == adminUsername && LoginService.VerifyPassword(PasswordInput, hashPassword))
        {
            Console.WriteLine("Login successful");
            ToggleLoginPageVisibility();
        }
        else
        {
            Console.WriteLine("Invalid login attempt");
        }
    }
    

    [RelayCommand]
    public void ToggleLoginPageVisibility()
    {
        IsVisibleGrid = !IsVisibleGrid;
    }

    [RelayCommand]
    partial void OnSelectedListItemChanged(ListItemTemplate? value)
    {
        if (value is null) return;
        var instance = Activator.CreateInstance(value.ModelType);
        if (instance is null) return;
        CurrentPage = (ViewModelBase)instance;
    }

    public ObservableCollection<ListItemTemplate> Items { get; } = new()
    {
        new ListItemTemplate(typeof(ArticlesViewModel)),
        new ListItemTemplate(typeof(CommandesViewModel)),
        new ListItemTemplate(typeof(FournisseursViewModel)),
        new ListItemTemplate(typeof(ClientsViewModel)),
        new ListItemTemplate(typeof(DeconnexionViewModel)),
    };

    [RelayCommand]
    private void TriggerPane()
    {
        IsPaneOpen = !IsPaneOpen;
    }
}

public class ListItemTemplate
{
    public ListItemTemplate(Type type, bool hasDropdown = false)
    {
        ModelType = type;
        Label = type.Name.Replace("ViewModel", "");
    }

    public string Label { get; }
    public Type ModelType { get; }

}
