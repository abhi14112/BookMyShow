namespace BookMyShow.DTO
{
    public class AdminShowDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? MovieTitle { get; set; }
    }
}