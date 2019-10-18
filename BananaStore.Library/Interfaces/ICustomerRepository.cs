using System;
using System.Collections.Generic;
using System.Text;
using BananaStore.Library.Models;

namespace BananaStore.Library.Interfaces
{
    public interface ICustomerRepository : IDisposable
    {

        public IEnumerable<Customers> GetAllCustomers();
        public bool ValidateCustomerId(string customerId);

        Library.Models.Customers GetSingleCustomer(string customerId);
        IEnumerable<Library.Models.Customers> GetCustomersByName(string firstName, string lastName);
        Library.Models.Customers AddCustomer(string firstName, string lastName);


    }
}
