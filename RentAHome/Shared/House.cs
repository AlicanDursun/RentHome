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
    public class House
    {
        public int Id { get; set; }
        public string HouseTitle { get; set; } = string.Empty;
        public string HouseDescription { get; set; } = string.Empty;
        [Required]
        public virtual HouseAddress? HouseAddress { get; set; }
        [Required]
        public virtual HouseFeature? HouseFeature { get; set; } 
        public virtual List<Image>? Images { get; set; }
        public virtual User? User { get; set; }
        public int? UserId { get; set; }
        public virtual List<Favorite>? Favorites { get; set; }

    }

}
