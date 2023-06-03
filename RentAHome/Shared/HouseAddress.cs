using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RentAHome.Shared
{
    public class HouseAddress
    {
        [Key]
        [Required]
        [ForeignKey(nameof(House))]
        public int HouseId { get; set; }
        public string Street { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        [Required]
        public virtual Country Country { get; set; }
        public int CountryId { get; set; }
        [Required]
        public virtual City City { get; set; }
        public int CityId { get; set; }

        [JsonIgnore]
        public virtual House? House { get; set; }
    }
}
