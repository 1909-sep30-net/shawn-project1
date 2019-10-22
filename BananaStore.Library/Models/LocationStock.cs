using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BananaStore.Library.Models
{
    public partial class LocationStock
    {

        [DisplayName("Location Id")]
        public int? LocationId { get; set; }

        [DisplayName("Product Id")]
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
