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


        // GET: Customers
        public ActionResult Index([FromQuery]string FirstName, [FromQuery]string LastName)
        {

            IEnumerable<Customers> customers = _repository.GetCustomersByName(FirstName, LastName);

            if (customers.Count() == 0)
            {
                Response.Redirect("http://www.microsoft.com/gohere/look.htm");
            }

            var viewModels = customers.Select(p => new CustomersViewModel
            {
                CustomerId = p.CustomerId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                User_FirstName = FirstName,
                User_LastName = LastName

            });

            return View(viewModels);
        }

        //// GET: Customers/Details
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}