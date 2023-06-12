using Travlr.Models;
using Travlr.Repositories;
using Microsoft.AspNetCore.Mvc;



namespace Travlr.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StaysController : ControllerBase
    {
        private readonly IStayRepository _staysRepo;

        public StaysController(IStayRepository stayRepo)
        {
            _staysRepo = stayRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_staysRepo.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var activity = _staysRepo.GetById(id);
            if (activity == null)
            {
                return NotFound();
            }
            return Ok(activity);
        }

        [HttpPost]
        public IActionResult Post(Stay stay)
        {
            _staysRepo.Add(stay);
            return CreatedAtAction("Get", new { id = stay.Id }, stay);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Stay stay)
        {
            if (id != stay.Id)
            {
                return BadRequest();
            }

            _staysRepo.Update(stay);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _staysRepo.Delete(id);
            return NoContent();
        }
    }
}
