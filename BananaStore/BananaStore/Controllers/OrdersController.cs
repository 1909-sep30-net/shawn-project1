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

        //Get: ORders/ChooseLocation
        public ActionResult ChooseLocation([FromQuery]string customerId)
        {


            TempData["CustomerId"] = customerId;


            return View();
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

        public ActionResult LocationHistory([FromQuery]string locationId)
        {
            var orders = _repository.GetAllOrdersByLocationId(locationId);

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

        //GET: Orders/NEW
        public ActionResult New([FromQuery]int locationId)
        {
            TempData["LocationId"] = locationId;

            var locationDetails = _repository.GetLocationDetails(locationId);
            var customerDetails = _repository.GetCustomerDetails(Guid.Parse(TempData["CustomerId"].ToString()));
            var locationStockDetails = _repository.GetLocationStockDetails(locationId);

            Library.Models.LocationOrderForm CurrentOrderDetails = new Library.Models.LocationOrderForm()
            {
                OrderId = Guid.NewGuid(),
                OrderDate = DateTime.Now,
                CustomerId = customerDetails.First().CustomerId,
                LocationId = locationDetails.First().LocationId,
                FirstName = customerDetails.First().FirstName,
                LastName = customerDetails.First().LastName,
                LocationName = locationDetails.First().LocationName,

                LocationStock = new List<LocationStockDetails>(),
                Purchased = new List<OrderDetailsItems>()
            };

            foreach (var item in locationStockDetails)
            {
                CurrentOrderDetails.LocationStock.Add(new LocationStockDetails()
                {
                    LocationId = item.LocationId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,

                    ProductName = item.ProductName,
                    ProductDesc = item.ProductDesc
                });
            }

            Models.LocationOrderFormViewModel CurrentOrderDetailsViewModel = new Models.LocationOrderFormViewModel()
            {
                OrderId = Guid.NewGuid(),
                OrderDate = DateTime.Now,
                CustomerId = customerDetails.First().CustomerId,
                LocationId = locationDetails.First().LocationId,
                FirstName = customerDetails.First().FirstName,
                LastName = customerDetails.First().LastName,
                LocationName = locationDetails.First().LocationName,

                LocationStock = new List<LocationStockDetailsViewModel>(),

                Purchased = new List<OrderDetailsItemsViewModel>()
            };

            foreach (var item in CurrentOrderDetails.LocationStock)
            {
                CurrentOrderDetailsViewModel.LocationStock.Add(new LocationStockDetailsViewModel()
                {
                    LocationId = item.LocationId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,

                    ProductName = item.ProductName,
                    ProductDesc = item.ProductDesc
                });
            }

            return View(CurrentOrderDetailsViewModel);
        }

        
        public ActionResult PlaceOrder([FromQuery]string OrderId, 
                                                  string CustomerId, 
                                                  string FirstName, 
                                                  string LastName, 
                                                  string LocationId, 
                                                  string LocationName,
                                                  string OrderDate,
                                                  string PN2676,
                                                  string PN4c99,
                                                  string PN1765,
                                                  string PNe7dc,
                                                  string PN5526
                                                  )
        {
            int? intLocationId = (int?)(int.Parse(LocationId));
            var locationDetails = _repository.GetLocationDetails(intLocationId);
            var customerDetails = _repository.GetCustomerDetails(Guid.Parse(CustomerId));
            var locationStockDetails = _repository.GetLocationStockDetails(intLocationId);

            Library.Models.LocationOrderForm CurrentOrderDetails = new Library.Models.LocationOrderForm()
            {
                OrderId = Guid.Parse(OrderId),
                OrderDate = DateTime.Now,
                CustomerId = Guid.Parse(CustomerId),
                LocationId = intLocationId,
                FirstName = customerDetails.First().FirstName,
                LastName = customerDetails.First().LastName,
                LocationName = locationDetails.First().LocationName,

                LocationStock = new List<LocationStockDetails>(),
                Purchased = new List<OrderDetailsItems>()
            };

            foreach (var item in locationStockDetails)
            {
                CurrentOrderDetails.LocationStock.Add(new LocationStockDetails()
                {
                    LocationId = item.LocationId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,

                    ProductName = item.ProductName,
                    ProductDesc = item.ProductDesc
                });
            }

            Library.Models.Orders FinalOrder = new Library.Models.Orders()
            {
                OrderId = Guid.Parse(OrderId),
                OrderDate = DateTime.Now,
                CustomerId = Guid.Parse(CustomerId),
                LocationId = intLocationId,
            };

            foreach (var item in locationStockDetails)
            {
                string MappedQuantity = "";
                switch (item.ProductId.ToString().Substring(0,4))
                {
                    case "2676":
                        MappedQuantity = PN2676;
                        break;
                    case "4c99":
                        MappedQuantity = PN4c99;
                        break;
                    case "1765":
                        MappedQuantity = PN1765;
                        break;
                    case "e7dc":
                        MappedQuantity = PNe7dc;
                        break;
                    case "5526":
                        MappedQuantity = PN5526;
                        break;
                    default:
                        MappedQuantity = "0";
                        break;
                }
                if (String.IsNullOrEmpty(MappedQuantity)) { MappedQuantity = "0"; }
                CurrentOrderDetails.Purchased.Add(new OrderDetailsItems()
                {
                    OrderId = Guid.Parse(OrderId),
                    ProductId = item.ProductId,
                    Quantity = (int?)int.Parse(MappedQuantity)
                });
            }

            List<OrderItems> FinalOrderItems = CurrentOrderDetails.Purchased.Select(x => new OrderItems()
            {
                OrderId = x.OrderId,
                ProductId = x.ProductId,
                Quantity = x.Quantity
            }).ToList();


            

            Models.LocationOrderFormViewModel CurrentOrderDetailsViewModel = new Models.LocationOrderFormViewModel()
            {
                OrderId = Guid.Parse(OrderId),
                OrderDate = DateTime.Now,
                CustomerId = Guid.Parse(CustomerId),
                LocationId = intLocationId,
                FirstName = customerDetails.First().FirstName,
                LastName = customerDetails.First().LastName,
                LocationName = locationDetails.First().LocationName,

                LocationStock = new List<LocationStockDetailsViewModel>(),

                Purchased = new List<OrderDetailsItemsViewModel>()
            };

            foreach (var item in CurrentOrderDetails.LocationStock)
            {
                CurrentOrderDetailsViewModel.LocationStock.Add(new LocationStockDetailsViewModel()
                {
                    LocationId = item.LocationId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,

                    ProductName = item.ProductName,
                    ProductDesc = item.ProductDesc
                });
            }

            foreach (var item in locationStockDetails)
            {
                string MappedQuantity = "";
                switch (item.ProductId.ToString().Substring(0, 4))
                {
                    case "2676":
                        MappedQuantity = PN2676;
                        break;
                    case "4c99":
                        MappedQuantity = PN4c99;
                        break;
                    case "1765":
                        MappedQuantity = PN1765;
                        break;
                    case "e7dc":
                        MappedQuantity = PNe7dc;
                        break;
                    case "5526":
                        MappedQuantity = PN5526;
                        break;
                    default:
                        MappedQuantity = "0";
                        break;
                }
                if (String.IsNullOrEmpty(MappedQuantity)) { MappedQuantity = "0"; }
                CurrentOrderDetailsViewModel.Purchased.Add(new OrderDetailsItemsViewModel()
                {
                    OrderId = Guid.Parse(OrderId),
                    ProductId = item.ProductId,
                    Quantity = (int?)int.Parse(MappedQuantity)
                });
            }

            if (_repository.PlaceOrder(FinalOrder, FinalOrderItems))
            {
                TempData["OrderCompleted"] = true;
                
            } else
            {
                TempData["OrderCompleted"] = false;
            }
            return View(CurrentOrderDetailsViewModel);
        }
    }
}