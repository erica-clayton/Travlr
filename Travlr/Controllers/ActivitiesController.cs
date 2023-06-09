﻿using Travlr.Models;
using Travlr.Repositories;
using Microsoft.AspNetCore.Mvc;



namespace Travlr.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivityRepository _activityRepo;

        public ActivitiesController(IActivityRepository activityRepo)
        {
            _activityRepo = activityRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_activityRepo.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var activity = _activityRepo.GetById(id);
            if (activity == null)
            {
                return NotFound();
            }
            return Ok(activity);
        }

        [HttpPost]
        public IActionResult Post(Activity activity)
        {
            _activityRepo.Add(activity);
            return CreatedAtAction("Get", new { id = activity.Id }, activity);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Activity activity)
        {
            if (id != activity.Id)
            {
                return BadRequest();
            }

            _activityRepo.Update(activity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _activityRepo.Delete(id);
            return NoContent();
        }
    }
}
