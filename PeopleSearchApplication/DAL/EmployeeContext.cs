using PeopleSearchApplication.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Common;

namespace PeopleSearchApplication.DAL
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext() : base("name=EmployeeDB")
        {
        }
        public EmployeeContext (DbConnection connection)
        : base(connection, true)
        {
        }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

}