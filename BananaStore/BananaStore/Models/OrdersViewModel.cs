using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BananaStore.Models
{
    public partial class OrdersViewModel
    {
        public OrdersViewModel()
        {
            // Don't forget this piece of the puzzle
        }
        [Required]
        public Guid OrderId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public Guid CustomerId { get; set; }
        [Required]
        public int? LocationId { get; set; }
        public string User_OrderId { get; set; }
        public string User_CustomerId { get; set; }

        //public virtual Customers Customer { get; set; }
        //public virtual Locations Location { get; set; }
    }
}
