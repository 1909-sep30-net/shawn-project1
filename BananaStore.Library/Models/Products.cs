using System;
using System.Collections.Generic;

namespace BananaStore.Library.Models
{
    public partial class Products
    {

        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
    }
}
