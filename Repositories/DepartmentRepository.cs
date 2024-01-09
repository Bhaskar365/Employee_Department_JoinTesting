using JoinTesting.DbClass;
using JoinTesting.Interface;
using JoinTesting.Models;

namespace JoinTesting.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool CreateDepartment(Department dep)
        {
            var department = _context.Tbl_Department.Add(dep);
            return Save();
        }

        public bool DeleteDepartment(Department dep)
        {
            _context.Remove(dep);
            return Save();
        }

        public bool DepartmentExists(int depId)
        {
            return _context.Tbl_Department.Any(p => p.DepartmentId == depId);
        }

        public ICollection<Department> GetAllDepartments()
        {
            return _context.Tbl_Department.OrderBy(p => p.DepartmentId).ToList();
        }

        public Department GetDepartment(int id)
        {
            return _context.Tbl_Department.Where(p => p.DepartmentId == id).FirstOrDefault();
        }


        public bool UpdateDepartment(Department dep)
        {
            _context.Update(dep);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
