using System;
using System.ComponentModel.DataAnnotations;

namespace TORWebAPIDemo.Model
{
    public class Level
    {
        public int Id { get; set; }
        
        [Required, Range(1, int.MaxValue, ErrorMessage = "The location level's weight can't be lower than 1.")]
        public int Weight { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}