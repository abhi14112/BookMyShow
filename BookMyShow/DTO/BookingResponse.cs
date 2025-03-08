namespace BookMyShow.DTO
{
    public class BookingDto
    {
        public int Id { get; set; }
        public DateTime BookingTime { get; set; }
        public BookingShowDto Show { get; set; }
        public BookingCinemaDto Cinema { get; set; }
        public List<BookingSeatDto> Seats { get; set; }
    }
    public class BookingShowDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
    }
    public class BookingCinemaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class BookingSeatDto
    {
        public int Id { get; set; }
        public string SeatNumber { get; set; }
    }
}
