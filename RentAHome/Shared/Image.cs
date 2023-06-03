using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RentAHome.Shared
{
    public class Image
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        [JsonIgnore]
        public virtual House? House { get; set; }
        public int HouseId { get; set; }
    }
}
