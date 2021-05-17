﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ledger.Services
{
    public interface IDataStore<T> {
        //Task InitializeAsync();
        //bool IsInitialized();
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);

        List<T> GetRecords();
    }
}
