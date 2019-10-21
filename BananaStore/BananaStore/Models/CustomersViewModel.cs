using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BananaStore.Models
{
    public class CustomersViewModel
    {
        public CustomersViewModel()
        {
          // Don't forget this piece of the puzzle
        }
        [DisplayName("Customer Id")]
        public Guid CustomerId { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }


        [Required]
        [DisplayName("First Name")]
        public string User_FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string User_LastName { get; set; }
    }
}
