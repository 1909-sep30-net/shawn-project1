using System;
using System.Collections.Generic;

namespace BananaStore.Library.Models
{
    public partial class Customers
    {
        public Customers()
        {
            //Orders = new HashSet<Orders>();
        }

        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Customers(string firstName, string lastName)
        {
            CustomerId = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
