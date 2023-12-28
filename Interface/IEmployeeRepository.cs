using JoinTesting.Models;

namespace JoinTesting.Interface
{
    public interface IEmployeeRepository
    {
        ICollection<Employee> GetAllEmployees();
        Employee GetEmployee(int id);
        bool EmployeeExists(int empId);
        bool CreateEmployee(Employee emp);
        bool UpdateEmployee(Employee emp);
        bool DeleteEmployee(Employee emp);
        bool Save();

    }
}