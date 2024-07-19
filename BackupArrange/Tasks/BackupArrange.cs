
using System.Globalization;

namespace BackupArrange.Tasks
{
    /// <summary>
    /// 備份資料整理
    /// </summary>
    public sealed class BackupArrange
    {
        /// <summary>
        /// BackupArrange 實作物件
        /// </summary>
        public static BackupArrange Obj
        {
            get
            {
                return new BackupArrange();
            }
        }

        private BackupArrange()
        {

        }

        /// <summary>
        /// 功能執行
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            DateTime today = DateTime.Now;
            int year = today.Year;
            int month = today.Month;
            string tableName = "CharacterLog";
            DateTime maxTime = DateTime.ParseExact($"{year}-{month}-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);

            if (month == 1)
            {
                tableName = $"{tableName}_{year}";
            }

        }
    }
}
