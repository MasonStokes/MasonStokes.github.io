using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Homework6.Models.ImportersViewModel
{
    public class ViewModel
    {
        /// <summary>
        /// These are each an object of their given type (Person, Customer, InvoiceLine)
        /// and they allow a given item to be assigned to them from the database.
        /// </summary>
        public Person Person { get; set; }

        public Customer Customer { get; set; }

        public List<InvoiceLine> InvoiceLine { get; set; }
    }
}