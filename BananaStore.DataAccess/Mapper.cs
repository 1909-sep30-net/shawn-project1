using System;
using System.Collections.Generic;
using System.Text;
using BananaStore.Library.Models;
using System.Linq;

namespace BananaStore.DataAccess
{
    public static class Mapper
    {
        public static Entities.Customers MapAllCustomers(Library.Models.Customers customers)
        {
            return new Entities.Customers
            {
                CustomerId = customers.CustomerId,
                FirstName = customers.FirstName,
                LastName = customers.LastName
            };
            
        }

        public static Library.Models.Customers MapAllCustomers(Entities.Customers customers)
        {
            return new Library.Models.Customers
            {
                CustomerId = customers.CustomerId,
                FirstName = customers.FirstName,
                LastName = customers.LastName
            };
        }

        public static Entities.LocationStock MapLocationStock(Library.Models.LocationStock locationStock)
        {
            return new Entities.LocationStock
            {
                LocationId = locationStock.LocationId,
                ProductId = locationStock.ProductId,
                Quantity = locationStock.Quantity
            };
        }

        public static Library.Models.LocationStock MapLocationStock(Entities.LocationStock locationStock)
        {
            return new Library.Models.LocationStock
            {
                LocationId = locationStock.LocationId,
                ProductId = locationStock.ProductId,
                Quantity = locationStock.Quantity
            };
        }

        public static Entities.Products MapSingleProduct(Library.Models.Products products)
        {
            return new Entities.Products
            {
                ProductId = products.ProductId,
                ProductName = products.ProductName,
                ProductDesc = products.ProductDesc
            };
        }

        public static Library.Models.Products MapSingleProduct(Entities.Products products)
        {
            return new Library.Models.Products
            {
                ProductId = products.ProductId,
                ProductName = products.ProductName,
                ProductDesc = products.ProductDesc
            };
        }

        public static Entities.Locations MapSingleLocation(Library.Models.Locations locations)
        {
            return new Entities.Locations
            {
                LocationId = locations.LocationId,
                LocationName = locations.LocationName
            };
        }

        public static Library.Models.Locations MapSingleLocation(Entities.Locations locations)
        {
            return new Library.Models.Locations
            {
                LocationId = locations.LocationId,
                LocationName = locations.LocationName
            };
        }

        
        public static Entities.OrderItems MapSingleOrderItems(Library.Models.OrderItems orderItems)
        {
            return new Entities.OrderItems
            {
                OrderId = orderItems.OrderId,
                ProductId = orderItems.ProductId,
                Quantity = orderItems.Quantity
            };
        }

        public static Library.Models.OrderItems MapSingleOrderItems(Entities.OrderItems orderItems)
        {
            return new Library.Models.OrderItems
            {
                OrderId = orderItems.OrderId,
                ProductId = orderItems.ProductId,
                Quantity = orderItems.Quantity
            };
        }

        public static Entities.Orders MapSingleOrder(Library.Models.Orders orderItems)
        {
            return new Entities.Orders
            {
                OrderId = orderItems.OrderId,
                OrderDate = orderItems.OrderDate,
                CustomerId = orderItems.CustomerId,
                LocationId = orderItems.LocationId
            };
        }

        public static Library.Models.Orders MapSingleOrder(Entities.Orders orderItems)
        {
            return new Library.Models.Orders
            {
                OrderId = orderItems.OrderId,
                OrderDate = orderItems.OrderDate,
                CustomerId = orderItems.CustomerId,
                LocationId = orderItems.LocationId
            };
        }

    }
}
