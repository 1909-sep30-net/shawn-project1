using System;
using System.Collections.Generic;

namespace BananaStore.Library.Models
{
    public partial class OrderDetails
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid CustomerId { get; set; }
        public int? LocationId { get; set; }

        //Customer Info
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //Location Info
        public string LocationName { get; set; }
        //Products Info | OrderItems
        public List<OrderDetailsItems> Purchased { get; set; }

        public OrderDetails()
        {
            List<OrderDetailsItems> Purchased = new List<OrderDetailsItems>();
        }
    }
}
