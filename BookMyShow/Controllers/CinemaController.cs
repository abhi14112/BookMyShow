using BookMyShow.DTO;
using BookMyShow.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaController : ControllerBase
    {
        private readonly ICinemaRepository _cinemaService;
        public CinemaController(ICinemaRepository cinemaService)
        {
            _cinemaService = cinemaService;
        }
        [HttpGet("GetShowSeats/{id}")]
        public async Task<IActionResult>GetShowSeats(int id)
        {
            var seats = await _cinemaService.GetShowSeats(id);
            return Ok(seats);
        }
        [HttpGet("GetShows/{id}")]
        public async Task<IActionResult> GetShows(int id)
        {
            var shows = await _cinemaService.GetShows(id);
            return Ok(shows);
        }
        [HttpGet("GetCinemas/{id}")]
        public async Task<IActionResult> GetCinemas(int id)
        {
            var cinemas = await _cinemaService.GetCinemas(id);
            return Ok(cinemas);
        }

        [HttpPost("AddCinema/{id}")]
        public async Task<IActionResult>AddCinema( [FromBody]CinemaDto cinema, int id)
        {
           var result = await  _cinemaService.AddCinema(cinema, id);
            return Ok(result);
        }
    }
}