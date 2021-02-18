using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insulin_backend.Database.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }
        public int LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
        [Required]
        private string FirstName { get; set; }
        [Required]
        private string LastName { get; set; }
        [Required]
        private string Sex { get; set; }
        [Required]
        private string Weight { get; set; }
        [Required]
        private string DateOfBirth { get; set; }
        [Required]
        private string Height { get; set; }
        [Required]
        private string HashedPassword { get; set; }
        public ICollection<Tutorial> Tutorials { get; set; }
    }
}