using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, bool trackChanges) =>
            await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
            .OrderBy(c => c.Name).ToListAsync();

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeId, bool trackChanges) =>
            await FindByCondition(c => c.CompanyId.Equals(companyId) && c.Id.Equals(employeId), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }
    }
}
