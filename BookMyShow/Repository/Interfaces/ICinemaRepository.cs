using BookMyShow.DTO;
namespace BookMyShow.Repository.Interfaces
{
    public interface ICinemaRepository
    {
        Task<IEnumerable<CinemaDto>> GetCinemas(int id);
        Task AddCinema(AddCinemaDto cinema, int id);
        Task<IEnumerable<CinemaWithShowsDto>> GetShows(int id);
        Task<List<SeatDto>> GetShowSeats(int showId);
        Task<List<AdminShowDto>> GetAdminShows(int id);
        Task<ShowDetailsDto> GetShowDetails(int id);
        Task AddShowData(AddShowDto show, int id);
        Task<int> BookSeats(SeatIdDto ids,decimal amount);
        Task<List<BookingDto>> GetBooking(int id);
        Task<List<BookingDto>> GetAllBooking();
    }
}