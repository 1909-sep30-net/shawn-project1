using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BananaStore.DataAccess.Entities;
using System.Linq;
using BananaStore.Library.Interfaces;
using BananaStore.Library.Models;

namespace BananaStore.DataAccess.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {

        private readonly BananaStoreContext _dbContext;

        public OrdersRepository(BananaStoreContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        IEnumerable<Library.Models.Orders> IOrdersRepository.GetAllOrders()
        {
            IQueryable<Entities.Orders> Order = from od in _dbContext.Orders
                                                select od;

            return (IEnumerable<Library.Models.Orders>) Order.Select(Mapper.MapSingleOrder);
        }

        IEnumerable<Library.Models.Orders> IOrdersRepository.GetAllOrdersByCustomerId(string customerId)
        {
            IQueryable<Entities.Orders> Order = from od in _dbContext.Orders
                                                where od.CustomerId.ToString().Contains(customerId)
                                                select od;

            return Order.Select(Mapper.MapSingleOrder);
        }

        IEnumerable<Library.Models.Orders> IOrdersRepository.GetAllOrdersByLocationId(string locationId)
        {
            var locationIdAsInt = (int?) int.Parse(locationId);
            IQueryable<Entities.Orders> Order = from od in _dbContext.Orders
                                                where od.LocationId == locationIdAsInt
                                                select od;


            return Order.Select(Mapper.MapSingleOrder);
        }

        Library.Models.Orders IOrdersRepository.GetSingleOrder(string orderId)
        {
            IQueryable<Entities.Orders> Order = from od in _dbContext.Orders
                                                where od.OrderId.ToString().Contains(orderId)
                                                select od;

            return Order.Select(Mapper.MapSingleOrder).First();
        }

        bool IOrdersRepository.PlaceOrder(Library.Models.Orders orders, List<Library.Models.OrderItems> orderItems)
        {
            // Check stock
            var locationStock = from ls in _dbContext.LocationStock
                                where ls.LocationId == orders.LocationId
                                select ls;

            foreach (var item in orderItems)
            {
                foreach (var stockitem in locationStock)
                {
                    if (item.ProductId == stockitem.ProductId)
                    {
                        if (stockitem.Quantity < item.Quantity)
                        {
                            return false;
                        }
                    }
                }
            }


            // Update stock
            foreach (var item in orderItems)
            {
                var CurrentItem = from ls in _dbContext.LocationStock
                                  where ((ls.LocationId == orders.LocationId) && (ls.ProductId == item.ProductId))
                                  select ls;
                if (CurrentItem.Count() != 1)
                {
                    return false;
                }

                CurrentItem.First().Quantity -= (int)item.Quantity;
                _dbContext.SaveChanges();
            }

            // put order in db (Orders Table)
            var FinalOrder = Mapper.MapSingleOrder(orders);
            _dbContext.Orders.Add(FinalOrder);
            _dbContext.SaveChanges();
            // put order in db (OrderItems Table)
            var FinalOrderItems = orderItems.Select(Mapper.MapSingleOrderItems);
            foreach (var finalOrderItem in FinalOrderItems)
            {
                _dbContext.OrderItems.Add(finalOrderItem);
                _dbContext.SaveChanges();
            }

            return true;
        }

        public IEnumerable<Library.Models.Customers> GetCustomerDetails(Guid customerId)
        {
            IEnumerable<Entities.Customers> customers = from cs in _dbContext.Customers
                                                        where cs.CustomerId.ToString().Equals(customerId.ToString())
                                                        select cs;

            return customers.Select(Mapper.MapAllCustomers);
        }

        public IEnumerable<Library.Models.Locations> GetLocationDetails(int? locationId)
        {
            IEnumerable<Entities.Locations> locations = from ls in _dbContext.Locations
                                                        where ls.LocationId == locationId
                                                        select ls;

            return locations.Select(Mapper.MapSingleLocation);
        }

        Library.Models.OrderDetails IOrdersRepository.GetOrderDetails(string orderId)
        {
            IEnumerable<Entities.Orders> order = from od in _dbContext.Orders
                                                 where od.OrderId.ToString().Contains(orderId)
                                                 select od;

            var orderItems = from oi in _dbContext.OrderItems
                                                          where oi.OrderId.ToString().Equals(orderId)
                                                          join pd in _dbContext.Products on oi.ProductId.ToString() equals pd.ProductId.ToString()
                                                          select new
                                                          {
                                                              OrderId = oi.OrderId,
                                                              ProductId = oi.ProductId,
                                                              Quantity = oi.Quantity,
                                                              ProductName = pd.ProductName,
                                                              ProductDesc = pd.ProductDesc
                                                          };

            IEnumerable<Entities.Products> products = from ps in _dbContext.Products
                                                      select ps;

            IEnumerable <Entities.Customers> customers = from cs in _dbContext.Customers
                                                           where cs.CustomerId.ToString().Equals(order.First().CustomerId.ToString())
                                                           select cs;

            IEnumerable<Entities.Locations> locations = from ls in _dbContext.Locations
                                                        where ls.LocationId == order.First().LocationId
                                                        select ls;

            OrderDetails LibraryOrderDetails = new OrderDetails()
            {
                OrderId = order.First().OrderId,
                OrderDate = order.First().OrderDate,
                CustomerId = order.First().CustomerId,
                LocationId = order.First().LocationId,
                FirstName = customers.First().FirstName,
                LastName = customers.First().LastName,
                LocationName = locations.First().LocationName,
                Purchased = new List<OrderDetailsItems>()
                
            };

            var LibraryOrderDetailsItems = orderItems.Select(x => new Library.Models.OrderDetailsItems()
            {
                OrderId = x.OrderId,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                ProductName = x.ProductName,
                ProductDesc = x.ProductDesc
                

            }).ToList();

            foreach (var item in LibraryOrderDetailsItems)
            {
                LibraryOrderDetails.Purchased.Add(item);
            }

            return LibraryOrderDetails;
        }

        public IEnumerable<Library.Models.LocationStockDetails> GetLocationStockDetails(int? locationId)
        {
            var locations = from ls in _dbContext.LocationStock
                            where ls.LocationId == locationId
                            join pd in _dbContext.Products on ls.ProductId equals pd.ProductId
                            select new
                            {
                                LocationId = ls.LocationId,
                                ProductId = pd.ProductId,
                                Quantity = ls.Quantity,
                                ProductName = pd.ProductName,
                                ProductDesc = pd.ProductDesc
                            };
            IEnumerable<Library.Models.LocationStockDetails> locationStockDetails = locations.Select(x => new LocationStockDetails()
                                                                                                    {
                                                                                                        LocationId = x.LocationId,
                                                                                                        ProductId = x.ProductId,
                                                                                                        Quantity = x.Quantity,
                                                                                                        ProductName = x.ProductName,
                                                                                                        ProductDesc = x.ProductDesc 
                                                                                                    }).ToList();

            return locationStockDetails;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~OrdersRepository()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
