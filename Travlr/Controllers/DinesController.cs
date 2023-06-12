using Travlr.Models;
using Travlr.Repositories;
using Microsoft.AspNetCore.Mvc;



namespace Travlr.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DinesController : ControllerBase
    {
        private readonly IDineRepository _dinesRepo;

        public DinesController(IDineRepository dineRepo)
        {
            _dinesRepo = dineRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dinesRepo.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var dine = _dinesRepo.GetById(id);
            if (dine == null)
            {
                return NotFound();
            }
            return Ok(dine);
        }

        [HttpPost]
        public IActionResult Post(Dine dine)
        {
            _dinesRepo.Add(dine);
            return CreatedAtAction("Get", new { id = dine.Id }, dine);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Dine dine)
        {
            if (id != dine.Id)
            {
                return BadRequest();
            }

            _dinesRepo.Update(dine);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _dinesRepo.Delete(id);
            return NoContent();
        }
    }
}
