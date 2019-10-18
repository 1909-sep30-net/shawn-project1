using System;
using Xunit;
using Project0.Library;
using Project0.DataAccess.Entities;

namespace Project0.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void InvetoryItemsOnItemNotinCart()
        {

            // Add
            var TestCart = new Cart();

            var TestCartItem1 = new OrderItems();
            TestCartItem1.OrderId = Guid.Parse("4dfce571-6d27-4bb1-92b1-120507eae0ce");
            TestCartItem1.ProductId = Guid.Parse("6e1a67cb-8493-4145-bcfa-2ab5ec30e9df");
            TestCartItem1.Quantity = 25;

            var TestCartItem2 = new OrderItems();
            TestCartItem2.OrderId = Guid.Parse("589479e2-9c27-48cc-a49b-c61f4e74acc3");
            TestCartItem2.ProductId = Guid.Parse("fc3d54a1-aee2-4a0f-b337-fa1d0a8b0a5c");
            TestCartItem2.Quantity = 25;

            TestCart.Products.Add(TestCartItem1);
            TestCart.Products.Add(TestCartItem2);

            int expected = 0;

            // Arrange

            int? actual = TestCart.InvetoryItems(Guid.Parse("4dfce571-6d27-4bb1-92b1-120507eae0ce"));

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InvetoryItemsOnItemInCart()
        {

            // Add
            var TestCart = new Cart();

            var TestCartItem1 = new OrderItems();
            TestCartItem1.OrderId = Guid.Parse("4dfce571-6d27-4bb1-92b1-120507eae0ce");
            TestCartItem1.ProductId = Guid.Parse("6e1a67cb-8493-4145-bcfa-2ab5ec30e9df");
            TestCartItem1.Quantity = 25;

            var TestCartItem2 = new OrderItems();
            TestCartItem2.OrderId = Guid.Parse("589479e2-9c27-48cc-a49b-c61f4e74acc3");
            TestCartItem2.ProductId = Guid.Parse("fc3d54a1-aee2-4a0f-b337-fa1d0a8b0a5c");
            TestCartItem2.Quantity = 25;

            TestCart.Products.Add(TestCartItem1);
            TestCart.Products.Add(TestCartItem2);

            int? expected = 25;

            // Arrange

            int? actual = TestCart.InvetoryItems(Guid.Parse("fc3d54a1-aee2-4a0f-b337-fa1d0a8b0a5c"));

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemoveItemsOnItemsInCart()
        {

            // Add
            var TestCart = new Cart();

            var TestCartItem1 = new OrderItems();
            TestCartItem1.OrderId = Guid.Parse("4dfce571-6d27-4bb1-92b1-120507eae0ce");
            TestCartItem1.ProductId = Guid.Parse("6e1a67cb-8493-4145-bcfa-2ab5ec30e9df");
            TestCartItem1.Quantity = 25;

            var TestCartItem2 = new OrderItems();
            TestCartItem2.OrderId = Guid.Parse("589479e2-9c27-48cc-a49b-c61f4e74acc3");
            TestCartItem2.ProductId = Guid.Parse("fc3d54a1-aee2-4a0f-b337-fa1d0a8b0a5c");
            TestCartItem2.Quantity = 25;

            TestCart.Products.Add(TestCartItem1);
            TestCart.Products.Add(TestCartItem2);

            bool expected = true;

            // Arrange

            bool actual = TestCart.Remove(Guid.Parse("fc3d54a1-aee2-4a0f-b337-fa1d0a8b0a5c"));

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemoveItemsOnItemsNotInCart()
        {

            // Add
            var TestCart = new Cart();

            var TestCartItem1 = new OrderItems();
            TestCartItem1.OrderId = Guid.Parse("4dfce571-6d27-4bb1-92b1-120507eae0ce");
            TestCartItem1.ProductId = Guid.Parse("6e1a67cb-8493-4145-bcfa-2ab5ec30e9df");
            TestCartItem1.Quantity = 25;

            var TestCartItem2 = new OrderItems();
            TestCartItem2.OrderId = Guid.Parse("589479e2-9c27-48cc-a49b-c61f4e74acc3");
            TestCartItem2.ProductId = Guid.Parse("fc3d54a1-aee2-4a0f-b337-fa1d0a8b0a5c");
            TestCartItem2.Quantity = 25;

            TestCart.Products.Add(TestCartItem1);
            TestCart.Products.Add(TestCartItem2);

            bool expected = false;

            // Arrange

            bool actual = TestCart.Remove(Guid.Parse("589479e2-9c27-48cc-a49b-c61f4e74acc3"));

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddNewItemsToEmptyCart()
        {

            // Add
            var TestCart = new Cart();

            var TestCartItem1 = new OrderItems();
            TestCartItem1.OrderId = Guid.Parse("4dfce571-6d27-4bb1-92b1-120507eae0ce");
            TestCartItem1.ProductId = Guid.Parse("6e1a67cb-8493-4145-bcfa-2ab5ec30e9df");
            TestCartItem1.Quantity = 25;

            //var TestCartItem2 = new OrderItems();
            //TestCartItem2.OrderId = Guid.Parse("589479e2-9c27-48cc-a49b-c61f4e74acc3");
            //TestCartItem2.ProductId = Guid.Parse("fc3d54a1-aee2-4a0f-b337-fa1d0a8b0a5c");
            //TestCartItem2.Quantity = 25;

            //TestCart.Products.Add(TestCartItem1);

            TestCart.Add(TestCartItem1);

            int? expected = 25;

            // Arrange

            int? actual = TestCart.InvetoryItems(Guid.Parse("6e1a67cb-8493-4145-bcfa-2ab5ec30e9df"));

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddNewItemsToCartWithItemsInItAlready()
        {

            // Add
            var TestCart = new Cart();

            var TestCartItem1 = new OrderItems();
            TestCartItem1.OrderId = Guid.Parse("4dfce571-6d27-4bb1-92b1-120507eae0ce");
            TestCartItem1.ProductId = Guid.Parse("6e1a67cb-8493-4145-bcfa-2ab5ec30e9df");
            TestCartItem1.Quantity = 25;

            var TestCartItem2 = new OrderItems();
            TestCartItem2.OrderId = Guid.Parse("589479e2-9c27-48cc-a49b-c61f4e74acc3");
            TestCartItem2.ProductId = Guid.Parse("fc3d54a1-aee2-4a0f-b337-fa1d0a8b0a5c");
            TestCartItem2.Quantity = 25;

            TestCart.Products.Add(TestCartItem1);

            TestCart.Add(TestCartItem2);

            int? expected = 25;

            // Arrange

            int? actual = TestCart.InvetoryItems(Guid.Parse("fc3d54a1-aee2-4a0f-b337-fa1d0a8b0a5c"));

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddItemsToCartWithSameItemsAlreadyInCart()
        {

            // Add
            var TestCart = new Cart();

            var TestCartItem1 = new OrderItems();
            TestCartItem1.OrderId = Guid.Parse("4dfce571-6d27-4bb1-92b1-120507eae0ce");
            TestCartItem1.ProductId = Guid.Parse("6e1a67cb-8493-4145-bcfa-2ab5ec30e9df");
            TestCartItem1.Quantity = 25;

            var TestCartItem2 = new OrderItems();
            TestCartItem2.OrderId = Guid.Parse("589479e2-9c27-48cc-a49b-c61f4e74acc3");
            TestCartItem2.ProductId = Guid.Parse("fc3d54a1-aee2-4a0f-b337-fa1d0a8b0a5c");
            TestCartItem2.Quantity = 25;

            TestCart.Products.Add(TestCartItem1);
            TestCart.Products.Add(TestCartItem2);

            TestCart.Add(TestCartItem2);

            int? expected = 50;

            // Arrange

            int? actual = TestCart.InvetoryItems(Guid.Parse("fc3d54a1-aee2-4a0f-b337-fa1d0a8b0a5c"));

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PlaceOrderWithProductNotInStockAtLocation()
        {

            // Add
            var TestCart = new Cart();
            TestCart.CustomerId = Guid.Parse("E21A6012-C4C4-48B8-B110-AA1AC9F64A32"); //Elmer Fudd
            TestCart.LocationId = 30;

            var TestCartItem1 = new OrderItems();
            TestCartItem1.OrderId = TestCart.OrderId;
            TestCartItem1.ProductId = Guid.Parse("4A4AA035-8FB3-40E4-8FFD-7FB18B89C3CB");
            TestCartItem1.Quantity = 25;

            TestCart.Products.Add(TestCartItem1);

            bool expected = true;

            // Arrange

            bool actual = TestCart.PlaceOrder();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PlaceOrderWithProductInStockAtLocation()
        {

            // Add
            var TestCart = new Cart();

            var TestCartItem1 = new OrderItems();
            TestCartItem1.OrderId = Guid.Parse("4dfce571-6d27-4bb1-92b1-120507eae0ce");
            TestCartItem1.ProductId = Guid.Parse("6e1a67cb-8493-4145-bcfa-2ab5ec30e9df");
            TestCartItem1.Quantity = 25;

            var TestCartItem2 = new OrderItems();
            TestCartItem2.OrderId = Guid.Parse("589479e2-9c27-48cc-a49b-c61f4e74acc3");
            TestCartItem2.ProductId = Guid.Parse("fc3d54a1-aee2-4a0f-b337-fa1d0a8b0a5c");
            TestCartItem2.Quantity = 25;

            TestCart.Products.Add(TestCartItem1);
            TestCart.Products.Add(TestCartItem2);

            TestCart.Add(TestCartItem2);

            int? expected = 50;

            // Arrange

            int? actual = TestCart.InvetoryItems(Guid.Parse("fc3d54a1-aee2-4a0f-b337-fa1d0a8b0a5c"));

            // Assert
            Assert.Equal(expected, actual);
        }



        //4A4AA035-8FB3-40E4-8FFD-7FB18B89C3CB




    }
}
