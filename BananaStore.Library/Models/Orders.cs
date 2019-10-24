using System;
using System.Collections.Generic;

namespace BananaStore.Library.Models
{
    public partial class Orders
    {

        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid CustomerId { get; set; }
        public int? LocationId { get; set; }

        public Orders()
        {
            OrderId = Guid.NewGuid();
        }

    }
}
