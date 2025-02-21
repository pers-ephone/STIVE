
using Avalonia.Controls;
using STIVEClient.ViewModels;

namespace STIVEClient.Views
{
    public partial class ArticlesView : UserControl
    {
        public ArticlesView()
        {
            InitializeComponent();
            DataContext = new ArticlesViewModel();
        }

    }
}