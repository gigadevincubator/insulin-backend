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
        public string VideoUrl { get; set; }
   
        public string Audio { get; set; }
      
        public string Text { get; set; }
        
        public string Title { get; set; }
        public int StepNumber { get; set; }
    }
}