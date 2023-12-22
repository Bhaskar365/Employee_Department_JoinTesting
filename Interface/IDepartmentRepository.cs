using JoinTesting.Models;

namespace JoinTesting.Interface
{
    public interface IDepartmentRepository
    {
        ICollection<Department> GetAllDepartments();
        Department GetDepartment(int id);
        bool DepartmentExists(int depId);
        bool CreateDepartment(Department dep);
        bool UpdateDepartment(Department dep);
        bool DeleteDepartment(Department dep);
        bool Save();
    }
}
