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


        // Post: Customer/Search
        [HttpPost]
        public ActionResult Search(CustomersViewModel searched)
        {
            string thisMethod = TempData["actionMethod"].ToString();
            string nextMethod = "";
            switch (thisMethod)
            {
                case "ChooseLocation":
                    nextMethod = "New";
                    break;
                case "":
                    nextMethod = "";
                    break;
                default:
                    break;
            }

            IEnumerable<Customers> customers = _repository.GetCustomersByName(searched.User_FirstName, searched.User_LastName);
            
            var result = customers.Select(p => new CustomersViewModel
            {
                CustomerId = p.CustomerId,
                FirstName = p.FirstName,
                LastName = p.LastName

            }).First();

            if (!ModelState.IsValid)
            {

                return RedirectToAction(thisMethod, "Orders", new { CustomerId = result.CustomerId, actionMethod = nextMethod }, null);
            }
            return View(searched);
        }

        
        // GET: Customers/Search
        [HttpGet]
        public ActionResult Search(string actionMethod)
        {
            TempData["actionMethod"] = actionMethod;
            return View();
        }

        // GET: Customers/Create
        [HttpGet]
        public ActionResult Create(string actionMethod)
        {
            var newCustomerInfo = new CustomersViewModel();
            TempData["actionMethod"] = actionMethod;
            return View(newCustomerInfo);
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken] //CSRF Implementation
        public ActionResult Create(CustomersViewModel newCustomerInfo)
        {
            string thisMethod = TempData["actionMethod"].ToString();
            string nextMethod = "";
            switch (thisMethod)
            {
                case "ChooseLocation":
                    nextMethod = "New";
                    break;
                case "":
                    nextMethod = "";
                    break;
                default:
                    break;
            }


            if (!ModelState.IsValid)
            {
                var NewCustomer = _repository.AddCustomer(newCustomerInfo.User_FirstName, newCustomerInfo.User_LastName);
                return RedirectToAction(thisMethod, "Orders", new { CustomerId = NewCustomer.CustomerId, actionMethod = nextMethod }, null);
            }
            return RedirectToAction("Index");
        }
    }
}