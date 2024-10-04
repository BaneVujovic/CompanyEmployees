using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObject;

namespace Service
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompanyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        private async Task<Company> GetCompanyAndCheckIfItExists(Guid companyId, bool trackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            return company;
        }

        public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(bool trackChanges) {
            var companies = await _repository.Company.GetAllCompaniesAsync(trackChanges);
            //var companyDto = companies.Select(c => new CompanyDto(c.Id, c.Name ?? "", string.Join('-', c.Address, c.Country))).ToList();
            var companyDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return companyDto;
        }

        public async Task<CompanyDto> GetCompanyAsync(Guid id, bool trackChanges)
        {
            //Ovaj dio koda smo zanijenili sa privatnom metodom ispod konstruktora
            //var company = await _repository.Company.GetCompanyAsync(id, trackChanges);
            ////provjeriti da je li je Company null
            //if (company == null)
            //    throw new CompanyNotFoundException(id);

            var company = await GetCompanyAndCheckIfItExists(id, trackChanges);

            var companyDto = _mapper.Map<CompanyDto>(company);

            return companyDto;
        }

        public async Task<CompanyDto> CreateCompanyAsync(CompanyForCreateDto company)
        {
            var companyEntity = _mapper.Map<Company>(company);
            _repository.Company.CreateCompany(companyEntity);
            await _repository.SaveAsync();

            var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);

            return companyToReturn;
        }

        public async Task<IEnumerable<CompanyDto>> GetByIdsAsync(IEnumerable<Guid> companyId, bool trackChanges) {
            if (companyId == null)
                throw new IDParametarsBadRequestException();

            var companyEntity = await _repository.Company.GetByIdsAsync(companyId, trackChanges);
            if (companyId.Count() != companyEntity.Count())
                throw new CollectionByIdsBadRequestsException();

            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntity);
            return companiesToReturn;
        }

        public async Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollectionAsync(IEnumerable<CompanyForCreateDto> companyCollection)
        {

            if (companyCollection is null)
                throw new CompanyCollectionBadRequest();

            var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);

            foreach (var company in companyEntities)
            {
                _repository.Company.CreateCompany(company);
            }
            await _repository.SaveAsync();

            var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
            var ids = string.Join(",", companyCollectionToReturn.Select(c => c.Id));

            return (companies: companyCollectionToReturn, ids: ids);
        }

        public async Task DeleteCompanyAsync(Guid companyId, bool trackChanges)
        {
            //zamijenjeno privatnom metodom
            //var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
            //if (company == null)
            //    throw new CompanyNotFoundException(companyId);

            var company = await GetCompanyAndCheckIfItExists(companyId, trackChanges);

            _repository.Company.DeleteCompany(company);
            await _repository.SaveAsync();
        }

        public async Task UpdateCompanyAsync(Guid companyId, CompanyForUpdateDto companyForUpdate, bool trackChanges)
        {
            //var companyEntity = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
            //if(companyEntity == null)
            //    throw new CompanyNotFoundException(companyId);

            var companyEntity = await GetCompanyAndCheckIfItExists(companyId, trackChanges);

            _mapper.Map(companyForUpdate, companyEntity);
            await _repository.SaveAsync();
        }
    }
}
