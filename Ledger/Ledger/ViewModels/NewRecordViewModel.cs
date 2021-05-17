using Ledger.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Ledger.ViewModels
{
    public class NewRecordViewModel : BaseViewModel
    {
        private string _amount;
        private string _description;
        private string _incomeExpenses;
        private string _type;
        public DateTime MinDate, MaxDate, SelectedDate;

        public NewRecordViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            MinDate = new DateTime(2000, 1, 1);
            MaxDate = SelectedDate = DateTime.Today;
        }

        private bool ValidateSave() {
            return !String.IsNullOrWhiteSpace(_amount) &&
                !String.IsNullOrWhiteSpace(_description) &&
                !String.IsNullOrWhiteSpace(_incomeExpenses) &&
                !String.IsNullOrWhiteSpace(_type);
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
            //Item newItem = new Item()
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Text = Text,
            //    Description = Description
            //};
            System.Diagnostics.Trace.WriteLine("shouru:"+IncomeExpensesSelection);
            System.Diagnostics.Trace.WriteLine("leixing:"+Type);
            System.Diagnostics.Trace.WriteLine(SelectedDate.Year+"/"+SelectedDate.Month+"/"+SelectedDate.Day);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
