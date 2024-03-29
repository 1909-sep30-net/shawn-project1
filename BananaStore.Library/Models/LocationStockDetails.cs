﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BananaStore.Library.Models
{
    public partial class LocationStockDetails
    {

        [DisplayName("Location Id")]
        public int? LocationId { get; set; }
        [DisplayName("Product Id")]
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [DisplayName("Product Description")]
        public string ProductDesc { get; set; }
    }
}
