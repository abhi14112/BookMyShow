namespace BookMyShow.DTO
{
    public class SeatIdDto
    {
        public List<int> Ids { get; set; }
        public int CinemaId { get; set; }  
        public int ShowId { get; set; } 
        public int UserId { get; set; }
    }
}