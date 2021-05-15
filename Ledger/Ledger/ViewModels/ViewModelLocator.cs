using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Ioc;
using Ledger.Services;

namespace Ledger.ViewModels
{
    public class ViewModelLocator {
        /// <summary>
        /// 搜索结果页ViewModel。
        /// </summary>
        public DetailPageViewModel DetailViewModel =>
            SimpleIoc.Default.GetInstance<DetailPageViewModel>();

        public ViewModelLocator() {
            SimpleIoc.Default.Register<IPreferenceStorage, PreferenceStorage>();
            SimpleIoc.Default.Register<IRecordStorage, RecordStorage>();
            SimpleIoc.Default.Register<DetailPageViewModel>();
        }
    }
}
