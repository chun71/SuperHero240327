
using Dapper;
using Microsoft.Data.SqlClient;
using Models.CharacterLog;
using System.Data;

namespace Repositories.Repositories
{
    public class CharacterLogRepository : Repository
    {
        public async Task<List<CharacterLog>> QueryAsync()
        {
            string querySql = @"
                               SELECT   [CharacterID], [Name], [FirstName], [LastName], [Place], [Action], [CreateTime]
                               FROM     [CharacterLog]
                                        ";

            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();

            var characterLogs = await dbConnection.QueryAsync<CharacterLog>(querySql);

            return characterLogs.ToList();
        }

        public async Task DeleteAsync(DateTime maxTime) 
        {
            string deleteSql = @"
                                DELETE FROM [CharacterLog]
                                WHERE [CreateTime] <@MaxTime
                                    "
                                            ;

            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();

            await dbConnection.ExecuteAsync(deleteSql, new { MaxTime = maxTime });
        }

    }
}
