using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;


//using Microsoft.EntityFrameworkCore;
using SuperHero240327.Models;
using System.Data;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        //private readonly SuperHeroContext _context;

        //public SuperHeroController(SuperHeroContext context)
        //{
        //    _context = context;
        //}

        private static readonly ConfigurationBuilder configBuilder = new ConfigurationBuilder();

        private readonly string connectionString = configBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build()["ConnectionStrings:SqlServer"];

        public SuperHeroController()
        {

        }

        //[HttpGet]
        //public async Task<ActionResult<List<SuperHeroContext>>> GetSuperHeroes()
        //{
        //    return Ok(await _context.Character.ToListAsync());
        //}

        [HttpGet]
        public async Task<ActionResult<List<Character>>> GetSuperHeroes()
        {
            //return Ok(await _context.Character.ToListAsync());

            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();

            string sqlCmd = "SELECT ID, Name, FirstName, LastName, Place FROM Character WHERE 1 = 1";
            var dynParams = new DynamicParameters();

            return Ok(dbConnection.Query<Character>(sqlCmd, dynParams));
        }


        [HttpPost]
        public async Task<ActionResult<List<Character>>> CreateSuperHero(Character hero)
        {
            //_context.Character.Add(hero);
            //await _context.SaveChangesAsync();

            //return Ok(await _context.Character.ToListAsync());

            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();

            string sqlCmd = "";
            string calTitle = "";
            string valMsg = "";
            var dynParams = new DynamicParameters();

            if (hero != null)
            {
                if (hero.FirstName.IsNullOrEmpty() == false)
                {
                    calTitle = $"{calTitle}FirstName, ";
                    valMsg = $"{valMsg}@FirstName, ";
                    dynParams.Add("@FirstName", hero.FirstName, DbType.String, ParameterDirection.Input, hero.FirstName.Length);
                }

                if (hero.LastName.IsNullOrEmpty() == false)
                {
                    calTitle = $"{calTitle}LastName, ";
                    valMsg = $"{valMsg}@LastName, ";
                    dynParams.Add("@LastName", hero.LastName, DbType.String, ParameterDirection.Input, hero.LastName.Length);
                }

                if (hero.Place.IsNullOrEmpty() == false)
                {
                    calTitle = $"{calTitle}Place, ";
                    valMsg = $"{valMsg}@Place, ";
                    dynParams.Add("@Place", hero.Place, DbType.String, ParameterDirection.Input, hero.Place.Length);
                }

                calTitle = $"{calTitle}Name)";
                valMsg = $"{valMsg}@Name)";
                dynParams.Add("@Name", hero.Name, DbType.String, ParameterDirection.Input, hero.Name.Length);

                sqlCmd = $"INSERT INTO Character ({calTitle} VALUES ({valMsg}";
                dbConnection.Query<Character>(sqlCmd, dynParams);
            }

            return Ok(GetSuperHeroes());
        }

        [HttpPut]
        public async Task<ActionResult<List<Character>>> UpdateSuperHero(Character hero)
        {
            //var dbHero = await _context.Character.FindAsync(hero.ID);
            //if (dbHero == null)
            //    return BadRequest("Hero not found.");

            //dbHero.Name = hero.Name;
            //dbHero.FirstName = hero.FirstName;
            //dbHero.LastName = hero.LastName;
            //dbHero.Place = hero.Place;

            //await _context.SaveChangesAsync();

            //return Ok(await _context.Character.ToListAsync());

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Character>>> DeleteSuperHero(long id)
        {
            //var dbHero = await _context.Character.FindAsync(id);
            //if (dbHero == null)
            //    return BadRequest("Hero not found.");

            //_context.Character.Remove(dbHero);
            //await _context.SaveChangesAsync();

            //return Ok(await _context.Character.ToListAsync());

            return Ok();
        }
    }
}
