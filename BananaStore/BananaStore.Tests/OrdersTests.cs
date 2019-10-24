using System;
using Xunit;
using BananaStore.DataAccess.Entities;
using BananaStore.DataAccess.Repositories;
using BananaStore.Library.Models;


namespace BananaStore.Tests
{
    public class OrdersTests
    {

        private BananaStore.Library.Models.Orders orders = new BananaStore.Library.Models.Orders();

        [Fact]
        public void NewOrderGivenGuidId()
        {
            var check = (orders.OrderId != null && orders.OrderId != Guid.Empty);
            Assert.True(check);
        }

        [Fact]
        public void NewOrderCanRecieveCustomerId()
        {
            var customerId = Guid.NewGuid();
            orders.CustomerId = customerId;
            var check = (orders.CustomerId != null && orders.CustomerId != Guid.Empty);
            Assert.True(check);
        }

        [Fact]
        public void NewOrderCanRecieveLocationId()
        {
            Random rnd = new Random();
            int locationId = rnd.Next(10, 1000);
            orders.LocationId = locationId;
            Assert.Equal(locationId, orders.LocationId);
        }
    }
}
