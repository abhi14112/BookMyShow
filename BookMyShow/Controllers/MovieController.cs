using BookMyShow.DTO;
using BookMyShow.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace BookMyShow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieService;
        public MovieController(IMovieRepository movieService)
        {
            _movieService = movieService;
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var movies = await _movieService.GetAll();
            return Ok(movies);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddMovie([FromBody] MovieDto movie)
        {
            var newMovie = await _movieService.AddMovie(movie);
            return Ok(newMovie);
        }
    }
}
