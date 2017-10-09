using System;
using System.ComponentModel.DataAnnotations;

namespace TORWebAPIDemo.Model
{
    public class CncMember
    {
        public int Id { get; set; }
        
        [Required]
        public int LocationId { get; set; }
        
        public string Names { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        
        [StringLength(1, ErrorMessage = "Gender can only be 'F' for female, 'M' for male or 'X' for unkown")]
        public string Gender { get; set; }
        
        public string EducationLevel { get; set; }
        
        public string Position { get; set; }
        
        public string Contact { get; set; }
        
        public bool Trained { get; set; }
        
        //Navigation properties
        public Location Location { get; set; }
    }
}