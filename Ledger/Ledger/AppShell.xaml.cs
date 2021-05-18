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
            //子页路径注册
            Routing.RegisterRoute(nameof(NewRecordPage), typeof(NewRecordPage));
            Routing.RegisterRoute(nameof(RecordDetailPage),typeof(RecordDetailPage));
        }

    }
}
