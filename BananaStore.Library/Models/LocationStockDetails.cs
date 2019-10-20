using System;
using System.Collections.Generic;

namespace BananaStore.Library.Models
{
    public partial class LocationStockDetails
    {
        public int? LocationId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public string ProductName { get; set; }
        public string ProductDesc { get; set; }

        //public virtual Locations Location { get; set; }
        //public virtual Products Product { get; set; }
    }
}
