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
        public ActionResult History(string id)
        {

            return View();
        }

        // GET: Orders/History *By Store Id
        public ActionResult History(int id)
        {
            return View();
        }
    }
}