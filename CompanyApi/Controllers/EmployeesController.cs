using AutoMapper;
using CompanyApi.ActionFilters;
using Contract;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public async Task<IActionResult> GetEmployeesForCompany(Guid companyId, [FromQuery] EmployeeParameters employeeParameters)
        {
            if (!employeeParameters.ValidAgeRange)
                return BadRequest("Max age can't be less than min age.");

            var company = await _unitOfWork.Company.GetCompanyAsync(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }

            var employeesFromDb = await _unitOfWork.Employee.GetEmployeesAsync(companyId, employeeParameters, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(employeesFromDb.PageData));

            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);
            return Ok(employeesDto);
        }

        [HttpGet("{id}", Name = "EmployeeById")]
        public async Task<IActionResult> GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var company = await _unitOfWork.Company.GetCompanyAsync(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }
            var employeeDb = await _unitOfWork.Employee.GetEmployeeAsync(companyId, id, trackChanges:false);
            if (employeeDb == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var employee = _mapper.Map<EmployeeDto>(employeeDb);
            return Ok(employee);
        }

        [HttpPost]
        public async Task <IActionResult> CreateEmployeeForCompany(Guid companyId, [FromBody] PostEmployeeDto employee)
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

            var company = await _unitOfWork.Company.GetCompanyAsync(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }
            var employeeEntity = _mapper.Map<Employee>(employee);

            _unitOfWork.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            await _unitOfWork.SaveAsync();

            //return Ok();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return CreatedAtRoute("EmployeeById", new { companyId, id = employeeToReturn.Id }, employeeToReturn);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEmployeeCompanyExists))]
        public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid id)
        {
            //var company = _unitOfWork.Company.GetCompanyAsync(companyId, trackChanges: false);
            //if (company == null)
            //{
            //    _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
            //    return NotFound();
            //}

            //var employeeForCompany = await _unitOfWork.Employee.GetEmployeeAsync(companyId, id, trackChanges: false);
            //if (employeeForCompany == null)
            //{
            //    _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
            //    return NotFound();
            //}

            var employeeForCompany = HttpContext.Items["employee"] as Employee;

            _unitOfWork.Employee.DeleteEmployee(employeeForCompany);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFiltering))]
        [ServiceFilter(typeof(ValidateEmployeeCompanyExists))]
        public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid id, [FromBody] EmployeeUpdateDto employee)
        {
            //var company = _unitOfWork.Company.GetCompanyAsync(companyId, trackChanges: false);
            //if (company == null)
            //{
            //    _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
            //    return NotFound();
            //}
            //var employeeEntity = await _unitOfWork.Employee.GetEmployeeAsync(companyId, id, trackChanges: true);
            //if (employeeEntity == null)
            //{
            //    _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
            //    return NotFound();
            //}

            var employeeEntity = HttpContext.Items["employee"] as Employee;

            _mapper.Map(employee, employeeEntity);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
