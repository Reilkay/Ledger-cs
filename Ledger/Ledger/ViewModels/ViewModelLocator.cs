using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Ioc;
using Ledger.Models;
using Ledger.Services;

namespace Ledger.ViewModels
{
    public class ViewModelLocator {
        /// <summary>
        /// 搜索结果页ViewModel。
        /// </summary>
        public DetailPageViewModel DetailViewModel =>
            SimpleIoc.Default.GetInstance<DetailPageViewModel>();

        public NewRecordViewModel NewRecordViewModel =>
            SimpleIoc.Default.GetInstance<NewRecordViewModel>();

        public OverviewViewModel OverviewViewModel =>
            SimpleIoc.Default.GetInstance<OverviewViewModel>();

        public RecordDetailPageViewModel RecordDetailPageViewModel =>
            SimpleIoc.Default.GetInstance<RecordDetailPageViewModel>();

        public ViewModelLocator() {
            SimpleIoc.Default.Register<IDataStore<Record>, RecordDataStore>();
            SimpleIoc.Default.Register<DetailPageViewModel>();
            SimpleIoc.Default.Register<NewRecordViewModel>();
            SimpleIoc.Default.Register<OverviewViewModel>();
            SimpleIoc.Default.Register<RecordDetailPageViewModel>();
        }
    }
}
