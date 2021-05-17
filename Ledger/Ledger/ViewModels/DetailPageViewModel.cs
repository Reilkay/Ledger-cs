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

namespace Ledger.ViewModels {
    public class DetailPageViewModel : BaseViewModel {

        private Record _selectedRecord;
        public ObservableCollection<Record> RecordCollection { get; }
        public Command<Record> RecordTappedCommand { get; }
        public Command LoadItemsCommand { get; }
        public Command AddRecordCommand { get; }
        public Command PageAppearingCommand { get; }

        private IDataStore<Record> _recordDataStore;


        public DetailPageViewModel(IDataStore<Record> recordDataStore) {
            _recordDataStore = recordDataStore;
            RecordCollection = new ObservableCollection<Record>(recordDataStore.GetRecords());
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            RecordTappedCommand = new Command<Record>(OnRecordSelected);
            AddRecordCommand = new Command(OnAddItem);
            PageAppearingCommand = new Command(OnAppearingAsync);
        }


        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                RecordCollection.Clear();
                var records = await DataStore.GetItemsAsync(true);
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

        public Record SelectedRecord
        {
            get => _selectedRecord;
            set
            {
                SetProperty(ref _selectedRecord, value);
                OnRecordSelected(value);
            }
        }

        public async void OnAppearingAsync() {
            IsBusy = true;
            SelectedRecord = null;
            await ExecuteLoadItemsCommand();
        }
    }
}
