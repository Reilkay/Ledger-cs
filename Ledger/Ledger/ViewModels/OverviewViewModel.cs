using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using Ledger.Models;
using Ledger.Services;
using Xamarin.Forms;

namespace Ledger.ViewModels
{
    public class OverviewViewModel : BaseViewModel {
        private string _spent;
        private string _balance;
        private List<Record> _records;
        private DateTime _minDate, _maxDate, _selectedDate;

        public OverviewViewModel()
        {
            _minDate = new DateTime(2000, 1, 1);
            _maxDate = _selectedDate = DateTime.Today;
            SwitchCommand = new Command(OnDateSelected);
        }

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

        public DateTime MinDate
        {
            get => _minDate;
            set => SetProperty(ref _minDate, value);
        }

        public DateTime MaxDate
        {
            get => _maxDate;
            set => SetProperty(ref _maxDate, value);
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        /// <summary>
        /// 页面显示命令
        /// </summary>
        private RelayCommand _pageAppearingCommand;

        /// <summary>
        /// 页面显示命令
        /// </summary>
        public RelayCommand PageAppearingCommand => _pageAppearingCommand ??=
            new RelayCommand(() => PageAppearingCommandFunction());

        public Command SwitchCommand { get; }
        public void OnDateSelected() => FreshData();

        public void PageAppearingCommandFunction()
        {
            _selectedDate = DateTime.Today;
            FreshData();
        }
        
        private void FreshData() 
        {
            _records = DataStore.GetRecords();
            var _monthIncomeRecords = _records.Where(r =>
                r.Year == _selectedDate.Year &&
                r.Month == _selectedDate.Month && r.Budget == "收入").ToList();
            var _monthExpensesRecords = _records.Where(r =>
                r.Year == _selectedDate.Year &&
                r.Month == _selectedDate.Month && r.Budget == "支出").ToList();
            Spent = _monthExpensesRecords.Sum(r => r.Amount).ToString();
            Balance =
                (_monthIncomeRecords.Sum(r => r.Amount) - float.Parse(Spent))
                .ToString();
        }
    }
}
