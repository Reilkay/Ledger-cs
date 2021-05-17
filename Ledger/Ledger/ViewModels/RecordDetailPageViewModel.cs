using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using GalaSoft.MvvmLight;
using Ledger.Models;
using Ledger.Services;
using Ledger.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Ledger.ViewModels
{
    [QueryProperty(nameof(RecordId), nameof(RecordId))]
    public class RecordDetailPageViewModel : BaseViewModel {
        private Record record;
        private string _recordId;
        private float _amount;
        private string _type;
        private string _time;
        private string _content;
        private string _budget;
        private string _id;

        public Command DeleteRecordCommand { get; }

        public RecordDetailPageViewModel() {
            DeleteRecordCommand = new Command(OnDelete);
        }

        private async void OnDelete() {
            await DataStore.DeleteItemAsync(record.Id);
            await Shell.Current.GoToAsync("..");
        }

        public float Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }

        public string Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        public string Content {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        public string Budget {
            get => _budget;
            set => SetProperty(ref _budget, value);
        }

        public string Time {
            get => _time;
            set => SetProperty(ref _time, value);
        }

        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string RecordId
        {
            get
            {
                return _recordId;
            }
            set
            {
                _recordId = value;
                LoadItemId(value);
            }
        }
        public async void LoadItemId(string recordId)
        {
            try {
                record = await DataStore.GetItemAsync(recordId);
                //Guid = record.Guid;
                Id = record.Id;
                Amount = record.Amount;
                Type = record.Type;
                Content = record.Content;
                Budget = record.Budget;
                Time = $"{record.Year:0000}/{record.Month:00}/{record.Day:00}";
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
