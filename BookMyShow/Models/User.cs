using System.ComponentModel.DataAnnotations;
namespace BookMyShow.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? ProfilePic { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? Password { get; set; }
        public PersonalDetails? PersonalDetails { get; set; }
        public Address? Address { get; set; }
        public string Role { get; set; } = "User";
    }
}