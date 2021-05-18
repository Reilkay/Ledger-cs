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
    /// <summary>
    /// 添加新记录页
    /// </summary>
    public class NewRecordViewModel : BaseViewModel {
        //金额
        private string _amount;
        //描述
        private string _description;
        //收支
        private string _incomeExpenses;
        //类型
        private string _type;
        //日期：最小日期，最大日期，选中日期
        public DateTime _minDate, _maxDate, _selectedDate;
        //记录存储对象
        private IDataStore<Record> _recordDataStore;
        public NewRecordViewModel(IDataStore<Record> recordDataStore) {
            _recordDataStore = recordDataStore;
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            _minDate = new DateTime(2000, 1, 1);
            _maxDate = _selectedDate = DateTime.Today;
        }

        /// <summary>
        /// 判断是否数值
        /// </summary>
        /// <param name="oText"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 判断输入空
        /// </summary>
        /// <returns></returns>
        private bool ValidateSave() {
            return !String.IsNullOrWhiteSpace(_amount) &&
                !String.IsNullOrWhiteSpace(_description) &&
                !String.IsNullOrWhiteSpace(_incomeExpenses) &&
                !String.IsNullOrWhiteSpace(_type) && IsNumberic(_amount);
        }

        /// <summary>
        /// 金额
        /// </summary>
        public string Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        /// <summary>
        /// 收支
        /// </summary>
        public string IncomeExpensesSelection 
        {
            get => _incomeExpenses;
            set => SetProperty(ref _incomeExpenses, value);
        }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type 
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        /// <summary>
        /// 最小日期
        /// </summary>
        public DateTime MinDate
        {
            get => _minDate;
            set => SetProperty(ref _minDate, value);
        }

        /// <summary>
        /// 最大日期
        /// </summary>
        public DateTime MaxDate
        {
            get => _maxDate;
            set => SetProperty(ref _maxDate, value);
        }

        /// <summary>
        /// 选中日期
        /// </summary>
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        /// <summary>
        /// 保存命令
        /// </summary>
        public Command SaveCommand { get; }

        /// <summary>
        /// 取消命令
        /// </summary>
        public Command CancelCommand { get; }

        /// <summary>
        /// 取消命令内容
        /// </summary>
        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        /// <summary>
        /// 保存命令内容
        /// </summary>
        private async void OnSave()
        {
            System.Diagnostics.Trace.WriteLine("shouru:"+IncomeExpensesSelection);
            System.Diagnostics.Trace.WriteLine("leixing:"+Type);
            System.Diagnostics.Trace.WriteLine(SelectedDate.Year+"/"+SelectedDate.Month+"/"+SelectedDate.Day);
            Record newRecord = new Record() {
                Id = (_recordDataStore.GetMaxId()+1).ToString(),
                Content = Description,
                Amount = float.Parse(Amount),
                Type = Type,
                Budget = IncomeExpensesSelection,
                Year = SelectedDate.Year,
                Month = SelectedDate.Month,
                Day = SelectedDate.Day
            };
            await DataStore.AddItemAsync(newRecord);
            _amount = null;
            _description = null;
            _incomeExpenses = null;
            _type = null;
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
