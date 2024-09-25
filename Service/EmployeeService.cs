using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company == null)
                throw new CompanyNotFoundException(companyId);

            var employeeFromDb = _repository.Employee.GetEmployees(companyId, trackChanges);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeeFromDb);

            return employeesDto;
        }

        public EmployeeDto GetEmployee(Guid companyId, Guid employeeId, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if(company == null)
                throw new CompanyNotFoundException(companyId);
            var employeeDb = _repository.Employee.GetEmployee(companyId, employeeId, trackChanges);
            if(employeeDb==null)
                throw new EmployeeNotFoundException(employeeId);
            var employeDto = _mapper.Map<EmployeeDto>(employeeDb);

            return employeDto;
        }

        public EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDto employeeForCreation, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if(company==null)
                throw new CompanyNotFoundException(companyId);
            var employeeEntity = _mapper.Map<Employee>(employeeForCreation);
            _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            _repository.Save();

            var employeeToretun = _mapper.Map<EmployeeDto>(employeeEntity);

            return employeeToretun;
        }

        public void DeleteEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company == null)
                throw new CompanyNotFoundException(companyId);

            var employeeForCompany = _repository.Employee.GetEmployee(companyId, employeeId, trackChanges);
            if (employeeForCompany == null)
                throw new EmployeeNotFoundException(employeeId);
            _repository.Employee.DeleteEmployee(employeeForCompany);
            _repository.Save();
        }

        public void UpdateEmployeeForCompany(Guid companyId, Guid employeeId, EmployeeForUpdateDto employee, bool compTrackChanges, bool empTrackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, compTrackChanges);
            if(company == null)
                throw new CompanyNotFoundException(companyId);

            var employeeEntitey = _repository.Employee.GetEmployee(companyId, employeeId, empTrackChanges);
            if (employeeEntitey == null)
                throw new EmployeeNotFoundException(employeeId);

            _mapper.Map(employee, employeeEntitey);
            _repository.Save();
        }
    }
}
