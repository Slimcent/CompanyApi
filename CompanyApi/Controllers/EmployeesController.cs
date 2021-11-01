using AutoMapper;
using Contract;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyApi.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public EmployeesController(IUnitOfWork unitOfWork, ILoggerManager logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetEmployeesForCompany(Guid companyId)
        {
            var company = _unitOfWork.Company.GetCompany(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }

            var employeesFromDb = _unitOfWork.Employee.GetEmployees(companyId, trackChanges: false);

            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);
            return Ok(employeesDto);
        }

        [HttpGet("{id}", Name = "EmployeeById")]
        public IActionResult GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var company = _unitOfWork.Company.GetCompany(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
            return NotFound();
            }
            var employeeDb = _unitOfWork.Employee.GetEmployee(companyId, id, trackChanges:false);
            if (employeeDb == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var employee = _mapper.Map<EmployeeDto>(employeeDb);
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult CreateEmployeeForCompany(Guid companyId, [FromBody] PostEmployeeDto employee)
        {
            if (employee == null)
            {
                _logger.LogError("Employee object sent from client is null.");
                return BadRequest("Employee object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the Employee object");
                return UnprocessableEntity(ModelState);
            }

            var company = _unitOfWork.Company.GetCompany(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }
            var employeeEntity = _mapper.Map<Employee>(employee);

            _unitOfWork.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            _unitOfWork.Save();

            //return Ok();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return CreatedAtRoute("GetEmployeeForCompany",
                new { companyId, id = employeeToReturn.Id }, employeeToReturn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployeeForCompany(Guid companyId, Guid id)
        {
            var company = _unitOfWork.Company.GetCompany(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
            return NotFound();
            }
            var employeeForCompany = _unitOfWork.Employee.GetEmployee(companyId, id,
           trackChanges: false);
            if (employeeForCompany == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _unitOfWork.Employee.DeleteEmployee(employeeForCompany);
            _unitOfWork.Save();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployeeForCompany(Guid companyId, Guid id, [FromBody] EmployeeUpdateDto employee)
        {
            if (employee == null)
            {
                _logger.LogError("Employee object sent from client is null.");
                return BadRequest("Employee object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the EmployeeForUpdateDto object");
                return UnprocessableEntity(ModelState);
            }
            var company = _unitOfWork.Company.GetCompany(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
            return NotFound();
            }
            var employeeEntity = _unitOfWork.Employee.GetEmployee(companyId, id, trackChanges:
           true);
            if (employeeEntity == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(employee, employeeEntity);
            _unitOfWork.Save();
            return NoContent();
        }

    }
}
