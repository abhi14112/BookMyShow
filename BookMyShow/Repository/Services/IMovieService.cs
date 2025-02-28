using BookMyShow.Data;
using BookMyShow.DTO;
using BookMyShow.Models;
using BookMyShow.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookMyShow.Repository.Services
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;
        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<MovieDto> AddMovie(MovieDto movie)
        {
            var newMovie = new Movie
            {
                Title = movie.Title,
                Description = movie.Description,
                CoverImage = movie.CoverImage
            };
            _context.Movies.Add(newMovie);
            await _context.SaveChangesAsync();
            return movie;
        }
        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _context.Movies.ToListAsync();
        }
    }
}
