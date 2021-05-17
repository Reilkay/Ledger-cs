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
        private string _textLeft, _textRight;

        public OverviewViewModel()
        {
            _minDate = new DateTime(2000, 1, 1);
            _maxDate = _selectedDate = DateTime.Today;
            _textLeft = "当月支出";
            _textRight = "当月剩余";
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

        public string TextLeft
        {
            get => _textLeft;
            set => SetProperty(ref _textLeft, value);
        }

        public string TextRight
        {
            get => _textRight;
            set => SetProperty(ref _textRight, value);
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
            TextLeft = "当月支出";
            TextRight = "当月剩余";
            FreshData();
        }
        
        private void FreshData() 
        {
            if (_selectedDate.Year != DateTime.Today.Year ||
                _selectedDate.Month != DateTime.Today.Month) {
                TextLeft = _selectedDate.Year + "年" + _selectedDate.Month + "月支出";
                TextRight = _selectedDate.Year + "年" + _selectedDate.Month + "月剩余";
            } else {
                TextLeft = "当月支出";
                TextRight = "当月剩余";
            }
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
