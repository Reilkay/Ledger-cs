using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ledger.Models;
using Ledger.Services;
using Ledger.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Ledger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage {
        //private DetailPageViewModel _detailPageViewModel;
        public DetailPage()
        {
            InitializeComponent();
            //BindingContext = _detailPageViewModel = new DetailPageViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //_detailPageViewModel.OnAppearing();
        }

    }
}