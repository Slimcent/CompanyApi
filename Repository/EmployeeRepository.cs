﻿using Contract;
using Entities.Models;
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

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, bool trackChanges) => FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
           .OrderBy(e => e.Name);

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
