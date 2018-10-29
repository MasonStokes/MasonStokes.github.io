using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Homework5.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required, Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required, Display(Name = "Apartment Name")]
        public string ApartmentName { get; set; }

        [Required, Display(Name = "Unit Number"]
        public int UnitNumber { get; set; }

        [Required]
        public string Explanation { get; set; }

        public bool Permission { get; set; }
    }
}