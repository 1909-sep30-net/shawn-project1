using System;
using Xunit;
using BananaStore.DataAccess.Entities;
using BananaStore.DataAccess.Repositories;
using BananaStore.Library.Interfaces;


namespace BananaStore.Tests
{
    public class OrdersTests
    {

        private readonly Orders _orders = new Orders();

        [Fact]
        public void Name_NonEmptyValue_StoresCorrectly()
        {
             Assert.IsType<Guid>(_orders.OrderId);

        }
    }
}
