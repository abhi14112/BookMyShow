using BookMyShow.DTO;
namespace BookMyShow.Repository.Interfaces
{
    public interface ICinemaRepository
    {
        Task<IEnumerable<CinemaDto>> GetCinemas(int id);
        Task<CinemaDto>AddCinema(CinemaDto cinema, int id);
        Task<IEnumerable<CinemaWithShowsDto>> GetShows(int id);
        Task<List<SeatDto>> GetShowSeats(int showId);
    }
}