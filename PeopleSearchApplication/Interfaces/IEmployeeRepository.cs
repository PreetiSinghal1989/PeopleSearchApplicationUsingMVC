using PeopleSearchApplication.Models;
using System.Collections.Generic;

namespace PeopleSearchApplication.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployee();
        Employee GetEmployeeByID(int? id);
        void CreateEmployee(Employee employeeToCreate);
        void EditEmployee(Employee employee);
        void DeleteEmployee(int id);
        int SaveChanges();
        byte[] GetImageFromDataBase(int id);
    }
}