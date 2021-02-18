using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insulin_backend.Database.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
        public int LanguageId { get; set; }
        [Required]
        private string FirstName { get; set; }
        [Required]
        private string LastName { get; set; }
        [Required]
        private string Sex { get; set; }
        [Required]
        private string DateOfBirth { get; set; }
        [Required]
        private string Education { get; set; }
        [Required]
        private string Email { get; set; }
        [Required]
        private string PhoneNumber { get; set; }
        [Required]
        private string HashedPassword { get; set; }
    }
}