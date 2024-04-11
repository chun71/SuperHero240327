using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHero240327.Models;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly SuperHeroContext _context;

        public SuperHeroController(SuperHeroContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHeroContext>>> GetSuperHeroes()
        {
            return Ok(await _context.Character.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Character>>> CreateSuperHero(Character hero)
        {
            _context.Character.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.Character.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Character>>> UpdateSuperHero(Character hero)
        {
            var dbHero = await _context.Character.FindAsync(hero.ID);
            if (dbHero == null)
                return BadRequest("Hero not found.");

            dbHero.Name = hero.Name;
            dbHero.FirstName = hero.FirstName;
            dbHero.LastName = hero.LastName;
            dbHero.Place = hero.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.Character.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Character>>> DeleteSuperHero(long id)
        {
            var dbHero = await _context.Character.FindAsync(id);
            if (dbHero == null)
                return BadRequest("Hero not found.");

            _context.Character.Remove(dbHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.Character.ToListAsync());
        }
    }
}
