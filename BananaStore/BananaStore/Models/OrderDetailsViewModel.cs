using System;
using System.Collections.Generic;

namespace BananaStore.Models
{
    public partial class OrderDetailsViewModel
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
        public List<OrderDetailsItemsViewModel> Purchased { get; set; }

        public OrderDetailsViewModel()
        {
            List<OrderDetailsItemsViewModel> Purchased = new List<OrderDetailsItemsViewModel>();
        }

        //public virtual Customers Customer { get; set; }
        //public virtual Locations Location { get; set; }
    }
}
