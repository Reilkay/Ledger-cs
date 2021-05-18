using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ledger.Services
{
    /// <summary>
    /// 记录存取接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataStore<T> {
        /// <summary>
        /// 添加一个记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> AddItemAsync(T item);
        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> UpdateItemAsync(T item);
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteItemAsync(string id);
        /// <summary>
        /// 获取一个记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetItemAsync(string id);
        /// <summary>
        /// 获取遍历集合
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        /// <summary>
        /// 从内存获取全部记录
        /// </summary>
        /// <returns></returns>
        List<T> GetRecords();
        /// <summary>
        /// 获取最大ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxId();
    }
}
