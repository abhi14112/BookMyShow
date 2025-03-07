namespace BookMyShow.DTO
{
    public class AddShowDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalSeats { get; set; }
        public int TicketPrice { get; set; }
        public int MovieId { get; set; }
    }
}