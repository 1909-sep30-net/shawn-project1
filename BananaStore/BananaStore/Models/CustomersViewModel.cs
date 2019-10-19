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
        [Required]
        public Guid CustomerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string User_FirstName { get; set; }
        public string User_LastName { get; set; }
    }
}
