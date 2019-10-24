using System;
using Xunit;


namespace BananaStore.Tests
{
    public class LocationStockTests
    {

        private BananaStore.Library.Models.LocationStock locationStock = new BananaStore.Library.Models.LocationStock();

        [Fact]
        public void NewLocationCanRecieveLocationId()
        {
            Random rnd = new Random();
            int locationId = rnd.Next(10, 1000);
            locationStock.LocationId = locationId;
            Assert.Equal(locationId, locationStock.LocationId);
        }

        [Fact]
        public void NewLocaionCanRecieveProductId()
        {
            var productId = Guid.NewGuid();
            locationStock.ProductId = productId;
            var check = (locationStock.ProductId != null && locationStock.ProductId != Guid.Empty);
            Assert.True(check);
        }

        [Fact]
        public void NewLocationCanRecieveQuantity()
        {
            Random rnd = new Random();
            int qty = rnd.Next(10, 1000);
            locationStock.Quantity = qty;
            Assert.Equal(qty, locationStock.LocationId);
        }
    }
}
