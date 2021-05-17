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
    public class RecordDataStore : IDataStore<Record> {
        public  List<Record> Records;

        private SQLiteConnection _connection;

        private SQLiteConnection Connection =>
            _connection ??= new SQLiteConnection(RecordDbPath);


        /// <summary>
        /// 数据库名。
        /// </summary>
        private const string DbName = "recorddb.sqlite3";

        /// <summary>
        /// 诗词数据库路径。
        /// </summary>
        public static readonly string RecordDbPath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder
                    .LocalApplicationData), DbName);


        public RecordDataStore() {
            Records = Connection.Table<Record>().ToList();
        }

        public async Task<bool> AddItemAsync(Record item) {
            Records.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Record item) {
            var oldItem = Records.Where((Record arg) => arg.Id == item.Id).FirstOrDefault();
            Records.Remove(oldItem);
            Records.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id) {
            var oldItem = Records.Where((Record arg) => arg.Id == id).FirstOrDefault();
            Records.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Record> GetItemAsync(string id) {
            return await Task.FromResult(Records.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Record>> GetItemsAsync(bool forceRefresh = false) {

            return await Task.FromResult(Records);
        }

        public List<Record> GetRecords() {
            return Records;
        }

        public int GetMaxId()
        {
            return Records.Max(t => int.Parse(t.Id));
        }
    }
}

