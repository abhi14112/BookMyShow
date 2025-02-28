using System.ComponentModel.DataAnnotations;

namespace BookMyShow.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public double Rating { get; set; }
        public string? Description { get; set; }
        public int Votes { get; set; }
        public string? CoverImage { get; set; }
    }
}
