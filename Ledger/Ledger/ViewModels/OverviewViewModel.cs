using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using Ledger.Models;
using Ledger.Services;

namespace Ledger.ViewModels
{
    public class OverviewViewModel : BaseViewModel {
        private string _spent;
        private string _balance;
        private List<Record> _records;

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
            FreshData();
        }

        private void FreshData() {
            _records = DataStore.GetRecords();
            var _monthIncomeRecords = _records.Where(r =>
                r.Year == DateTime.Today.Year &&
                r.Month == DateTime.Today.Month && r.Budget == "收入").ToList();
            var _monthExpensesRecords = _records.Where(r =>
                r.Year == DateTime.Today.Year &&
                r.Month == DateTime.Today.Month && r.Budget == "支出").ToList();
            Spent = _monthExpensesRecords.Sum(r => r.Amount).ToString();
            Balance =
                (_monthIncomeRecords.Sum(r => r.Amount) - float.Parse(Spent))
                .ToString();
        }
    }
}
