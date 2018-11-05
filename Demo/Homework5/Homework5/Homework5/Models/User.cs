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

        [Required, MaxLength(64), Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, MaxLength(128), Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required, MaxLength(64), Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required, MaxLength(128), Display(Name = "Apartment Name")]
        public string ApartmentName { get; set; }

        [Required, Display(Name = "Unit Number")]
        public int UnitNumber { get; set; }

        [Required, MaxLength(512), Display(Name = "Explanation of Request")]
        public string Explanation { get; set; }

        public bool Permission { get; set; }

        private DateTime date = DateTime.Now;
        [Required, Display(Name = "Time of Submission")]
        public DateTime SubmitTime
        {
            get { return date; }
            set { date = value; }
        }
    }
}