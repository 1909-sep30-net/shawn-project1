using System;
using System.Collections.Generic;

namespace BananaStore.Library.Models
{
    public partial class OrderDetailsItems
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int? Quantity { get; set; }

        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
    }
}
