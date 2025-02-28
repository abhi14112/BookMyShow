using System.ComponentModel.DataAnnotations;

namespace BookMyShow.Models
{
    public class PersonalDetails
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender Gender { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; }
    }
    public enum Gender
    {
        Male,
        Female
    }
}
