
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
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
            DateTime maxTime = DateTime.ParseExact($"{year}-{month.ToString().PadLeft(2, '0')}-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);

            if (month == 1)
            {
                year = year - 1;
                tableName = $"{tableName}_{year}";
            }
            else
            {
                tableName = $"{tableName}_{year}";
            }

            string createTableSql = @$"
                        USE SuperHero
                        IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = '{tableName}')
                        BEGIN
                            CREATE TABLE [{tableName}] (
                                    [ID]                        BIGINT PRIMARY KEY IDENTITY,
                                    [CharacterID]       BIGINT NOT NULL,
		                            [Name]                  NVARCHAR(50) NOT NULL,
		                            [FirstName]         NVARCHAR(50) NOT NULL,
		                            [LastName]          NVARCHAR(50) NOT NULL, 
		                            [Place]                     NVARCHAR(50),
		                            [Action]                NVARCHAR(5) NOT NULL,
		                            [CreateTime]        DATETIME NOT NULL
                            );
                        END
                            ";

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDir, "sqlconnectionString.txt");
            string connectionString = File.ReadAllText(filePath);

            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();

            await dbConnection.ExecuteAsync(createTableSql);




        }
    }
}
