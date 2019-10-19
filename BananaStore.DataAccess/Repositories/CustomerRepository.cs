using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BananaStore.DataAccess.Entities;
using System.Linq;
using BananaStore.Library.Interfaces;
using BananaStore.Library.Models;


namespace BananaStore.DataAccess.Repositories
{
    public class CustomerRepository: ICustomerRepository
    {
        private readonly BananaStoreContext _dbContext;

        public CustomerRepository(BananaStoreContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IEnumerable<Library.Models.Customers> GetAllCustomers()
        {
            IEnumerable<Entities.Customers> customers = _dbContext.Customers;
            return customers.Select(Mapper.MapAllCustomers);
        }

        public Library.Models.Customers GetSingleCustomer(string customerId)
        {
            Entities.Customers customer = _dbContext.Customers.Where(n => n.CustomerId.ToString().Contains(customerId)).First();
            return Mapper.MapAllCustomers(customer);
        }

        public bool ValidateCustomerId(string customerId)
        {
            var CustomerCount = _dbContext.Customers.Where(n => n.CustomerId.ToString().Contains(customerId)).Count();
            return CustomerCount == 1;
        }

        public IEnumerable<Library.Models.Customers> GetCustomersByName(string firstName, string lastName)
        {
            IQueryable<Entities.Customers> CustomerResults = _dbContext.Customers.Where(n => n.FirstName.Contains(firstName) && n.LastName.Contains(lastName));
                        
            return CustomerResults.Select(Mapper.MapAllCustomers);
        }

        public Library.Models.Customers AddCustomer(string firstName, string lastName)
        {
            Library.Models.Customers newCustomer = new Library.Models.Customers(firstName, lastName);

            _dbContext.Customers.Add( Mapper.MapAllCustomers(newCustomer) );
            try
            {
                _dbContext.SaveChanges();
                //Log the addition here
            }catch (DbUpdateException ex)
            {
                // Log the exception here
            }

            return newCustomer;
        }

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                _disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }

}
