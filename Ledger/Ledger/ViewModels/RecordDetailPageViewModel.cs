using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using GalaSoft.MvvmLight;
using Ledger.Models;
using Ledger.Services;
using Ledger.Views;
using Xamarin.Forms;

namespace Ledger.ViewModels
{
    [QueryProperty(nameof(RecordId), nameof(RecordId))]
    public class RecordDetailPageViewModel : BaseViewModel {
        private DetailPageViewModel _detailPageViewModel;
        private string _recordId;
        private float _amount;
        private string _type;
        //private string _time;

        public string Guid { get; set; }

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

        public string RecordId {
            get => _recordId;
            set
            {
                _recordId = value;
                LoadItemId(value);
            }
        }
        public async void LoadItemId(string recordId)
        {
            try {
                var record = await IDataStore.GetItemAsync(recordId);
                Guid = record.Guid;
                Amount = record.Amount;
                Type = record.Type;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
