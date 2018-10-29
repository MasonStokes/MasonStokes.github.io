using Homework5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Homework5.DAL
{
    public class UserContext : DbContext
    {
        public UserContext() : base("name=User")
        {

        }

        public virtual DbSet<User> Users { get; set; }
    }
}