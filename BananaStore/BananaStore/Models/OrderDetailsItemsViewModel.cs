using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BananaStore.Models
{
    public partial class OrderDetailsItemsViewModel
    {
        [DisplayName("Order Id")]
        public Guid OrderId { get; set; }
        [DisplayName("Product Id")]
        public Guid ProductId { get; set; }
        public int? Quantity { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [DisplayName("Product Description")]
        public string ProductDesc { get; set; }

        //public virtual Products Product { get; set; }
    }
}
