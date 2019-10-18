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

        Library.Models.Orders IOrdersRepository.GetSingleOrder(string orderId)
        {
            IQueryable<Entities.Orders> Order = from od in _dbContext.Orders
                                                where od.OrderId.ToString().Contains(orderId)
                                                select od;

            return Order.Select(Mapper.MapSingleOrder).First();
        }

        void IOrdersRepository.PlaceOrder(Library.Models.Orders orders)
        {
            var CurrentOrder = Mapper.MapSingleOrder(orders);
            _dbContext.Orders.Add(CurrentOrder);
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
