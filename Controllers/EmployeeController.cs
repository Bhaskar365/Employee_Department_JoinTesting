using JoinTesting.Interface;
using JoinTesting.Models;
using Microsoft.AspNetCore.Mvc;

namespace JoinTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //testing text for commit test
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        public IActionResult GetAllEmployees() 
        {
            var employees = _employeeRepository.GetAllEmployees();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (employees == null)
                return NotFound();

            return Ok(employees);
        }

        [HttpGet("{empID}")]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployee(int id) 
        {
            if (!_employeeRepository.EmployeeExists(id))
                return NotFound();

            var employee = _employeeRepository.GetEmployee(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEmployee([FromBody] Employee emp, [FromQuery] Department department) 
        {
            var departmentValue = 
        }
    }
}
