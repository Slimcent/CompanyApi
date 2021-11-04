using Entities.Models;
using Entities.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contract
{
    public interface IEmployeeRepository
    {

        Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges);
        Task<Employee> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges);
        void CreateEmployeeForCompany(Guid companyId, Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
