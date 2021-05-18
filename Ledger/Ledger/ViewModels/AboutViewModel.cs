using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Ledger.ViewModels
{
    /// <summary>
    /// 关于页
    /// </summary>
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "关于";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://gitee.com/reilkay-shinyakko/ledger"));
        }

        public ICommand OpenWebCommand { get; }
    }
}