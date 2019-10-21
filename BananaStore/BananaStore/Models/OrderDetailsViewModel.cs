using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BananaStore.Models
{
    public partial class OrderDetailsViewModel
    {

        [DisplayName("Order Id")]
        public Guid OrderId { get; set; }

        [DisplayName("Order Date")]
        public DateTime OrderDate { get; set; }

        [DisplayName("Customer Id")]
        public Guid CustomerId { get; set; }
        [DisplayName("Location Id")]
        public int? LocationId { get; set; }

        //Customer Info
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        //Location Info
        [DisplayName("Location Name")]
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
