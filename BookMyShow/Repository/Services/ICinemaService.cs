using BookMyShow.Data;
using BookMyShow.DTO;
using BookMyShow.Models;
using BookMyShow.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookMyShow.Repository.Services
{
    public class CinemaRepository:ICinemaRepository
    {
        private readonly AppDbContext _context;
        public CinemaRepository(AppDbContext context)
        {
            _context = context;
        }
           public async Task<List<SeatDto>> GetShowSeats(int showId)
           {
            var seats = await _context.Seats
                .Where(s => s.ShowId == showId)  
                .OrderBy(s => s.Row)
                .ThenBy(s => s.Columns)
                .GroupBy(s => s.Row)
                .Select(g => new SeatDto
                {
                    Row = g.Key,
                    ShowId = showId,
                    Columns = g.Select(s => new ColumnsDto
                    {
                        Column = s.Columns,
                        SeatStatus = s.SeatStatus,
                        SeatType = s.SeatType
                    }).ToList()
                })
                .ToListAsync();

            return seats;
        }
        public async Task<IEnumerable<CinemaWithShowsDto>> GetShows(int movieId)
        {
            var cinemas = await _context.Cinemas
                .Include(c => c.Shows)
                .Where(c => c.Shows.Any(s => s.Movie.Id == movieId)) 
                .Select(c => new CinemaWithShowsDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Location = c.Location,
                    Shows = c.Shows
                        .Where(s => s.Movie.Id == movieId) 
                        .Select(s => new ShowDto
                        {
                            Id = s.Id,
                            StartDate = s.StartDate,
                            EndDate = s.EndDate,
                            TicketPrice = s.TicketPrice
                        }).ToList()
                })
                .ToListAsync();

            return cinemas;
        }
        public async Task<IEnumerable<CinemaDto>> GetCinemas(int id)
        {
            return await _context.Cinemas
                .Where(c => c.OwnerId == id)
                .Select(c => new CinemaDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Location = c.Location
                }).ToListAsync();
        }
        public async Task<CinemaDto>AddCinema(CinemaDto cinema,int id)
        {
            var newCinema = new Cinema
            {
                Name = cinema.Name,
                Location = cinema.Location,
                OwnerId = id
            };
            _context.Cinemas.Add(newCinema);
            await _context.SaveChangesAsync();
            return cinema;
        }
        public async Task<List<AdminShowDto>>GetAdminShows(int id)
        {
           var shows =  await _context.Shows.Where(s => s.Cinema.Id == id)
                .Select(s => new AdminShowDto
                {
                    Id = s.Id,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    MovieTitle = s.Movie.Title
                }).ToListAsync();
            return shows;
        }
    }
}