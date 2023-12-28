﻿using JoinTesting.Interface;
using JoinTesting.Models;
using Microsoft.AspNetCore.Mvc;

namespace JoinTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Department>))]
        public IActionResult GetAllDepartments()
        {
            var department = _departmentRepository.GetAllDepartments();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (department == null)
                return NotFound();

            return Ok(department);
        }

        [HttpGet("{depId}")]
        [ProducesResponseType(200, Type = typeof(Department))]
        [ProducesResponseType(400)]
        public IActionResult GetDepartment(int departmentId)
        {
            if (_departmentRepository.DepartmentExists(departmentId))
                return NotFound();

            var department = _departmentRepository.GetDepartment(departmentId);

            if (department == null)
                return NotFound();

            return Ok(department);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDepartment([FromBody] Department department)
        {
            if (department == null)
                return BadRequest(ModelState);

            var existingDepartment = _departmentRepository.GetAllDepartments().Where(d => d.DepartmentName == department.DepartmentName || d.DepartmentId == department.DepartmentId).FirstOrDefault();

            if (existingDepartment != null)
            {
                ModelState.AddModelError("", "Department name or id in use");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_departmentRepository.CreateDepartment(department))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{depID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDepartment(int depID, [FromBody] Department dep) 
        {
            if (dep == null)
                return BadRequest(ModelState);

            if (depID != dep.Id)
                return BadRequest(ModelState);

            if (!_departmentRepository.DepartmentExists(depID))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_departmentRepository.UpdateDepartment(dep)) 
            {
                ModelState.AddModelError("","Something went wrong while updating department");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{depID}")]
        public IActionResult DeleteDepartment(int depID) 
        {
            if (!_departmentRepository.DepartmentExists(depID))
                return NotFound();

            var departmentToDelete = _departmentRepository.GetDepartment(depID);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_departmentRepository.DeleteDepartment(departmentToDelete)) 
            {
                ModelState.AddModelError("","Something went wrong while deleting departments");
            }
            return NoContent();
        }
    }
}
