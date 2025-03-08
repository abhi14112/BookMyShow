using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BookMyShow.Models
{
    public class Show
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
        [ForeignKey("Cinema")]
        public int CinemaId { get; set; }
        public Cinema? Cinema { get; set; }
        public List<Seat>? Seats { get; set; }
        public int TicketPrice { get; set; }
    }
}