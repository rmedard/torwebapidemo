using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json.Serialization;

namespace TORWebAPIDemo.Model
{
    public class Location
    {
        
        public int Id { get; set; }
        
        [Required]
        public int LevelId { get; set; }
        
        [Column("LocationId")]
        public int ParentLocationId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        // Navigation properties
        
        public Location ParentLocation { get; set; }
        
        public Level Level { get; set; }
        
        public ICollection<Location> ChildLocations { get; set; }
    }
}