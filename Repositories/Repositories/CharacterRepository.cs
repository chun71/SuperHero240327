
using Dapper;
using Microsoft.Data.SqlClient;
using Models.Character;
using Repositories.Repositories.IRepositories;
using System.Data;

namespace Repositories.Repositories
{

    public sealed class CharacterRepository : Repository, ICharacterRepository
    {

        public async Task<List<Character>> QueryAsync()
        {
            string querySql = @"
                               SELECT  [ID], [Name], [FirstName], [LastName], [Place], [CreateTime], [UpdateTime]
                               FROM    [Character] 
                                                    ";

            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();

            var characters = await dbConnection.QueryAsync<Character>(querySql);

            return characters.ToList();
        }


        public async Task<Character> QueryFirstOrDefaultAsync(DateTime createTime)
        {
            string querySql = @"
                                SELECT  [ID], [Name], [FirstName], [LastName], [Place], [CreateTime], [UpdateTime]
                                FROM    [Character] 
                                WHERE  [CreateTime] = @CreateTime
                                        ";

            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();

            var characters = await dbConnection.QueryFirstOrDefaultAsync<Character>(querySql, new { CreateTime = createTime });

            return characters;
        }


        public async Task<Character> QueryFirstOrDefaultAsync(long id)
        {
            string querySql = @"
                                SELECT  [ID], [Name], [FirstName], [LastName], [Place], [CreateTime], [UpdateTime]
                                FROM    [Character] 
                                WHERE  [ID] = @ID
                                        ";

            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();

            var characters = await dbConnection.QueryFirstOrDefaultAsync<Character>(querySql, new { ID = id });

            return characters;
        }


        public async Task CreateAsync(Character character)
        {
            if (character != null)
            {
                string insertSql = @"
                                INSERT INTO 
                                [Character] ([Name], [FirstName], [LastName], [Place], [CreateTime])
                                VALUES       (@Name, @FirstName, @LastName, @Place, @CreateTime)
                                            ";

                using IDbConnection dbConnection = new SqlConnection(connectionString);
                dbConnection.Open();

                await dbConnection.ExecuteAsync(insertSql, character);
            }
        }


        public async Task UpdateAsync(Character character)
        {
            if (character != null)
            {
                string updateSql = @"
                                UPDATE              [Character] 
                                SET
                                [Name]                  = @Name,
                                [FirstName]         = @FirstName,
                                [LastName]          = @LastName,
                                [Place]                     = @Place, 
                                [UpdateTime]    = @UpdateTime
                                WHERE [ID]          = @ID
                                            ";

                using IDbConnection dbConnection = new SqlConnection(connectionString);
                dbConnection.Open();

                await dbConnection.ExecuteAsync(updateSql, character);
            }
        }


        public async Task DeleteAsync(long id)
        {
            string deleteSql = @"
                                    DELETE FROM [Character]
                                    WHERE [ID] = @ID
                                                             "
            ;

            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();

            await dbConnection.ExecuteAsync(deleteSql, new { ID = id });
        }
    }
}
