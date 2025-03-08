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
                        Id = s.Id,
                        Column = s.Columns,
                        SeatStatus = s.SeatStatus,
                        SeatType = s.SeatType
                    }).ToList()
                })
                .ToListAsync();
            return seats;
        }
        public async Task AddShowData(AddShowDto show, int id)
        {
            try
            {
                var showData = new Show
                {
                    CinemaId = id,
                    MovieId = show.MovieId,
                    TicketPrice = show.TicketPrice,
                    StartDate = show.StartDate,
                    EndDate = show.EndDate
                };

                _context.Shows.Add(showData);
                await _context.SaveChangesAsync();

                int rows = (int)Math.Ceiling((double)show.TotalSeats / 15);
                string[] rowData = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };

                List<Seat> seats = new List<Seat>();

                for (int i = 0; i < rows; i++)
                {
                    int columns = (i == rows - 1 && show.TotalSeats % 15 > 0) ? show.TotalSeats % 15 : 15;

                    for (int j = 1; j <= columns; j++)
                    {
                        seats.Add(new Seat
                        {
                            Row = rowData[i],
                            Columns = j,
                            ShowId = showData.Id,
                            SeatStatus = SeatStatus.AVAILABLE,
                            SeatType = SeatType.NORMAL
                        });
                    }
                }
                _context.Seats.AddRange(seats);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding show data: " + ex.Message);
            }
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
        public async Task<ShowDetailsDto> GetShowDetails(int id)
        {
            var result = await _context.Shows.Where(s => s.Id == id)
                .Select(s => new ShowDetailsDto
                {
                    Id = s.Id,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    TicketPrice = s.TicketPrice,
                    TotalSeats = s.Seats.Count
                }).FirstOrDefaultAsync();
            return result;
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
        public async Task AddCinema(AddCinemaDto cinema,int id)
        {
            var newCinema = new Cinema
            {
                Name = cinema.Name,
                Location = cinema.Location,
                OwnerId = id
            };
            _context.Cinemas.Add(newCinema);
            await _context.SaveChangesAsync();
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
         
        public async Task BookSeats(SeatIdDto ids)
        {
            var seats = _context.Seats.Where(s => ids.Ids.Contains(s.Id)).ToList();
            foreach(var seat in seats)
            {
                seat.SeatStatus = (SeatStatus)1;
            }
            await _context.SaveChangesAsync();
        }
    }
}