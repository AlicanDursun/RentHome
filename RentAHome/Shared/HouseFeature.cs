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
    public class HouseFeature
    {
        [Key]
        [Required]
        [ForeignKey(nameof(House))]
        public int HouseId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SquareMeters { get; set; }

        public int RoomCount { get; set; }

        public int BalconyCount { get; set; }

        public bool NaturalGas { get; set; }

        public bool Furnished { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
       
        [JsonIgnore]
        public virtual House? House { get; set; }
       


    }
}
