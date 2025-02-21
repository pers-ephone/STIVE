using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibrarySTIVE.Models;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using STIVEClient.Services;

namespace STIVEClient.ViewModels
{
    public partial class ArticlesViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<ModelArticleJson> _articles = new ObservableCollection<ModelArticleJson>();

        [ObservableProperty]
        private ObservableCollection<ModelArticleJson> _filteredArticles = new ObservableCollection<ModelArticleJson>();
        //----------------------------------------------------------------COMBO_BOX----------------------------------------------------------------------------

        [ObservableProperty]
        private ObservableCollection<string> _dropdownItems = new ObservableCollection<string>
        {
            "Ajouter",
            "Rechercher",
            "Supprimer",
            "Modifier"
        };

        [ObservableProperty]
        private string? _selectedDropdownItem;

        [ObservableProperty]
        private bool _isRecherching;
        [ObservableProperty]
        private bool _isAdding;
        [ObservableProperty]
        private bool _isDeleting;
        [ObservableProperty]
        private bool _isEditing;

        //----------------------------------------------------------------------------------------------------------------------------------------------------------//

        [ObservableProperty]
        private string _searchName = "";

        [ObservableProperty]
        private string _searchFamille = "";

        [ObservableProperty]
        private string _searchReference = "";

        [ObservableProperty]
        private string _searchPrix = "";

        [ObservableProperty]
        private string _searchAge = "";

        [ObservableProperty]
        private string _searchQuantite = "";
        //----------------------------------------------------------------------------------------------------------------------------------------------------------//

        //--------------------------------------------------------POST-Variables------------------------------------------------------------------------------------------------//
        [ObservableProperty]
        private string _newName = "";
        [ObservableProperty]
        private string _newFamille = "";
        [ObservableProperty]
        private string _newReference = "";
        [ObservableProperty]
        private string _newPrix = "";
        [ObservableProperty]
        private string _newAge = "";
        [ObservableProperty]
        private string _newQuantite = "";

        //-------------------------------------------------------------------DELETING-----------------------------------------------------------------------------------//
        [ObservableProperty]
        private string _deleteArticleId;
        //---------------------------------------------------------------------UPDATING-----------------------------------------------------------------------------------//
        [ObservableProperty]
        private string _idForUpdate = "";
        [ObservableProperty]
        private string _nomForUpdate = "";
        [ObservableProperty]
        private string _familleIdForUpdate = "";
        [ObservableProperty]
        private string _refForUpdate = "";
        [ObservableProperty]
        private string _prixForUpdate = "";
        [ObservableProperty]
        private string _artQuantityForUpdate = "";
        //----------------------------------------------------------------------------------------------------------------------------------------------------------//

        partial void OnSelectedDropdownItemChanged(string? value)
        {
            IsAdding = value == "Ajouter";
            IsRecherching = value == "Rechercher";
            IsDeleting = value == "Supprimer";
            IsEditing = value == "Modifier";

            if (IsRecherching)
            {
                _ = LoadArticlesAsync();
            }
        }

        public ArticlesViewModel()
        {
            _ = LoadArticlesAsync();
        }

        [RelayCommand]
        public void SearchByNameAndPrice()
        {
            if (Articles.Count == 0) return;
            FilteredArticles.Clear();
            foreach (var article in Articles.Where(a =>
                (string.IsNullOrEmpty(SearchName) || a.Nom.Contains(SearchName, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(SearchFamille) || a.FamilleId.ToString().Contains(SearchFamille, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(SearchReference) || a.Ref.Contains(SearchReference, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(SearchPrix) || a.Prix.ToString().Contains(SearchPrix)) &&
                (string.IsNullOrEmpty(SearchAge) || a.Age.ToString().Contains(SearchAge)) &&
                (string.IsNullOrEmpty(SearchQuantite) || a.ArtQuantity.ToString().Contains(SearchQuantite))))
            {
                FilteredArticles.Add(article);
            }
        }


        private async Task LoadArticlesAsync()
        {
            try
            {
                var articles = await APIserviceArticle.GetAllArticles();
                if (articles != null)
                {
                    Console.WriteLine($"[DEBUG] Loaded {articles.Count} articles");
                    Articles.Clear();
                    foreach (var article in articles)
                    {
                        Console.WriteLine($"[DEBUG] Article: {article.Nom} - {article.Prix}€");
                        Articles.Add(article);
                    }

                    FilteredArticles.Clear();
                    foreach (var article in Articles)
                    {
                        FilteredArticles.Add(article);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to load articles: {ex.Message}");
            }
        }


        [RelayCommand]
        public async Task AddArticleAsync()
        {
            if (string.IsNullOrWhiteSpace(NewName) || string.IsNullOrWhiteSpace(NewFamille))
            {
                await MessageBoxManager.GetMessageBoxStandard(
                    "Erreur",
                    "Veuillez remplir tous les champs obligatoires.",
                    ButtonEnum.Ok,
                    Icon.Error
                ).ShowAsync();
                return;
            }

            var newArticle = new ModelArticleJson
            {
                Nom = NewName,
                FamilleId = 1,
                Ref = NewReference,
                Prix = int.TryParse(NewPrix, out var prix) ? prix : 0,
                Age = DateTime.TryParse(NewAge, out var age) ? age : DateTime.Now,
                ArtQuantity = int.TryParse(NewQuantite, out var quantity) ? quantity : 0
            };

            bool success = await APIserviceArticle.CreateArticle(newArticle);

            if (success)
            {
                Articles.Add(newArticle);
                NewName = NewFamille = NewReference = NewPrix = NewAge = NewQuantite = "";

                await MessageBoxManager.GetMessageBoxStandard(
                    "Succès",
                    "L'article a été ajouté avec succès.",
                    ButtonEnum.Ok,
                    Icon.Success
                ).ShowAsync();
            }
            else
            {
                await MessageBoxManager.GetMessageBoxStandard(
                    "Erreur",
                    "Échec de l'ajout de l'article.",
                    ButtonEnum.Ok,
                    Icon.Error
                ).ShowAsync();
            }
        }

        [RelayCommand]
        public async Task DeleteArticleAsync()
        {
            if (!int.TryParse(DeleteArticleId, out int articleId) || articleId <= 0)
            {
                await MessageBoxManager.GetMessageBoxStandard(
                    "Erreur",
                    "ID d'article invalide.",
                    ButtonEnum.Ok,
                    Icon.Error
                ).ShowAsync();
                return;
            }

            var confirmationBox = MessageBoxManager.GetMessageBoxStandard(
                "Confirmation",
                "Êtes-vous sûr de vouloir supprimer cet article ?",
                ButtonEnum.YesNo,
                Icon.Warning
            );

            var result = await confirmationBox.ShowAsync();

            if (result == ButtonResult.Yes)
            {
                bool success = await APIserviceArticle.DeleteArticle(articleId);
                if (success)
                {
                    var articleToRemove = Articles.FirstOrDefault(a => a.Id == articleId);
                    if (articleToRemove != null)
                    {
                        Articles.Remove(articleToRemove);
                    }
                    DeleteArticleId = "";

                    await MessageBoxManager.GetMessageBoxStandard(
                        "Succès",
                        "L'article a été supprimé avec succès.",
                        ButtonEnum.Ok,
                        Icon.Success
                    ).ShowAsync();
                }
                else
                {
                    await MessageBoxManager.GetMessageBoxStandard(
                        "Erreur",
                        "Échec de la suppression de l'article.",
                        ButtonEnum.Ok,
                        Icon.Error
                    ).ShowAsync();
                }
            }
        }

        [RelayCommand]
        public async Task UpdateArticleAsync()
        {
            if (string.IsNullOrWhiteSpace(NomForUpdate) || string.IsNullOrWhiteSpace(FamilleIdForUpdate))
            {
                await MessageBoxManager.GetMessageBoxStandard(
                    "Erreur",
                    "Veuillez remplir tous les champs obligatoires.",
                    ButtonEnum.Ok,
                    Icon.Error
                ).ShowAsync();
                return;
            }

            var updatedArticle = new ModelArticleJson
            {
                Id = 9,
                Nom = NomForUpdate,
                FamilleId = int.TryParse(FamilleIdForUpdate, out var familleId) ? familleId : 1,
                Ref = RefForUpdate,
                Prix = int.TryParse(PrixForUpdate, out var prix) ? prix : 0,
                ArtQuantity = int.TryParse(ArtQuantityForUpdate, out var quantity) ? quantity : 0
            };

            bool success = await APIserviceArticle.UpdateArticle(updatedArticle.Id, updatedArticle);

            if (success)
            {
                var existingArticle = Articles.FirstOrDefault(a => a.Id == updatedArticle.Id);
                if (existingArticle != null)
                {
                    existingArticle.Nom = updatedArticle.Nom;
                    existingArticle.FamilleId = updatedArticle.FamilleId;
                    existingArticle.Ref = updatedArticle.Ref;
                    existingArticle.Prix = updatedArticle.Prix;
                    existingArticle.ArtQuantity = updatedArticle.ArtQuantity;
                }

                await MessageBoxManager.GetMessageBoxStandard(
                    "Succès",
                    "L'article a été mis à jour avec succès.",
                    ButtonEnum.Ok,
                    Icon.Success
                ).ShowAsync();
            }
            else
            {
                await MessageBoxManager.GetMessageBoxStandard(
                    "Erreur",
                    "Échec de la mise à jour de l'article.",
                    ButtonEnum.Ok,
                    Icon.Error
                ).ShowAsync();
            }
        }




    }
}
