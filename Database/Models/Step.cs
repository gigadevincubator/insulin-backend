using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insulin_backend.Database.Models
{
    public class Step
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TutorialId { get; set; }
        [ForeignKey("TutorialId")]
        public Tutorial Tutorial { get; set; }
        [Required]
        public string VideoUrl { get; set; }
        [Required]
        public string Audio { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int StepNumber { get; set; }
    }
}