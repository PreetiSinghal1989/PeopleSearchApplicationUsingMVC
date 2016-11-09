using PeopleSearchApplication.Interfaces;
using PeopleSearchApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PeopleSearchApplicationTest.Repository
{
    class FakeEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _db = new List<Employee>();

        public Exception ExceptionToThrow { get; set; }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _db.ToList();
        }

        public Employee GetEmployeeByID(int? id)
        {
            return _db.FirstOrDefault(d => d.EmployeeId == id);
        }

        public void CreateEmployee(Employee employeeToCreate)
        {
            if (ExceptionToThrow != null)
                throw ExceptionToThrow;

            _db.Add(employeeToCreate);
        }

        public void EditEmployee(Employee employeeToUpdate)
        {
            foreach (Employee employee in _db)
            {
                if (employee.EmployeeId == employeeToUpdate.EmployeeId)
                {
                    _db.Remove(employee);
                    _db.Add(employeeToUpdate);
                    break;
                }
            }
        }

        public int SaveChanges()
        {
            return 1;
        }

        public void DeleteEmployee(int id)
        {
            _db.Remove(GetEmployeeByID(id));
        }

        public byte[] GetImageFromDataBase(int Id)
        {
            var q = from temp in _db where temp.EmployeeId == Id select temp.Image;
            byte[] cover = q.First();
            return cover;
        }

    }
}

