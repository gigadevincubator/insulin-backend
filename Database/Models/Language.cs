using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace insulin_backend.Database.Models
{
    public class Language
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}