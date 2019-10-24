using System;
using Xunit;

namespace BananaStore.Tests
{
    public class ProductsTests
    {
        private BananaStore.Library.Models.Products product = new BananaStore.Library.Models.Products();
    
    
        [Fact]
        public void NewCustomerGivenGuidId()
        {
            var productId = Guid.NewGuid();
            product.ProductId = productId;
            var check = (product.ProductId != null && product.ProductId != Guid.Empty);
            Assert.True(check);
        }
    
        [Fact]
        public void NewProductRecieveProductName()
        {
            var productName = "Widget";
            product.ProductName = productName;
            Assert.Equal(productName, product.ProductName);
        }

        [Fact]
        public void NewCustomerRecieveProductDesc()
        {
            var productDesc = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";
            product.ProductName = productDesc;
            Assert.Equal(productDesc, product.ProductName);
        }
    }
}
