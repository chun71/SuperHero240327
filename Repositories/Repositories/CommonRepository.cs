
using Dapper;
using Microsoft.Data.SqlClient;
using Models.CharacterLog;
using System.Data;

namespace Repositories.Repositories
{
    public class CommonRepository : Repository
    {
        public async Task CreateAsync(string tableName)
        {
            string createTableSql = @$"
                        USE [SuperHero]
                        IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = '{tableName}')
                        BEGIN
                            CREATE TABLE [{tableName}] (
                                    [CharacterID]       BIGINT NOT NULL,
		                            [Name]                  NVARCHAR(50) NOT NULL,
		                            [FirstName]         NVARCHAR(50) NOT NULL,
		                            [LastName]          NVARCHAR(50) NOT NULL, 
		                            [Place]                     NVARCHAR(50),
		                            [Action]                NVARCHAR(5) NOT NULL,
		                            [CreateTime]        DATETIME PRIMARY KEY
                            );
                        END
                            ";

            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();

            await dbConnection.ExecuteAsync(createTableSql);
        }

        public async Task DeleteAsync(string tableName)
        {
            string deleteSql = @$"
                                DELETE FROM [{tableName}]
                                             ";

            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();

            await dbConnection.ExecuteAsync(deleteSql);
        }

        public async Task InsertAsync(string tableName, List<CharacterLog> characterLogs) 
        {
            string insertSql = @$"
                                INSERT INTO 
                                [{tableName}] ([CharacterID], [Name], [FirstName], [LastName], [Place], [Action], [CreateTime]) 
                                VALUES            (@CharacterID, @Name, @FirstName, @LastName, @Place, @Action, @CreateTime)
                                            ";

            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();

            await dbConnection.ExecuteAsync(insertSql, characterLogs);
        }
    }
}
