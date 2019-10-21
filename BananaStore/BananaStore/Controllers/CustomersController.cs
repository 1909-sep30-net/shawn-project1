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
    public class CustomersController : Controller
    {

        private readonly ICustomerRepository _repository;

        public CustomersController(ICustomerRepository repository)
        {
            _repository = repository;
        }


        // GET: Customers from firstname/lastname
        public ActionResult CustomerSearchResults([FromQuery]string FirstName, [FromQuery]string LastName)
        {

            IEnumerable<Customers> customers = _repository.GetCustomersByName(FirstName, LastName);
            
            var viewModels = customers.Select(p => new CustomersViewModel
            {
                CustomerId = p.CustomerId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                User_FirstName = FirstName,
                User_LastName = LastName

            }).ToList();

            
            
            return View(viewModels);
        }

        
        // GET: Customers/Search
        public ActionResult Search()
        {
            return View();
        }


        // GET: Customers/Create
        public ActionResult CreateSuccess([FromQuery]Guid CustomerId, [FromQuery]string FirstName, [FromQuery]string LastName)
        {
            var viewModel = new CustomersViewModel()
            {
                CustomerId = CustomerId,
                FirstName = FirstName,
                LastName = LastName
            };

            return View(viewModel);
        }

        // POST: Customers/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(CustomersViewModel newCustomerInfo)
        {
            try
            {
                var NewCustomer = _repository.AddCustomer(newCustomerInfo.User_FirstName, newCustomerInfo.User_LastName);
                return RedirectToAction("ChooseLocation", "Orders", new { CustomerId = NewCustomer.CustomerId }, null);
            }
            catch
            {
                return View("Error");
            }
        }
    }
}