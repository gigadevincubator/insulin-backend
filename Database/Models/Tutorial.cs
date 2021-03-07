using System.ComponentModel.DataAnnotations;

namespace insulin_backend.Database.Models
{
    public class Tutorial
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Thumbnail { get; set; }
        
        // public ICollection<User> Users { get; set; }
    }
}
