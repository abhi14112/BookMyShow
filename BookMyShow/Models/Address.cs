using System.ComponentModel.DataAnnotations;

namespace BookMyShow.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string? Pincode { get; set; }
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; }
    }
}
