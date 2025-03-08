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
        [HttpGet("Bookings/{id}")]
        public async Task<IActionResult>GetBookings(int id)
        {
            var result = await _cinemaService.GetBooking(id);
            return Ok(result);
        }
        [HttpGet("Bookings")]
        public async Task<IActionResult>GetAllBookings()
        {
            var result = await _cinemaService.GetAllBooking();
            return Ok(result);
        }
        [HttpPost("BookSeats")]
        public async Task<IActionResult> BookSeats([FromBody]SeatIdDto ids)
        {
            await _cinemaService.BookSeats(ids);
            return Ok("Seats booked");
        }
        [HttpGet("GetShowDetails/{id}")]
        public async Task<IActionResult>GetShowDetails(int id)
        {
            var result = await _cinemaService.GetShowDetails(id);
            return Ok(result);
        }
        [HttpGet("GetShowSeats/{id}")]
        public async Task<IActionResult>GetShowSeats(int id)
        {
            var seats = await _cinemaService.GetShowSeats(id);
            return Ok(seats);
        }
        [HttpGet("GetAdminShows/{id}")]
        public async Task<IActionResult>GetAdminShows(int id)
        {
            var result = await _cinemaService.GetAdminShows(id);
            return Ok(result);
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
        [HttpPost("AddShow/{id}")]
        public async Task<IActionResult> AddShow([FromBody] AddShowDto showData, int id)
        {
            try
            {
                if (showData == null)
                    return BadRequest("Invalid show data.");

                await _cinemaService.AddShowData(showData, id);
                return Ok(new { message = "Show data added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal Server Error", details = ex.Message });
            }
        }

        [HttpPost("AddCinema/{id}")]
        public async Task<IActionResult>AddCinema( [FromBody]AddCinemaDto cinema, int id)
        {
            await  _cinemaService.AddCinema(cinema, id);
            return Ok("Cinema Added");
        }
    }
}