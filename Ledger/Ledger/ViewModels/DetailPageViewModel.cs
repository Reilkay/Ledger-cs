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
        public Command AddItemCommand { get; }

        private IDataStore<Record> _recordDataStore;

        public DetailPageViewModel(IDataStore<Record> recordDataStore) {
            _recordDataStore = recordDataStore;
            RecordCollection = new ObservableCollection<Record>(recordDataStore.GetRecords());
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            RecordTappedCommand = new Command<Record>(OnRecordSelected);
            AddItemCommand = new Command(OnAddItem);
        }


        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            try
            {
                RecordCollection.Clear();

                var items = await RDataStore.GetItemsAsync(true);
                foreach (var record in RecordCollection)
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
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnRecordSelected(Record record)
        {
            if (record == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(RecordDetailPage)}?{nameof(RecordDetailPageViewModel.RecordId)}={record.Guid}");
        }

        public async Task OnAppearing()
        {
            IsBusy = true;
            _selectedRecord = null;
        }

        /**
        private IRecordStorage _recordStorage;

        public DetailPageViewModel(IRecordStorage recordStorage) {
            _recordStorage = recordStorage;

            RecordTapped = new Command<Record>(OnRecordSelected);
            RecordCollection = new InfiniteScrollCollection<Record>();
            RecordCollection.OnCanLoadMore = () => _canLoadMore;
            RecordCollection.OnLoadMore = async () => {
                Status = Loading;
                var records = await recordStorage.GetRecordsAsync(Where,
                    RecordCollection.Count, PageSize);
                Status = String.Empty;

                if (records.Count < PageSize)
                {
                    _canLoadMore = false;
                    Status = NO_MORE_RESULT;
                }

                if (RecordCollection.Count == 0 && records.Count == 0)
                {
                    Status = NO_RESULT;
                }

                return records;
            };
        }
        async void OnRecordSelected(Record record)
        {
            if (record == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(RecordDetailPage)}?{nameof(RecordDetailPageViewModel.RecordId)}={record.Guid}");
        }


        // ******** 公开变量

        public Command<Record> RecordTapped { get; }

        /// <summary>
        /// 加载状态。
        /// </summary>
        private string _status;

        /// <summary>
        /// 查询语句。
        /// </summary>
        public Expression<Func<Record, bool>> Where
        {
            get => _where;
            set
            {
                Set(nameof(Where), ref _where, value);
                _newQuery = true;
            }
        }


        /// <summary>
        /// 查询语句。
        /// </summary>
        private Expression<Func<Record, bool>> _where;

        public InfiniteScrollCollection<Record> RecordCollection { get; }


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
            await _recordStorage.InitializeAsync();

            Where = Expression.Lambda<Func<Record, bool>>(
                Expression.Constant(true),
                Expression.Parameter(typeof(Record), "p"));

            if (!_newQuery) return;
            _newQuery = false;

            RecordCollection.Clear();
            _canLoadMore = true;
            await RecordCollection.LoadMoreAsync();
        }

        /// <summary>
        /// 一页显示的记录数量。
        /// </summary>
        public const int PageSize = 1000;

        /// <summary>
        /// 正在载入。
        /// </summary>
        public const string Loading = "正在载入";

        /// <summary>
        /// 没有满足条件的结果。
        /// </summary>
        public const string NO_RESULT = "没有满足条件的结果";

        /// <summary>
        /// 没有更多结果。
        /// </summary>
        public const string NO_MORE_RESULT = "没有更多结果";

        public Command AddRecordCommand { get; }

        private async void OnAddRecord(object obj)
        {
            await Shell.Current.GoToAsync(nameof(Views.NewRecordPage));
        }

        // ******** 私有变量

        /// <summary>
        /// 是否为新查询。
        /// </summary>
        private bool _newQuery;

        private bool _canLoadMore = true;
        **/
    }
}
