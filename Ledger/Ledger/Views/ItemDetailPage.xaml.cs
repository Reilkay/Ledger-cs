using Ledger.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Ledger.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}