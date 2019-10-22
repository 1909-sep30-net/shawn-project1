using System;
using Xunit;
using BananaStore.DataAccess.Entities;
using BananaStore.DataAccess.Repositories;
using BananaStore.Library.Interfaces;

namespace BananaStore.Tests
{
    public class UnitTest1
    {
     
        private readonly BananaStoreContext _dbContext;

        public UnitTest1(BananaStoreContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        [Fact]
        public void InvalidCustomerValidationTest()
        {

            var CustomerRepositoryTest = new CustomerRepository(_dbContext);

            var test = CustomerRepositoryTest.ValidateCustomerId((12345).ToString());
            
            Assert.False(test);
        }

        [Fact]
        public void ValidCustomerValidationTest()
        {

            var CustomerRepositoryTest = new CustomerRepository(_dbContext);

            var test = CustomerRepositoryTest.ValidateCustomerId("518B9A19-BD55-4497-A01F-2E48F23D8D30");

            Assert.True(test);
        }

    }

}
