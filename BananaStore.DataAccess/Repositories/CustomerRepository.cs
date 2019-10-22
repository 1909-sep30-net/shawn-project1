using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BananaStore.DataAccess.Entities;
using System.Linq;
using BananaStore.Library.Interfaces;
using BananaStore.Library.Models;
using NLog;


namespace BananaStore.DataAccess.Repositories
{
    public class CustomerRepository: ICustomerRepository
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly BananaStoreContext _dbContext;

        public CustomerRepository(BananaStoreContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Lists all customers.
        /// </summary>
        /// <returns>IEnumerable of every customer.</returns>
        public IEnumerable<Library.Models.Customers> GetAllCustomers()
        {
            IEnumerable<Entities.Customers> customers = _dbContext.Customers;
            return customers.Select(Mapper.MapAllCustomers);
        }

        /// <summary>
        /// Returns Model for a single customer by Customer Id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>Model for customer</returns>
        public Library.Models.Customers GetSingleCustomer(string customerId)
        {
            Entities.Customers customer = _dbContext.Customers.Where(n => n.CustomerId.ToString().Contains(customerId)).First();
            return Mapper.MapAllCustomers(customer);
        }

        /// <summary>
        /// Validates input from the user to ensure it's a customer id in the system.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>Boolean - true if id exists, false if it doesn't</returns>
        public bool ValidateCustomerId(string customerId)
        {
            var CustomerCount = _dbContext.Customers.Where(n => n.CustomerId.ToString().Contains(customerId)).Count();
            return CustomerCount == 1;
        }

        /// <summary>
        /// Used to retrieve a list of customers whose names match user input
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns>List of Customers</returns>
        public IEnumerable<Library.Models.Customers> GetCustomersByName(string firstName, string lastName)
        {
            IQueryable<Entities.Customers> CustomerResults = _dbContext.Customers.Where(n => n.FirstName.Contains(firstName) && n.LastName.Contains(lastName));
                        
            return CustomerResults.Select(Mapper.MapAllCustomers);
        }

        /// <summary>
        /// Used to add customers to the database.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns>Model of the new customer.</returns>
        public Library.Models.Customers AddCustomer(string firstName, string lastName)
        {
            Library.Models.Customers newCustomer = new Library.Models.Customers(firstName, lastName);

            _dbContext.Customers.Add( Mapper.MapAllCustomers(newCustomer) );
            try
            {
                _dbContext.SaveChanges();
                logger.Debug($"Database Addition Successful | Customer Id : { newCustomer.CustomerId } First Name : { newCustomer.FirstName } Last Name : { newCustomer.LastName }");
            }
            catch (DbUpdateException ex)
            {
                logger.Error($"Database Update Exception | Adding New Customer Error ({firstName} {lastName}) : {ex}.");
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
