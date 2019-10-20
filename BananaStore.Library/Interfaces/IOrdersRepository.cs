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
        IEnumerable<Library.Models.Orders> GetAllOrdersByLocationId(string locationId);

        public Library.Models.Orders GetSingleOrder(string orderId);

        public bool PlaceOrder(Library.Models.Orders orders, List<Library.Models.OrderItems> orderItems);
        public Library.Models.OrderDetails GetOrderDetails(string orderId);

        public IEnumerable<Locations> GetLocationDetails(int? locationId);
        public IEnumerable<Library.Models.Customers> GetCustomerDetails(Guid customerId);
        public IEnumerable<Library.Models.LocationStockDetails> GetLocationStockDetails(int? locationId);
    }
}
