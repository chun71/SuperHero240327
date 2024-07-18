
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SuperHero240327.Enum;
using SuperHero240327.Models;
using System.Data;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static readonly ConfigurationBuilder configBuilder = new ConfigurationBuilder();

        private readonly string connectionString = configBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build()["ConnectionStrings:SqlServer"];

        public SuperHeroController()
        {

        }

        [HttpGet]
        public async Task<ActionResult<List<Character>>> GetSuperHeroes()
        {
            string querySql = @"
                                SELECT  [ID], [Name], [FirstName], [LastName], [Place] 
                                FROM    [Character] 
                                                    ";

            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();

            return Ok(await dbConnection.QueryAsync<Character>(querySql));
        }

        [HttpPost]
        public async Task<ActionResult<List<Character>>> CreateSuperHero(Character hero)
        {
            if (hero != null)
            {
                string insertSql = @"
                                INSERT INTO 
                                [Character] ([Name], [FirstName], [LastName], [Place], [CreateTime])
                                VALUES       (@Name, @FirstName, @LastName, @Place, @CreateTime)
                                            ";

                var parameters = new Character()
                {
                    Name = hero.Name,
                    FirstName = hero.FirstName,
                    LastName = hero.LastName,
                    Place = hero.Place,
                    CreateTime = DateTime.Now
                };

                using IDbConnection dbConnection = new SqlConnection(connectionString);
                dbConnection.Open();

                await dbConnection.ExecuteAsync(insertSql, parameters);

                string querySql = @"
                                SELECT  [ID], [Name], [FirstName], [LastName], [Place], [CreateTime], [UpdateTime]
                                FROM    [Character] 
                                WHERE  [CreateTime] = @CreateTime
                                                    ";

                var dbData = await dbConnection.QueryFirstOrDefaultAsync<Character>(querySql, new { CreateTime = parameters.CreateTime });

                insertSql = @"
                                INSERT INTO 
                                [CharacterLog] ([CharacterID], [Name], [FirstName], [LastName], [Place], [CreateTime], [Action], [LogTime]) 
                                VALUES       (@CharacterID, @Name, @FirstName, @LastName, @Place, @CreateTime, @Action, @LogTime)
                                            ";

                var backupData = new CharacterLog()
                {
                    CharacterID = dbData.ID,
                    Name = dbData.Name,
                    FirstName = dbData.FirstName,
                    LastName = dbData.LastName,
                    Place = dbData.Place,
                    CreateTime = dbData.CreateTime,
                    Action = ActionType.Create,
                    LogTime = DateTime.Now
                };

                await dbConnection.ExecuteAsync(insertSql, backupData);
            }

            return Ok(GetSuperHeroes());
        }

        [HttpPut]
        public async Task<ActionResult<List<Character>>> UpdateSuperHero(Character hero)
        {
            string querySql = @"
                               SELECT  [ID], [Name], [FirstName], [LastName], [Place], [CreateTime], [UpdateTime]
                               FROM    [Character] 
                               WHERE  [ID] = @ID
                                                    ";

            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();

            var parameters = await dbConnection.QueryFirstOrDefaultAsync<Character>(querySql, new { ID = hero.ID });

            if (parameters != null)
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

                if (string.IsNullOrWhiteSpace(hero.Name) == false)
                {
                    parameters.Name = hero.Name;
                }

                if (string.IsNullOrWhiteSpace(hero.FirstName) == false)
                {
                    parameters.FirstName = hero.FirstName;
                }

                if (string.IsNullOrWhiteSpace(hero.LastName) == false)
                {
                    parameters.LastName = hero.LastName;
                }

                if (string.IsNullOrWhiteSpace(hero.Place) == false)
                {
                    parameters.Place = hero.Place;
                }

                parameters.UpdateTime = DateTime.Now;
                await dbConnection.ExecuteAsync(updateSql, parameters);

                string insertSql = @"
                                INSERT INTO 
                                [CharacterLog] ([CharacterID], [Name], [FirstName], [LastName], [Place], [CreateTime], [UpdateTime], [Action], [LogTime])
                                VALUES       (@CharacterID, @Name, @FirstName, @LastName, @Place, @CreateTime, @UpdateTime, @Action, @LogTime)
                                            ";

                var backupData = new CharacterLog()
                {
                    CharacterID = parameters.ID,
                    Name = parameters.Name,
                    FirstName = parameters.FirstName,
                    LastName = parameters.LastName,
                    Place = parameters.Place,
                    CreateTime = parameters.CreateTime,
                    UpdateTime = parameters.UpdateTime,
                    Action = ActionType.Update,
                    LogTime = DateTime.Now
                };

                await dbConnection.ExecuteAsync(insertSql, backupData);
            }

            return Ok(GetSuperHeroes());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Character>>> DeleteSuperHero(long id)
        {
            string querySql = @"
                                SELECT  [ID], [Name], [FirstName], [LastName], [Place], [CreateTime], [UpdateTime]
                                FROM    [Character] 
                                WHERE  [ID] = @ID
                                                    ";

            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();

            var parameters = await dbConnection.QueryFirstOrDefaultAsync<Character>(querySql, new { ID = id });

            string insertSql = @"
                                INSERT INTO 
                                [CharacterLog] ([CharacterID], [Name], [FirstName], [LastName], [Place], [CreateTime], [UpdateTime], [Action], [LogTime])
                                VALUES       (@CharacterID, @Name, @FirstName, @LastName, @Place, @CreateTime, @UpdateTime, @Action, @LogTime)
                                            ";

            var backupData = new CharacterLog()
            {
                CharacterID = parameters.ID,
                Name = parameters.Name,
                FirstName = parameters.FirstName,
                LastName = parameters.LastName,
                Place = parameters.Place,
                CreateTime = parameters.CreateTime,
                UpdateTime = parameters.UpdateTime,
                Action = ActionType.Delete,
                LogTime = DateTime.Now
            };

            await dbConnection.ExecuteAsync(insertSql, backupData);

            string deleteSql = @"
                                    DELETE FROM [Character]
                                    WHERE [ID] = @ID
                                                             ";

            await dbConnection.ExecuteAsync(deleteSql, new { ID = id });
            return Ok(GetSuperHeroes());
        }
    }
}
