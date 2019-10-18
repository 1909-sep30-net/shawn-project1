using System;
using System.Collections.Generic;
using System.Text;
using BananaStore.Library.Models;

namespace BananaStore.Library.Interfaces
{
    public interface IOrderItemsRepository : IDisposable
    {
        public IEnumerable<Models.OrderItems> GetItemsFromOrder(Guid orderId);
    }
}
