using AutoMapper;
using Contract;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyApi.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;


        public CompaniesController(IUnitOfWork unitOfWork, ILoggerManager logger, IMapper mapper)

        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;

        }
        [HttpGet]
        public IActionResult GetCompanies()
        {
            var companies = _unitOfWork.Company.GetAllCompanies(trackChanges: false);
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            
            return Ok(companiesDto);

            throw new Exception("Exception");
        }

        [HttpGet("{id}", Name = "CompanyById")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _unitOfWork.Company.GetCompany(id, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var companyDto = _mapper.Map<CompanyDto>(company);
                return Ok(companyDto);
            }
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody]PostCompanyDto company)
        {
            if (company == null)
            {
                _logger.LogError("Company object sent from client is null.");
                return BadRequest("Companyo object is null");
            }
            var companyEntity = _mapper.Map<Company>(company);
            _unitOfWork.Company.AddCompany(companyEntity);
            _unitOfWork.Save();

            var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);
            return CreatedAtRoute("CompanyById", new { id = companyToReturn.Id }, companyToReturn);
        }
    }
}

