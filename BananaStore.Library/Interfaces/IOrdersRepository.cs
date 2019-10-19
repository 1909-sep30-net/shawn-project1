using System;
using System.Collections.Generic;
using System.Text;
using BananaStore.Library.Models;

namespace BananaStore.Library.Interfaces
{
    public interface IOrdersRepository : IDisposable
    {
        // Add Get GetAll

        public IEnumerable<Library.Models.Orders> GetAllOrders();

        public IEnumerable<Library.Models.Orders> GetAllOrdersByCustomerId(string customerId);

        public Library.Models.Orders GetSingleOrder(string orderId);

        public void PlaceOrder(Library.Models.Orders orders);
        public Library.Models.OrderDetails GetOrderDetails(string orderId);
    }
}
