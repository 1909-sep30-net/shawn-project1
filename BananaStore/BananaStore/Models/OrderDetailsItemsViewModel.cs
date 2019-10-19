using System;
using System.Collections.Generic;

namespace BananaStore.Models
{
    public partial class OrderDetailsItemsViewModel
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int? Quantity { get; set; }

        public string ProductName { get; set; }
        public string ProductDesc { get; set; }

        //public virtual Products Product { get; set; }
    }
}
