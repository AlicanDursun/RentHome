using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RentAHome.Shared
{
    public class HouseType
    {
        public int Id { get; set; }
        public string TypeName { get; set; } = string.Empty;
        
        //public override bool Equals(object o)
        //{
        //    var other = o as HouseType;
        //    return other?.Id == Id;
        //}
        //public override int GetHashCode() => Id.GetHashCode();
        //public override string ToString()
        //{
        //    return TypeName;
        //}
    }
}
