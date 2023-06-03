using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentAHome.Shared
{
    public class FavoriteHouseResponse
    {
        //DTO
        public int HouseId { get; set; }
        public string HouseCountry { get; set; } = string.Empty;
        public string HouseCity { get; set; } = string.Empty;
        public string HouseStreet { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
