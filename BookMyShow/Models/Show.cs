using System.ComponentModel.DataAnnotations;
namespace BookMyShow.Models
{
    public class Show
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Movie? Movie { get; set; }
        public Cinema? Cinema { get; set; }
        public List<Seat>? Seats { get; set; }
        public int TicketPrice { get; set; }
    }
}
