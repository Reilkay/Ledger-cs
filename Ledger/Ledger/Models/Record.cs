using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Models
{
    [SQLite.Table("works")]
    public class Record {
        /// <summary>
        /// 主键
        /// </summary>
        [SQLite.Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [SQLite.Column("content")]
        public string Content { get; set; }

        /// <summary>
        /// 类型
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
        /// 收支
        /// </summary>
        [SQLite.Column("budget")]
        public string Budget { get; set; }

        private string _color;

        /// <summary>
        /// 颜色
        /// </summary>
        [SQLite.Ignore]
        public string Color =>
            _color ?? (_color = Budget == "收入" ? "green" : "red");

        private string _money;

        public string Money =>
            _money ?? (_money = Budget == "收入" ? "+" + Amount : "-" + Amount);

        public const string Budgetexpense = "expense";

        public const string Budgetincome = "income";
    }
}
