using System;
using System.Collections.Generic;

namespace BananaStore.DataAccess.Entities
{
    public partial class Products
    {
        public Products()
        {
            LocationStock = new HashSet<LocationStock>();
            OrderItems = new HashSet<OrderItems>();
        }

        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }

        public virtual ICollection<LocationStock> LocationStock { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
