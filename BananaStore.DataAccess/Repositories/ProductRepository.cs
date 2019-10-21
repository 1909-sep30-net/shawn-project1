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
    public class ProductRepository : IProductsRepository
    {

        private readonly BananaStoreContext _dbContext;

        public ProductRepository(BananaStoreContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Uses product Id to return all info about a product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>Model of single product</returns>
        Library.Models.Products IProductsRepository.GetSingleProduct(Guid productId)
        {
            IQueryable<Entities.Products> Product = from pd in _dbContext.Products
                                                    where pd.ProductId == productId
                                                    select pd;
            
            return Product.Select(Mapper.MapSingleProduct).First();

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
        // ~ProductRepository()
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
