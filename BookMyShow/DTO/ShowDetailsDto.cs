namespace BookMyShow.DTO
{
    public class ShowDetailsDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalSeats { get; set; }
        public int TicketPrice { get; set; }
    }
}