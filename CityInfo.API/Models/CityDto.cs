using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
    public class CityDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You need to provide a Name")]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfPointsOfInterest
        {
            get
            {
                return PointsOfInterest.Count;
            }
        }

        public ICollection<PointOfInterestDto> PointsOfInterest { get; set; }
          = new List<PointOfInterestDto>();
    }
}
