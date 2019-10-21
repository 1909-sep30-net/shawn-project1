using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BananaStore.DataAccess.Entities;
using System.Linq;
using BananaStore.Library.Interfaces;
using BananaStore.Library.Models;
using NLog;

namespace BananaStore.DataAccess.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly BananaStoreContext _dbContext;

        public OrdersRepository(BananaStoreContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Lists all orders in database
        /// </summary>
        /// <returns>List of all orders</returns>
        IEnumerable<Library.Models.Orders> IOrdersRepository.GetAllOrders()
        {
            IQueryable<Entities.Orders> Order = from od in _dbContext.Orders
                                                select od;

            return (IEnumerable<Library.Models.Orders>) Order.Select(Mapper.MapSingleOrder);
        }

        /// <summary>
        /// Lists all orders in database from a certain customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>IEnumerable of Orders matching customerId</returns>
        IEnumerable<Library.Models.Orders> IOrdersRepository.GetAllOrdersByCustomerId(string customerId)
        {
            IQueryable<Entities.Orders> Order = from od in _dbContext.Orders
                                                where od.CustomerId.ToString().Contains(customerId)
                                                select od;

            return Order.Select(Mapper.MapSingleOrder);
        }

        /// <summary>
        /// Lists all orders in data base from a certain location
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns>IEnumberable of Orders matching locationId</returns>
        IEnumerable<Library.Models.Orders> IOrdersRepository.GetAllOrdersByLocationId(string locationId)
        {
            var locationIdAsInt = (int?) int.Parse(locationId);
            IQueryable<Entities.Orders> Order = from od in _dbContext.Orders
                                                where od.LocationId == locationIdAsInt
                                                select od;


            return Order.Select(Mapper.MapSingleOrder);
        }

        /// <summary>
        /// Returns a single order by orderId
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>Single order matching orderId</returns>
        Library.Models.Orders IOrdersRepository.GetSingleOrder(string orderId)
        {
            IQueryable<Entities.Orders> Order = from od in _dbContext.Orders
                                                where od.OrderId.ToString().Contains(orderId)
                                                select od;

            return Order.Select(Mapper.MapSingleOrder).First();
        }

        /// <summary>
        /// Places order in database across 3 Tables:
        /// Orders, OrderItems and LocationStock.
        /// Adds order info to the Orders table.
        /// Add all OrderItems info to the OrderITems table.
        /// Updates LocationStock as needed (Depletes LocationStock quantities as needed)
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="orderItems"></param>
        /// <returns>Boolean - true if everything was successful, false if there was an exception.</returns>
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
                try
                {
                    _dbContext.SaveChanges();
                    logger.Debug($"Stock updated successfully. | LocationId : {CurrentItem.First().LocationId} | ProductId : {item.ProductId}.");
                } catch (DbUpdateException ex)
                {
                    logger.Error($"Database Update Exception. | LocationId : {CurrentItem.First().LocationId}  | ProductId : {item.ProductId}: {ex}.");
                    return false;
                }
                
            }

            // put order in db (Orders Table)
            var FinalOrder = Mapper.MapSingleOrder(orders);
            _dbContext.Orders.Add(FinalOrder);
            try
            {
                _dbContext.SaveChanges();
                logger.Debug($"Order created successfully. | OrderId : {FinalOrder.OrderId}.");
            }
            catch (DbUpdateException ex)
            {
                logger.Error($"Database Update Exception | Orders Tabe Order Id : {orders.OrderId}: {ex}.");
                return false;
            }

            // put orderitems in db (OrderItems Table)
            var FinalOrderItems = orderItems.Select(Mapper.MapSingleOrderItems);
            foreach (var finalOrderItem in FinalOrderItems)
            {
                _dbContext.OrderItems.Add(finalOrderItem);
                try
                {
                    _dbContext.SaveChanges();
                    logger.Debug($"OrderItem created successfully. | OrderId : {FinalOrder.OrderId} | ProductId : {FinalOrder.OrderId}.");
                } catch(DbUpdateException ex)
                {
                    logger.Error($"Database Update Exception | Orders Tabe Order Id : {orders.OrderId}: {ex}.");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Get customer details by Customer Id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>Ienumerable of a single customer model matching customerId.</returns>
        public IEnumerable<Library.Models.Customers> GetCustomerDetails(Guid customerId)
        {
            IEnumerable<Entities.Customers> customers = from cs in _dbContext.Customers
                                                        where cs.CustomerId.ToString().Equals(customerId.ToString())
                                                        select cs;

            return customers.Select(Mapper.MapAllCustomers);
        }

        /// <summary>
        /// Get location details by Location Id.
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns>IEnumerable of location matching locationId.</returns>
        public IEnumerable<Library.Models.Locations> GetLocationDetails(int? locationId)
        {
            IEnumerable<Entities.Locations> locations = from ls in _dbContext.Locations
                                                        where ls.LocationId == locationId
                                                        select ls;

            return locations.Select(Mapper.MapSingleLocation);
        }

        /// <summary>
        /// Get order details by Order Id.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>Single model of all order details matching orderId</returns>
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

        /// <summary>
        /// Get stock details of a single location.
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns>IEnumerable containing all details about stock at a location matching locationId.</returns>
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
