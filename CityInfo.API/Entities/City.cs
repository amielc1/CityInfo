using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Entities
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]// the key id will be generated at add (not in update)
        public int Id { get; set; }
        [Required(ErrorMessage = "You need to provide a Name")]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }


        public ICollection<PointOfInterest> PointsOfInterest { get; set; }
          = new List<PointOfInterest>();
    }
}
