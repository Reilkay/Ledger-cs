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
        public DetailPage()
        {
            InitializeComponent();
        }

    }
}