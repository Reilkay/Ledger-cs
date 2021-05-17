using Ledger.ViewModels;
using Ledger.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Ledger
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewRecordPage), typeof(NewRecordPage));
            Routing.RegisterRoute(nameof(RecordDetailPage),typeof(RecordDetailPage));
        }

    }
}
