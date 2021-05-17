using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using Ledger.Models;

namespace Ledger.ViewModels
{
    public class OverviewViewModel : BaseViewModel {
        private string _spent;
        private string _balance;

        public string Spent 
        {
            get => _spent;
            set => SetProperty(ref _spent, value);
        }

        public string Balance 
        {
            get => _balance;
            set => SetProperty(ref _balance, value);
        }

        /// <summary>
        /// 页面显示命令
        /// </summary>
        private RelayCommand _pageAppearingCommand;

        /// <summary>
        /// 页面显示命令
        /// </summary>
        public RelayCommand PageAppearingCommand => _pageAppearingCommand ??=
            new RelayCommand(async () => await PageAppearingCommandFunction());

        public async Task PageAppearingCommandFunction() {
            Balance = 1000.ToString();
            Spent = 15000.ToString();
        }

    }
}
