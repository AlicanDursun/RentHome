using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RentAHome.Shared
{
    public class UserType
    {
        public int Id { get; set; }
        [Required]
        public string TypeName { get; set; } = string.Empty;
        public User User { get; set; }


    }
}
