using System;
using System.Collections.Generic;

namespace BananaStore.DataAccess.Entities
{
    public partial class Customers
    {
        public Customers()
        {
            Orders = new HashSet<Orders>();
        }

        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
