using System.ComponentModel.DataAnnotations;

namespace BookMyShow.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public int OwnerId { get; set; }
        public List<Show>? Shows { get; set; }
    }
}
