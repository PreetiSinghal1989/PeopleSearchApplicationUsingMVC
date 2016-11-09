using PeopleSearchApplication.Interfaces;
using PeopleSearchApplication.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace PeopleSearchApplication.DAL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private EmployeeContext db = new EmployeeContext();

        public IEnumerable<Employee> GetAllEmployee()
        {
            var employees = from s in db.Employees
                           select s;
            return employees.ToList();
        }

        public Employee GetEmployeeByID(int? id)
        {
            Employee employee = db.Employees.Find(id);
            return employee;
        }


        // Retrieve image from DB by employee id
        public byte[] GetImageFromDataBase(int Id)
        {
            var q = from temp in db.Employees where temp.EmployeeId == Id select temp.Image;
            byte[] cover = q.First();
            return cover;
        }

        public void CreateEmployee(Employee employeeToCreate)
        {
            db.Employees.Add(employeeToCreate);
            db.SaveChanges();
        }

        public void EditEmployee(Employee employee)
        {
            db.Entry(employee).State = EntityState.Modified;
            if (employee.Image == null)
                employee.Image = GetImageFromDataBase(employee.EmployeeId);
            db.SaveChanges();
        }

        public int SaveChanges()
        {
            return db.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            var employeeToDelete = GetEmployeeByID(id);
            db.Employees.Remove(employeeToDelete);
            db.SaveChanges();
        }
    }
}