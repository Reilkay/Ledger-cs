using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using Ledger.Services;
using Ledger.Models;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ledger.Views;
using Xamarin.Forms;
using Xamarin.Forms.Extended;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Ledger.ViewModels {
    /// <summary>
    /// 明细页
    /// </summary>
    public class DetailPageViewModel : BaseViewModel {
        /// <summary>
        /// 当前选中的记录
        /// </summary>
        private Record _selectedRecord;

        /// <summary>
        /// 记录Collection
        /// </summary>
        public ObservableCollection<Record> RecordCollection { get; }

        /// <summary>
        /// 点击记录命令
        /// </summary>
        public Command<Record> RecordTappedCommand { get; }

        /// <summary>
        /// 加载记录命令
        /// </summary>
        public Command LoadItemsCommand { get; }

        /// <summary>
        /// 添加记录命令
        /// </summary>
        public Command AddRecordCommand { get; }

        /// <summary>
        /// 页面显示命令
        /// </summary>
        public Command PageAppearingCommand { get; }
        
        public DetailPageViewModel(IDataStore<Record> recordDataStore) {
            RecordCollection = new ObservableCollection<Record>(recordDataStore.GetRecords());
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            RecordTappedCommand = new Command<Record>(OnRecordSelected);
            AddRecordCommand = new Command(OnAddItem);
            PageAppearingCommand = new Command(OnAppearingAsync);
        }

        /// <summary>
        /// 加载记录命令内容
        /// </summary>
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                RecordCollection.Clear();
                var records = await DataStore.GetItemsAsync(true);
                records = records.OrderByDescending(p => p.Year).ThenByDescending(p => p.Month)
                    .ThenByDescending(p => p.Day).ThenByDescending(p => p.Id);

                foreach (var record in records)
                {
                    RecordCollection.Add(record);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// 添加记录命令内容
        /// </summary>
        /// <param name="obj"></param>
        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewRecordPage));
        }

        async void OnRecordSelected(Record record)
        {
            if (record == null)
                return;
            IEnumerable<Record> b = await DataStore.GetItemsAsync();
            // This will push the ItemDetailPage onto the navigation stack
            string a =
                $"{nameof(RecordDetailPage)}?{nameof(RecordDetailPageViewModel.RecordId)}={record.Id}";
            await Shell.Current.GoToAsync(a);
        }

        /// <summary>
        /// 当前选中记录
        /// </summary>
        public Record SelectedRecord
        {
            get => _selectedRecord;
            set
            {
                SetProperty(ref _selectedRecord, value);
                OnRecordSelected(value);
            }
        }

        /// <summary>
        /// 页面显示命令内容
        /// </summary>
        public async void OnAppearingAsync() {
            IsBusy = true;
            SelectedRecord = null;
            await ExecuteLoadItemsCommand();
        }
    }
}
