using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PeopleSearchApplication.Models;

namespace PeopleSearchApplication.DAL
{
    public class EmployeeInitializer: DropCreateDatabaseIfModelChanges<EmployeeContext>
    {
        
        //public override void InitializeDatabase(EmployeeContext context)
        //{
        //    context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction
        //        , string.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));

        //    base.InitializeDatabase(context);
        //}


        protected override void Seed(EmployeeContext context)
        {
            var employees = new List<Employee>
            {
                new Employee {FirstName = "John", LastName = "Smith", Address="1900 E 400 S, Salt Lake City, Utah", Age= 35, Interests="Reading"},
                new Employee {FirstName = "Taylor", LastName = "Swift", Address="1906 E 900 S, Salt Lake City, Utah", Age= 30, Interests="Learning new technologies"},
                new Employee {FirstName = "Shawn", LastName = "Michael", Address="1910 E 400 S, Salt Lake City, Utah", Age= 25, Interests="Surfing"},
                new Employee {FirstName = "Adam", LastName = "Frisbee", Address="1900 E 400 S, Salt Lake City, Utah", Age= 27, Interests="Listening music"},
                new Employee {FirstName = "Chris", LastName = "Densie", Address="1900 E 400 S, Salt Lake City, Utah", Age= 29, Interests="Reading"},
                new Employee {FirstName = "Jennifer", LastName = "Hanson", Address="1900 E 400 S, Salt Lake City, Utah", Age= 40, Interests="Dancing"},
                new Employee {FirstName = "Lucy", LastName = "Laputka", Address="1900 E 400 S, Salt Lake City, Utah", Age= 25, Interests="Singing"},
                new Employee {FirstName = "Mike", LastName = "Boyle", Address="1900 E 400 S, Salt Lake City, Utah", Age= 35, Interests="Reading"},
                new Employee {FirstName = "Boniface", LastName = "Nautwarama", Address="1900 E 400 S, Salt Lake City, Utah", Age= 45, Interests="Acting"},
                new Employee {FirstName = "Ryan", LastName = "Kenneth", Address="1900 E 400 S, Salt Lake City, Utah", Age= 25, Interests="Reading"},
                new Employee {FirstName = "Jeff", LastName = "Philips", Address="1900 E 400 S, Salt Lake City, Utah", Age= 28, Interests="Learning"}

            };

            employees.ForEach(e => context.Employees.Add(e));
            context.SaveChanges();
        }
    }
}