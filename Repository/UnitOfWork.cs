using Contract;
using Entities.Models;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private RepositoryContext _repositoryContext;
        private ICompanyRepository _companyRepository; 
        private IEmployeeRepository _employeeRepository;

        public UnitOfWork(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public ICompanyRepository Company 
        {
            get
            {
                if (_companyRepository == null)
                    _companyRepository = new CompanyRepository(_repositoryContext);
                return _companyRepository;
            } 
        }

        public IEmployeeRepository Employee 
        { 
            get 
            { 
                if (_employeeRepository == null) _employeeRepository = new EmployeeRepository(_repositoryContext); 
                return _employeeRepository; 
            } 
        }

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();

    }
}
