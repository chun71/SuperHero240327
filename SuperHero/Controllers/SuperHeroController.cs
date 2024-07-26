
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Enums;
using Models.Character;
using Models.CharacterLog;
using System.Data;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class SuperHeroController : ControllerBase
    {
        private static readonly ConfigurationBuilder configBuilder = new ConfigurationBuilder();

        private readonly string connectionString = configBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build()["ConnectionStrings:SqlServer"];

        public SuperHeroController()
        {

        }


        [HttpGet]
        public async Task<ActionResult<List<CharacterView>>> GetSuperHeroes()
        {
            string querySql = @"
                                SELECT  [ID], [Name], [FirstName], [LastName], [Place] 
                                FROM    [Character] 
                                                    ";

            using IDbConnection dbConnection = new SqlConnection(this.connectionString);
            dbConnection.Open();
            var characters = await dbConnection.QueryAsync<Character>(querySql);
            var viewModel = new List<CharacterView>();

            foreach (var character in characters)
            {
                var characterView = new CharacterView
                {
                    ID = character.ID,
                    Name = character.Name,
                    FirstName = character.FirstName,
                    LastName = character.LastName,
                    Place = character.Place
                };

                viewModel.Add(characterView);
            }

            return this.Ok(viewModel);
        }


        [HttpPost]
        public async Task<ActionResult<List<CharacterView>>> CreateSuperHero(CharacterView hero)
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

                using IDbConnection dbConnection = new SqlConnection(this.connectionString);
                dbConnection.Open();

                await dbConnection.ExecuteAsync(insertSql, parameters);

                string querySql = @"
                                SELECT  [ID], [Name], [FirstName], [LastName], [Place], [CreateTime], [UpdateTime]
                                FROM    [Character] 
                                WHERE  [CreateTime] = @CreateTime
                                                    ";

                var dbData = await dbConnection.QueryFirstOrDefaultAsync<Character>(querySql, new { CreateTime = parameters.CreateTime });
                await this.SaveLog(dbData, ActionType.Create);
            }

            return await this.GetSuperHeroes();
        }


        [HttpPut]
        public async Task<ActionResult<List<CharacterView>>> UpdateSuperHero(CharacterView hero)
        {
            string querySql = @"
                               SELECT  [ID], [Name], [FirstName], [LastName], [Place], [CreateTime], [UpdateTime]
                               FROM    [Character] 
                               WHERE  [ID] = @ID
                                                    ";

            using IDbConnection dbConnection = new SqlConnection(this.connectionString);
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

                await this.SaveLog(parameters, ActionType.Update);
            }

            return await this.GetSuperHeroes();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<CharacterView>>> DeleteSuperHero(long id)
        {
            string querySql = @"
                                SELECT  [ID], [Name], [FirstName], [LastName], [Place], [CreateTime], [UpdateTime]
                                FROM    [Character] 
                                WHERE  [ID] = @ID
                                                    ";

            using IDbConnection dbConnection = new SqlConnection(this.connectionString);
            dbConnection.Open();

            var parameters = await dbConnection.QueryFirstOrDefaultAsync<Character>(querySql, new { ID = id });

            await this.SaveLog(parameters, ActionType.Delete);

            string deleteSql = @"
                                    DELETE FROM [Character]
                                    WHERE [ID] = @ID
                                                             ";

            await dbConnection.ExecuteAsync(deleteSql, new { ID = id });
            return await this.GetSuperHeroes();
        }


        private async Task SaveLog(Character parameters, string action)
        {
            using IDbConnection dbConnection = new SqlConnection(this.connectionString);
            dbConnection.Open();

            string insertSql = @"
                                INSERT INTO 
                                [CharacterLog] ([CharacterID], [Name], [FirstName], [LastName], [Place], [Action], [CreateTime]) 
                                VALUES       (@CharacterID, @Name, @FirstName, @LastName, @Place, @Action, @CreateTime)
                                            ";

            var backupData = new CharacterLog()
            {
                CharacterID = parameters.ID,
                Name = parameters.Name,
                FirstName = parameters.FirstName,
                LastName = parameters.LastName,
                Place = parameters.Place,
                Action = action
            };

            if (action == ActionType.Create)
            {
                backupData.CreateTime = parameters.CreateTime;
            }
            else if (action == ActionType.Update)
            {
                backupData.CreateTime = parameters.UpdateTime.HasValue ? parameters.UpdateTime.Value : DateTime.Now;
            }
            else if (action == ActionType.Delete)
            {
                backupData.CreateTime = DateTime.Now;
            }

            await dbConnection.ExecuteAsync(insertSql, backupData);
        }
    }
}
