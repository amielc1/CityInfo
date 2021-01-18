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
        public string Name { get; set; }
        public string Description { get; set; }


        public ICollection<PointOfInterest> PointsOfInterest { get; set; }
          = new List<PointOfInterest>();
    }
}
