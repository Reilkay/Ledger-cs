using Ledger.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Ledger.Services;
using Xamarin.Forms;

namespace Ledger.ViewModels
{
    public class NewRecordViewModel : BaseViewModel {
        private string _amount;
        private string _description;
        private string _incomeExpenses;
        private string _type;
        public DateTime MinDate, MaxDate, SelectedDate;
        private IDataStore<Record> _recordDataStore;
        public NewRecordViewModel(IDataStore<Record> recordDataStore) {
            _recordDataStore = recordDataStore;
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            MinDate = new DateTime(2000, 1, 1);
            MaxDate = SelectedDate = DateTime.Today;
        }

        private bool IsNumberic(string oText)
        {
            try {
                float var1 = float.Parse(oText);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool ValidateSave() {
            return !String.IsNullOrWhiteSpace(_amount) &&
                !String.IsNullOrWhiteSpace(_description) &&
                !String.IsNullOrWhiteSpace(_incomeExpenses) &&
                !String.IsNullOrWhiteSpace(_type) && IsNumberic(_amount);
        }

        public string Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string IncomeExpensesSelection 
        {
            get => _incomeExpenses;
            set => SetProperty(ref _incomeExpenses, value);
        }

        public string Type 
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            System.Diagnostics.Trace.WriteLine("shouru:"+IncomeExpensesSelection);
            System.Diagnostics.Trace.WriteLine("leixing:"+Type);
            System.Diagnostics.Trace.WriteLine(SelectedDate.Year+"/"+SelectedDate.Month+"/"+SelectedDate.Day);
            Record newRecord = new Record() {
                Id = _recordDataStore.GetMaxId()+1.ToString(),
                Content = Description,
                Amount = float.Parse(Amount),
                Type = Type,
                Budget = IncomeExpensesSelection,
                Year = SelectedDate.Year,
                Month = SelectedDate.Month,
                Day = SelectedDate.Day
            };
            await DataStore.AddItemAsync(newRecord);
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
