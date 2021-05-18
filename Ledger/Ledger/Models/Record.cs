using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Ledger.Models
{
    [SQLite.Table("works")]
    public class Record {
        /// <summary>
        /// 主键
        /// </summary>
        [SQLite.Column("id")]
        [PrimaryKey]
        public string Id { get; set; }

        /// <summary>
        /// 账单描述内容
        /// </summary>
        [SQLite.Column("content")]
        public string Content { get; set; }

        /// <summary>
        /// 记账类型
        /// </summary>
        [SQLite.Column("type")]
        public string Type { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        [SQLite.Column("amount")]
        public float Amount { get; set; }

        /// <summary>
        /// 年
        /// </summary>
        [SQLite.Column("year")]
        public int Year { get; set; }

        /// <summary>
        /// 月
        /// </summary>
        [SQLite.Column("month")]
        public int Month { get; set; }

        /// <summary>
        /// 日
        /// </summary>
        [SQLite.Column("day")]
        public int Day { get; set; }

        /// <summary>
        /// 收支类型
        /// </summary>
        [SQLite.Column("budget")]
        public string Budget { get; set; }

        private string _color;
        private string _money;
        private string _describe;

        /// <summary>
        /// 颜色
        /// </summary>
        [SQLite.Ignore]
        public string Color => _color ??= Budget == "收入" ? "green" : "red";

        /// <summary>
        /// 金额显示
        /// </summary>
        [SQLite.Ignore]
        public string Money => _money ??= Budget == "收入" ? "+" + Amount : "-" + Amount;

        /// <summary>
        /// 描述显示
        /// </summary>
        [SQLite.Ignore]
        public string Describe => _describe ??= $"{Year:0000}/{Month:00}/{Day:00} {Content}";

    }
}
