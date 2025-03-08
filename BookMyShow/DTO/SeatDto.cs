using BookMyShow.Models;

namespace BookMyShow.DTO
{
    public class SeatDto
    {
        public string? Row { get; set; } // Grouping by Row
        public int ShowId { get; set; }
        public List<ColumnsDto> Columns { get; set; } = new();
    }

    public class ColumnsDto
    {
        public int Id { get; set; }
        public int Column { get; set; }
        public SeatStatus SeatStatus { get; set; }
        public SeatType SeatType { get; set; }
    }

}
