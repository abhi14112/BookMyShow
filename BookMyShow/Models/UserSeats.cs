using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BookMyShow.Models
{
    public class UserSeats
    {
        [Key]
        public int Id { get; set; }
        public List<Seat> Seats { get; set; } = new List<Seat>();
        [ForeignKey("Cinema")]
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        [ForeignKey("Show")]
        public int ShowId { get; set; }
        public Show Show { get; set; }
    }
}