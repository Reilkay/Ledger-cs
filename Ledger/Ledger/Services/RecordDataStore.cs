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
        public  List<Record> records;

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
            records = Connection.Table<Record>().ToList();
        }

        //public async Task InitializeAsync() {
           // records = await Connection.Table<Record>().ToListAsync();
        //}


        /**
        public bool IsInitialized() {
            throw new NotImplementedException();
        }
        **/

        public async Task<bool> AddItemAsync(Record item) {
            records.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Record item) {
            var oldItem = records.Where((Record arg) => arg.Id == item.Id).FirstOrDefault();
            records.Remove(oldItem);
            records.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id) {
            var oldItem = records.Where((Record arg) => arg.Id == id).FirstOrDefault();
            records.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Record> GetItemAsync(string id) {
            List<Record> a = records;
            return await Task.FromResult(records.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Record>> GetItemsAsync(bool forceRefresh = false) {

            return await Task.FromResult(records);
        }

        public List<Record> GetRecords() {
            return records;
        }
    }
}
