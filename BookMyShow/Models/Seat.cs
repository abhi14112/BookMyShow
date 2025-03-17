using System.ComponentModel.DataAnnotations;

namespace BookMyShow.Models
{
    public class Seat
    {
        [Key]
        public int Id { get; set; }
        public string? Row { get; set; }
        public int Columns { get; set; }
        public SeatType SeatType { get; set; }
        public SeatStatus SeatStatus { get; set; }
        public Show? Show { get; set; }
        public int ShowId { get; set; }
    }
    public enum SeatStatus
    {
        AVAILABLE = 0,
        BOOKED = 1,
        InProgress = 2
    }
    public enum SeatType
    {
        NORMAL,
        PREMIUM
    }
}
