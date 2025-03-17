using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
namespace BookMyShow.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set;}
        public int UserId { get; set; }
        public int CinemaId { get; set; }
        public int ShowId { get; set; }
        [Precision(18,2)]
        public decimal Amount { get; set; }
        public string? Status { get; set; }
        public DateTime BookingTime { get; set; }
        public string? PayPalOrderId { get; set; }
        public User? User { get; set; }
        public Cinema? Cinema { get; set; }
        public Show? Show { get; set; }
        public IEnumerable<BookingSeat>? BookingSeats{ get; set; }
    }
}