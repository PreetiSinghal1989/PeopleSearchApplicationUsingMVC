using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeopleSearchApplication.Models;
using PeopleSearchApplication.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using PeopleSearchApplicationTest.Repository;
using PeopleSearchApplication.Interfaces;
using System.Web.Routing;

namespace PeopleSearchApplicationTest
{
    [TestClass]
    public class EmployeeControllerTest
    {
        private EmployeeController employeeController;

        [TestInitialize]
        public void Init()
        {
            employeeController = GetEmployeeController(new FakeEmployeeRepository());
            CreateSampleData();
        }

        private static EmployeeController GetEmployeeController(IEmployeeRepository empRepository)
        {
            EmployeeController empController = new EmployeeController(empRepository);
            empController.ControllerContext = new ControllerContext()
            {
                Controller = empController,
                RequestContext = new RequestContext(new MockHttpContext(), new RouteData())
            };
            return empController;
        }

        private void CreateSampleData()
        {
            var employees = new List<Employee>
            {
                new Employee {EmployeeId = 1, FirstName = "John", LastName = "Smith", Address="1900 E 400 S, Salt Lake City, Utah", Age= 35, Interests="Reading"},
                new Employee {EmployeeId = 2, FirstName = "Taylor", LastName = "Swift", Address="1906 E 900 S, Salt Lake City, Utah", Age= 30, Interests="Learning new technologies"},
                new Employee {EmployeeId = 3, FirstName = "Shawn", LastName = "Michael", Address="1910 E 400 S, Salt Lake City, Utah", Age= 25, Interests="Surfing"},
                new Employee {EmployeeId = 4, FirstName = "Adam", LastName = "Frisbee", Address="1900 E 400 S, Salt Lake City, Utah", Age= 27, Interests="Listening music"},
                new Employee {EmployeeId = 5, FirstName = "Chris", LastName = "Densie", Address="1900 E 400 S, Salt Lake City, Utah", Age= 29, Interests="Reading"},
                new Employee {EmployeeId = 6, FirstName = "Jennifer", LastName = "Hanson", Address="1900 E 400 S, Salt Lake City, Utah", Age= 40, Interests="Dancing"},
                new Employee {EmployeeId = 7, FirstName = "Lucy", LastName = "Laputka", Address="1900 E 400 S, Salt Lake City, Utah", Age= 25, Interests="Singing"},
                new Employee {EmployeeId = 8, FirstName = "Mike", LastName = "Boyle", Address="1900 E 400 S, Salt Lake City, Utah", Age= 35, Interests="Reading"},
                new Employee {EmployeeId = 9, FirstName = "Boniface", LastName = "Nautwarama", Address="1900 E 400 S, Salt Lake City, Utah", Age= 45, Interests="Acting"},
                new Employee {EmployeeId = 10, FirstName = "Ryan", LastName = "Kenneth", Address="1900 E 400 S, Salt Lake City, Utah", Age= 25, Interests="Reading"},
                new Employee {EmployeeId = 11, FirstName = "Jeff", LastName = "Philips", Address="1900 E 400 S, Salt Lake City, Utah", Age= 28, Interests="Learning"}

            };

            employees.ForEach(e => employeeController.Create(e, null));
        }

        [TestMethod]
        public void IndexTest()
        {
            ActionResult result = employeeController.Index();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SearchEmployeeTest()
        {
            ActionResult result = employeeController.SearchEmployee(null, "John", null);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateTest()
        {      
            var employee = new Employee {EmployeeId = 12, FirstName = "Preeti", LastName = "Singhal", Address = "1900 E 400 S, Salt Lake City, Utah", Age = 25, Interests = "Reading" };
            var result = employeeController.Create(employee, null);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SearchEmployeeByFullNameTest()
        {
            ActionResult result = employeeController.SearchEmployee(null, "John Smith", null);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SearchEmployeeByLowerCaseNameTest()
        {
            ActionResult result = employeeController.SearchEmployee(null, "john smith", null);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetDetailsViewTest()
        {
            ActionResult result = employeeController.Details(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetEditViewTest()
        {
            ActionResult result = employeeController.Edit(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EditEmployeeTest()
        {
            var employee = new Employee {EmployeeId = 1, FirstName = "Johnston", LastName = "Barter", Address = "1900 E 400 S, Salt Lake City, Utah", Age = 25, Interests = "Reading" };
            ActionResult result = employeeController.Edit(employee, null);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetDeleteViewTest()
        {
            ActionResult result = employeeController.Delete(1);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteConfirmedTest()
        {
            ActionResult result = employeeController.DeleteConfirmed(1);
            Assert.IsNotNull(result);
        }

    }
}
