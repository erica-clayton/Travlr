using Travlr.Models;
using Travlr.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace Travlr.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripRepository _tripRepo;

        public TripsController(ITripRepository tripRepo)
        {
            _tripRepo = tripRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_tripRepo.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var trip = _tripRepo.GetById(id);
            if (trip == null)
            {
                return NotFound();
            }
            return Ok(trip);

        }

        [HttpGet("trips/{tripUserId}")]
        public IActionResult GetTripsByUserId(int tripUserId)
        {
            var trips = _tripRepo.GetByUserId(tripUserId);

            if (trips == null)
            {
                return NotFound();
            }

            return Ok(trips);
        }


        [HttpPost]
        public IActionResult Post(Trip trip)
        {
            _tripRepo.Add(trip);
            return CreatedAtAction("Get", new { id = trip.Id }, trip);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Trip trip)
        {
            if (id != trip.Id)
            {
                return BadRequest();
            }

            _tripRepo.Update(trip);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _tripRepo.Delete(id);
            return NoContent();
        }
    }
}
