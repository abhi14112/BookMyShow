namespace BookMyShow.DTO
{
    public class CinemaWithShowsDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public List<ShowDto>? Shows { get; set; }
    }
}
