using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Homework7.Models
{
    public class SearchLog
    {
        /// <summary>
        /// Primary key for the table.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Holds the time of the request.
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Holds the type of the request.
        /// </summary>
        public string REQUESTTYPE { get; set; }

        /// <summary>
        /// Holds the client IP address.
        /// </summary>
        public string CLIENTIP { get; set; }

        /// <summary>
        /// Holds the clients browser information.
        /// </summary>
        public string BROWSER { get; set; }

        /// <summary>
        /// Overwrites ToString to print table entries.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{base.ToString()}: {TIMESTAMP} {REQUESTTYPE} {CLIENTIP} {BROWSER}";
        }

    }
}