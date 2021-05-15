using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Ledger.Models;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace Ledger.Services {
    /// <summary>
    /// 记录存储。
    /// </summary>
    public class RecordStorage : IRecordStorage {
        /// <summary>
        /// 偏好存储。
        /// </summary>
        private IPreferenceStorage _preferenceStorage;

        /// <summary>
        /// 诗词存储。
        /// </summary>
        /// <param name="preferenceStorage">偏好存储。</param>
        public RecordStorage(IPreferenceStorage preferenceStorage)
        {
            _preferenceStorage = preferenceStorage;
        }

        // ******** 私有变量

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

        /// <summary>
        /// 数据库连接影子变量。
        /// </summary>
        private SQLiteAsyncConnection _connection;

        /// <summary>
        /// 数据库连接。
        /// </summary>
        private SQLiteAsyncConnection Connection =>
            _connection ??
            (_connection = new SQLiteAsyncConnection(RecordDbPath));

        // ******** 继承方法

        /// <summary>
        /// 获取一个诗词。
        /// </summary>
        /// <param name="id">诗词ID。</param>
        public async Task<Record> GetRecordAsync(int id) =>
            await Connection.Table<Record>().FirstOrDefaultAsync(p =>
                p.Id == id);

        /// <summary>
        /// 获得一组诗词。
        /// </summary>
        /// <param name="where">Where。</param>
        /// <param name="skip">Skip。</param>
        /// <param name="take">Take。</param>
        public async Task<IList<Record>> GetRecordsAsync(
            Expression<Func<Record, bool>> @where, int skip, int take) =>
            await Connection.Table<Record>().Where(@where).Skip(skip).Take(take)
                .ToListAsync();

        private FileStream dbFileStream;

        private Stream dbAssetStream;

        /// <summary>
        /// 初始化数据库
        /// </summary>
        public async Task InitializeAsync() {
            dbFileStream ??= new FileStream(RecordDbPath, FileMode
                .Create);
            dbAssetStream ??= Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("recorddb.sqlite3");
            await dbAssetStream.CopyToAsync(dbFileStream);
            
            _preferenceStorage.Set(RecordStorageConstants.VersionKey,
                 RecordStorageConstants.Version);
        }

        /// <summary>
        /// 数据库是否已经初始化。
        /// </summary>
        public bool IsInitialized() =>
            _preferenceStorage.Get(RecordStorageConstants.VersionKey, -1) ==
            RecordStorageConstants.Version;

        public async Task CloseAsync() => await _connection.CloseAsync();
    }
}