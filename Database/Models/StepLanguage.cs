using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace insulin_backend.Database.Models
{
    public class StepLanguage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int StepId { get; set; }
        [ForeignKey("StepId")]
        [JsonIgnore]
        public Step Step { get; set; }
        [Required]
        public int TutorialLanguageId { get; set; }
        [ForeignKey("TutorialLanguageId")]
        [JsonIgnore]
        public TutorialLanguage TutorialLanguage { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Audio { get; set; }
    }
}