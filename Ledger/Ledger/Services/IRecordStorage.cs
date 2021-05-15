using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ledger.Models;

namespace Ledger.Services {
    /// <summary>
    /// 账单记录存储接口
    /// </summary>
    public interface IRecordStorage {
        /// <summary>
        /// 初始化数据库存储。
        /// </summary>
        Task InitializeAsync();

        /// <summary>
        /// 数据库是否已经初始化。
        /// </summary>
        bool IsInitialized();

        /// <summary>
        /// 获取一个记录。
        /// </summary>
        /// <param name="id">记录id。</param>
        /// <returns></returns>
        Task<Record> GetRecordAsync(int id);

        /// <summary>
        /// 获得一组记录。
        /// </summary>
        /// <param name="where">Where条件</param>
        /// <param name="skip">跳过结果的数量</param>
        /// <param name="take">返回结果的数量</param>
        Task<IList<Record>> GetRecordsAsync(
            Expression<Func<Record, bool>> where, int skip, int take);

    }

    /// <summary>
    /// 记录存储常量。
    /// </summary>
    public static class RecordStorageConstants
    {
        /// <summary>
        /// 数据库版本号。
        /// </summary>
        public const int Version = 1;

        /// <summary>
        /// 版本号键。
        /// </summary>
        public const string VersionKey =
            nameof(RecordStorageConstants) + "." + nameof(Version);
    }
}
