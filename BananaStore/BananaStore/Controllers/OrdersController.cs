using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BananaStore.Library.Models;
using BananaStore.Models;
using BananaStore.Library.Interfaces;

namespace BananaStore.Controllers
{
    public class OrdersController : Controller
    {

        private readonly IOrdersRepository _repository;

        public OrdersController(IOrdersRepository repository)
        {
            _repository = repository;
        }

        // GET: Orders/History *By Customer Guid
        public ActionResult History([FromQuery]string customerId)
        {
            var orders = _repository.GetAllOrdersByCustomerId(customerId);

            var viewModel = orders.Select(o => new OrdersViewModel()
            {
                OrderId = o.OrderId,
                OrderDate = o.OrderDate,
                CustomerId = o.CustomerId,
                LocationId = o.LocationId
            }).ToList();

            return View(viewModel);
        }

        // Get: Orders/Details *By OrderId
        public ActionResult Details([FromQuery]string orderId)
        {
            var orders = _repository.GetOrderDetails(orderId);

            OrderDetailsViewModel viewModel = new OrderDetailsViewModel()
            {
                OrderId = orders.OrderId,
                OrderDate = orders.OrderDate,
                CustomerId = orders.CustomerId,
                LocationId = orders.LocationId,
                FirstName = orders.FirstName,
                LastName = orders.LastName,
                LocationName = orders.LocationName,
                Purchased = new List<OrderDetailsItemsViewModel>()
            };

            foreach (var item in orders.Purchased)
            {
                viewModel.Purchased.Add(new OrderDetailsItemsViewModel()
                {
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    ProductName = item.ProductName,
                    ProductDesc = item.ProductDesc
                });
            }

            var viewModelList = new List<OrderDetailsViewModel>();
            viewModelList.Add(viewModel);
            IEnumerable<OrderDetailsViewModel> ienumerableModel = viewModelList;
            return View(ienumerableModel);
        }

        //GET: Orders/NEW* By customerId
        public ActionResult New([FromQuery]int customerId)
        {
            return View();
        }
    }
}