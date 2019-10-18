using System;
using System.Collections.Generic;
using System.Text;
using BananaStore.Library.Models;

namespace BananaStore.Library.Interfaces
{
    public interface IProductsRepository : IDisposable
    {
        Products GetSingleProduct(Guid productId);
    }
}
