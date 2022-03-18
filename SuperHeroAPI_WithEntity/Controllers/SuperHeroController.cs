using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI_WithEntity.Data;

namespace SuperHeroAPI_WithEntity.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heros = new List<SuperHero>
            {
              //  new SuperHero{Id=1,Name="Spider Man",FirstName="Peter",LastName="Parker",Place="New York City" },
              //  new SuperHero{Id=2,Name="IRON Man",FirstName="Toni",LastName="Stark",Place="USA"}
            };

        private readonly DataContext _context;
        public SuperHeroController(DataContext context)
        {
            _context=context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddSuperHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
           // heros.Add(hero);
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var super = await _context.SuperHeroes.FindAsync(id);

           // var super = heros.Find(h => h.Id == id);
            if (super == null)
                return BadRequest("Hero Not Found");
            
                return Ok(super);
        }

        [HttpPut]
        public async Task<ActionResult<SuperHero>> Put(SuperHero hero)
        {
            var dbhero = await _context.SuperHeroes.FindAsync(hero.Id);
            if (dbhero == null)
                return BadRequest("Hero Not Found");

            dbhero.FirstName = hero.FirstName;
            dbhero.LastName = hero.LastName;
            dbhero.Place = hero.Place;
            dbhero.Name = hero.Name;
          

            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());


        }



        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateById(int id, SuperHero hero)
        {
            var dbhero = await _context.SuperHeroes.FindAsync(hero.Id);
            if (dbhero == null)
                return BadRequest("Hero Not Found");

            dbhero.FirstName = hero.FirstName;
            dbhero.LastName = hero.LastName;
            dbhero.Place = hero.Place;
            dbhero.Name = hero.Name;
          

            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeteleById(int id)
        {
            var dbhero = await _context.SuperHeroes.FindAsync(id);
            if (dbhero == null)
                return BadRequest("Hero Not Found");
            _context.SuperHeroes.Remove(dbhero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
            
        }
    }
}
