using System;
using System.Collections.Generic;

namespace BananaStore.Library.Models
{
    public partial class LocationStock
    {
        public int? LocationId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        //public virtual Locations Location { get; set; }
        //public virtual Products Product { get; set; }
    }
}
