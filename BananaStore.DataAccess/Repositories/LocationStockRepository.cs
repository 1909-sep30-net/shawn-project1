using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BananaStore.DataAccess.Entities;
using System.Linq;
using BananaStore.Library.Interfaces;

namespace BananaStore.DataAccess.Repositories
{
    public class LocationStockRepository : ILocationStockRepository, IDisposable
    {
        private readonly BananaStoreContext _dbContext;

        public LocationStockRepository(BananaStoreContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IEnumerable<Library.Models.LocationStock> GetLocationStock(int? locationId)
        {
            IQueryable<Entities.LocationStock> Invetory =   from ls in _dbContext.LocationStock
                                                            where ls.LocationId == locationId
                                                            select ls;

            return Invetory.Select(Mapper.MapLocationStock);
        }

        public bool UpdateLocationStock(Guid productId, int? locationId, int? quantity)
        {
            var CurrentItem = from ls in _dbContext.LocationStock
                              where ((ls.ProductId.Equals(productId)) && (ls.LocationId == locationId))
                              select ls;
            if (CurrentItem.Count() != 1)
            {
                return false;
            } else
            {
                CurrentItem.First().Quantity -= (int)quantity;
                _dbContext.SaveChanges();
                return true;
            }
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
        // ~LocationStockRepository()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}

