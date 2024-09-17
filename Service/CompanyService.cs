using Contracts;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObject;

namespace Service
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public CompanyService(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges) {
            try
            {
                var companies = _repository.Company.GetAllCompanies(trackChanges);
                var companyDto = companies.Select(c => new CompanyDto(c.Id, c.Name ?? "", string.Join('-', c.Address, c.Country))).ToList();
                return companyDto;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Neki je problem na {nameof(GetAllCompanies)} service metodi {ex}.");
                throw;
            }
        }

    }
}
