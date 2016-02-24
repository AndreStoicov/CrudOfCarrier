using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudOfCarrier.Models.Entities
{
    public class CarrierRating
    {
        
        public int Stars { get; set; }
        public Carrier Carrier { get; set; }
        public User User { get; set; }
    }
}