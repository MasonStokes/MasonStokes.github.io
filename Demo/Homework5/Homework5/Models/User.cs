using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Homework5.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int PhoneNumber { get; set; }

        [Required]
        public string ApartmentName { get; set; }

        [Required]
        public int UnitNumber { get; set; }


    }
}