using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCar.Context;
using MyCar.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MyCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CarrosController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CarrosController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetCarros()
        {
            return Ok(new
            {
                success = true,
                data = await _appDbContext.Carros.ToListAsync()
            });
        }
        [HttpPost]
        public async Task<IActionResult> CreateCarro(Carro carro)
        {
            _appDbContext.Carros.Add(carro);
            await _appDbContext.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                data = carro
            });
        }

        // GET: api/Carros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carro>> GetCarros(int id)
        {

            var carro = await _appDbContext.Carros.FindAsync(id);

            if (carro == null)
            {
                return NotFound();
            }

            return carro;
        }

        //PUT: api/Carros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarro(int id, Carro carro)
        {
            if (id != carro.Id)
            {
                return BadRequest();
            }
            _appDbContext.Entry(carro).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        //Delete: api/Carros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var carro = await _appDbContext.Carros.FindAsync(id);
            if (carro == null)
            {
                return NotFound();
            }
            _appDbContext.Carros.Remove(carro);
            await _appDbContext.SaveChangesAsync();

            return NoContent();

        }

        private bool CarroExists(int id)
        {
            return _appDbContext.Carros.Any(e => e.Id == id);
        }
    }
}
