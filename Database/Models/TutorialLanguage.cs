using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [ForeignKey("UserId"), JsonIgnoreAttribute]
        public User User { get; set; }
    }
}