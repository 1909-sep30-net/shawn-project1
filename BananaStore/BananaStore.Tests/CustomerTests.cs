using System;
using Xunit;

namespace BananaStore.Tests
{
    public class CustomerTests
    {
        private BananaStore.Library.Models.Customers customer = new BananaStore.Library.Models.Customers("George", "Costanza");
    
    
        [Fact]
        public void NewCustomerGivenGuidId()
            {
                var check = (customer.CustomerId != null && customer.CustomerId != Guid.Empty);
                Assert.True(check);
            }
    
        [Fact]
        public void NewCustomerRecieveFirstName()
        {
                Assert.Equal("George", customer.FirstName);
        }

        [Fact]
        public void NewCustomerRecieveLastName()
        {
            Assert.Equal("Costanza", customer.LastName);
        }

    }
}
