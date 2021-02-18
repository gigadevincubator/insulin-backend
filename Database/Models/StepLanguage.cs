using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insulin_backend.Database.Models
{
    public class StepLanguage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int StepId { get; set; }
        [ForeignKey("StepId")]
        public Step Step { get; set; }
        [Required]
        public int TutorialLanguageId { get; set; }
        [ForeignKey("TutorialLanguageId")]
        public TutorialLanguage TutorialLanguage { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Audio { get; set; }
    }
}