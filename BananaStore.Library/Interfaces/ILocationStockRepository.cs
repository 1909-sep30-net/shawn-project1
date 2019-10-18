using System;
using System.Collections.Generic;
using System.Text;

namespace BananaStore.Library.Interfaces
{
    public interface ILocationStockRepository : IDisposable
    {
        public IEnumerable<Library.Models.LocationStock> GetLocationStock(int? locationId);

        public bool UpdateLocationStock(Guid productId, int? locationId, int? quantity);
    }
}
