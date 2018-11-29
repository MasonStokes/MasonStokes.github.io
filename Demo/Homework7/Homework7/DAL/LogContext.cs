using Homework7.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Homework7.DAL
{
    public class LogContext : DbContext
    {
        public LogContext() : base("name=SearchLogs") { }

        public virtual DbSet<SearchLog> SearchLogs { get; set; }
    }
}