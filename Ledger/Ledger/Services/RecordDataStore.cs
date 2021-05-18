using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ledger.Models;
using SQLite;
using Xamarin.Essentials;

namespace Ledger.Services
{
    /// <summary>
    /// 记录存取
    /// </summary>
    public class RecordDataStore : IDataStore<Record> {
        /// <summary>
        /// 内存中的记录表
        /// </summary>
        public  List<Record> Records;

        /// <summary>
        /// 数据库连接
        /// </summary>
        private SQLiteConnection _connection;

        /// <summary>
        /// 数据库连接
        /// </summary>
        private SQLiteConnection Connection =>
            _connection ??= new SQLiteConnection(RecordDbPath);

        /// <summary>
        /// 数据库名。
        /// </summary>
        private const string DbName = "recorddb.sqlite3";

        /// <summary>
        /// 数据库路径。
        /// </summary>
        public static readonly string RecordDbPath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder
                    .LocalApplicationData), DbName);

        /// <summary>
        /// 构造函数
        /// </summary>
        public RecordDataStore() {
            RefreshRecords();
        }

        /// <summary>
        /// 重新加载数据库到内存
        /// </summary>
        private void RefreshRecords() 
        {
            Records = Connection.Table<Record>().ToList();
        }

        /// <summary>
        /// 添加一个记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> AddItemAsync(Record item) {
            Records.Add(item);
            Connection.Insert(item);
            return await Task.FromResult(true);
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> UpdateItemAsync(Record item) {
            var oldItem = Records.Where((Record arg) => arg.Id == item.Id).FirstOrDefault();
            Records.Remove(oldItem);
            Records.Add(item);
            return await Task.FromResult(true);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteItemAsync(string id) {
            var oldItem = Records.Where((Record arg) => arg.Id == id).FirstOrDefault();
            Records.Remove(oldItem);
            Connection.Delete(oldItem);
            return await Task.FromResult(true);
        }

        /// <summary>
        /// 获取一个记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Record> GetItemAsync(string id) {
            return await Task.FromResult(Records.FirstOrDefault(s => s.Id == id));
        }

        /// <summary>
        /// 获取遍历集合
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Record>> GetItemsAsync(bool forceRefresh = false) {

            return await Task.FromResult(Records);
        }

        /// <summary>
        /// 从内存获取全部记录
        /// </summary>
        /// <returns></returns>
        public List<Record> GetRecords() {
            return Records;
        }

        /// <summary>
        /// 获取最大ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxId() {
            RefreshRecords();
            return Records.Max(t => int.Parse(t.Id));
        }
    }
}

