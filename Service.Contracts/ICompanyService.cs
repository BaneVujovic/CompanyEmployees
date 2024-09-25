using Shared.DataTransferObject;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
        CompanyDto GetCompany(Guid companyId, bool trackChanges);
        CompanyDto CreateCompany(CompanyForCreateDto company);
        IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> companyId, bool trackChanges);

        (IEnumerable<CompanyDto> companies, string ids) CreateCompanyCollection(IEnumerable<CompanyForCreateDto> companyCollection);

        void DeleteCompany(Guid companyId, bool trackChanges);
    }
}
