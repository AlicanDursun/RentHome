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
    public class Favorite
    {
        [Key]
        [Required] 
        public int Id { get; set; }
        [JsonIgnore]
        public virtual House? House { get; set; }
        public int HouseId { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; }
        public int? UserId { get; set; }


    }
}
