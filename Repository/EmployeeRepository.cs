using Contract;
using Entities.Models;
using Entities.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepository : Repositories<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges)
        {
            var employees = await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
                 .FilterEmployees(employeeParameters.MinAge, employeeParameters.MaxAge)
                 .Search(employeeParameters.SearchTerm)
                 .Sort(employeeParameters.OrderBy)
                    .OrderBy(e => e.Name)
                    .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
                    .Take(employeeParameters.PageSize)
                    .ToListAsync();

            var count = await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges
            ).CountAsync();
            return new PagedList<Employee>(employees, employeeParameters.PageNumber, employeeParameters.PageSize, count);
        }

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges) => FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefault();

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Add(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }
    }
}
