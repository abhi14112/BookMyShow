using BookMyShow.DTO;
using BookMyShow.Models;

namespace BookMyShow.Repository.Interfaces
{
    public interface IMovieRepository
    {
        Task<MovieDto> AddMovie(MovieDto movie);
        Task<IEnumerable<Movie>> GetAll();
    }
}