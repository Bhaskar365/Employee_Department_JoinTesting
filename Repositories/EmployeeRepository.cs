using JoinTesting.DbClass;
using JoinTesting.Interface;
using JoinTesting.Models;
using Microsoft.EntityFrameworkCore;

namespace JoinTesting.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        
        public EmployeeRepository(AppDbContext context) 
        {
            _context = context;
        }

        public ICollection<Employee> GetAllEmployees()
        {
            return _context.Tbl_Employee.ToList();
        }

        public Employee GetEmployee(int id)
        {
            return _context.Tbl_Employee.Where(e => e.EmpId == id).FirstOrDefault();
        }

        public bool EmployeeExists(int id)
        {
            return _context.Tbl_Employee.Any(e => e.EmpId == id);
        }

        public bool CreateEmployee(Employee emp)
        {
            var employeesWithDepartments = _context.Tbl_Employee.Include(e => e.Department).ToList();
            emp.CreatedOn = DateTime.UtcNow;
            _context.Add(emp);
            return Save();
        }

        public bool UpdateEmployee(Employee emp, int depID)
        { 
            _context.Update(emp);
            return Save();
        }

        public bool DeleteEmployee(Employee emp)
        {
            _context.Remove(emp);
            return Save();
        }

        public bool Save()
        {
            var result = _context.SaveChanges();
            return result > 0 ? true : false;
        }
    }
}
