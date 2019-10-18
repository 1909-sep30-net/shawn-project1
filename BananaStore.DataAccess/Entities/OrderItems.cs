﻿using System;
using System.Collections.Generic;

namespace BananaStore.DataAccess.Entities
{
    public partial class OrderItems
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual Products Product { get; set; }
    }
}
