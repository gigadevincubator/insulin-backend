using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insulin_backend.Database.Models
{
    public class TutorialLanguage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int LanguageId { get; set; }
        [Required, ForeignKey("LanguageId")]
        public Language Language { get; set; }
        [Required]
        public int TutorialId { get; set; }
        [Required, ForeignKey("TutorialId")]
        public Tutorial Tutorial { get; set; }
        [Required]
        public string Title { get; set; }
        
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}