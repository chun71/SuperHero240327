
using Dapper;
using Microsoft.Data.SqlClient;
using Models.CharacterLog;
using System.Data;
using System.Globalization;
using Services;
using Repositories;

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
                return new BackupArrange(new CommonService(new Repository()));
            }
        }

        private readonly CommonService commonService;

        private BackupArrange(
            CommonService commonService
            )
        {
            this.commonService = commonService;
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
            DateTime maxTime = DateTime.ParseExact($"{year}-{month.ToString().PadLeft(2, '0')}-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);

            if (month == 1)
            {
                year = year - 1;
            }

            tableName = $"{tableName}_{year}";

            // 5行 待刪除 {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDir, "sqlconnectionString.txt");
            string connectionString = File.ReadAllText(filePath);

            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();
            // }

            await this.commonService.CreateTable(tableName);

            string querySql = @"
                               SELECT   [CharacterID], [Name], [FirstName], [LastName], [Place], [Action], [CreateTime]
                               FROM     [CharacterLog]
                                        ";

            var characterLogs = await dbConnection.QueryAsync<CharacterLog>(querySql);

            string deleteSql = @$"
                                DELETE FROM [{tableName}]
                                    ";

            await dbConnection.ExecuteAsync(deleteSql);

            string insertSql = @$"
                                INSERT INTO 
                                [{tableName}] ([CharacterID], [Name], [FirstName], [LastName], [Place], [Action], [CreateTime]) 
                                VALUES            (@CharacterID, @Name, @FirstName, @LastName, @Place, @Action, @CreateTime)
                                            ";

            await dbConnection.ExecuteAsync(insertSql, characterLogs);

            if (month == 1)
            {
                deleteSql = @"
                                DELETE FROM [CharacterLog]
                                WHERE [CreateTime] <@MaxTime
                                    ";

                await dbConnection.ExecuteAsync(deleteSql, new { MaxTime = maxTime });
            }
        }
    }
}
