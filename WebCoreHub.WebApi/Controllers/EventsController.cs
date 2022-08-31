using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCoreHub.Models;
using WebCoreHub.Dal;

namespace WebCoreHub.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ICommonRepository<Event> _eventRepository;

        public EventsController(ICommonRepository<Event> repository)
        {
            _eventRepository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Event>> Get()
        {
            var events = _eventRepository.GetAll();

            if (events.Count == 0)
            {
                return NotFound();
            }

            return Ok(events);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Event> GetDetails(int id)
        {
            var eventType = _eventRepository.GetDetails(id);

            if (eventType == null)
            {
                return NotFound();
            }

            return Ok(eventType);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create(Event eventType)
        {
            _eventRepository.Insert(eventType);

            var result = _eventRepository.SaveChanges();

            if (result == 0)
            {
                return BadRequest();
            }

            return CreatedAtAction("GetDetails", new { id = eventType.EventId }, eventType);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(Event eventType)
        {
            _eventRepository.Update(eventType);

            var result = _eventRepository.SaveChanges();

            if (result == 0)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Event> Delete(int id)
        {
            var eventType = _eventRepository.GetDetails(id);

            if (eventType == null)
            {
                return NotFound();
            }

            _eventRepository.Delete(eventType);
            _eventRepository.SaveChanges();

            return NoContent();
        }
    }
}
