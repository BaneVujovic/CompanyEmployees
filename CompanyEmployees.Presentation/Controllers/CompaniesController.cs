using CompanyEmployees.Presentation.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObject;

namespace CompanyEmployees.Presentation.Controllers
{
    //https://localhost:7154/api/companies
    [Route("api/companies")]
    //Da bi radili nasi (custom) exceptions-i moramo zakomentarisati [ApiController] atribut ->
    //-> koji se okida mnogo ranije nego sto se izvrse neki djelovi koda
    //[ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CompaniesController(IServiceManager service) => _service = service;

        [HttpGet]
        public IActionResult GetCompanies()
        {
            //throw new Exception("Izuzetak, testiranje!");
            var companies = _service.CompanyService.GetAllCompanies(trackChanges: false);
            return Ok(companies);
        }

        [HttpGet("{id:guid}", Name ="CompanyById")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _service.CompanyService.GetCompany(id, trackChanges: false);
            return Ok(company);
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyForCreateDto company)
        {
            if (company == null)
            {
                return BadRequest("CompanyForCreateDto je null!");
            }

            var createdCompany = _service.CompanyService.CreateCompany(company);
            return CreatedAtRoute("CompanyById", new
            {
                id = createdCompany.Id
            }, createdCompany);
        }

        [HttpGet("collection/({ids})", Name = "CompanyCollection")]
        public IActionResult GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            var companies = _service.CompanyService.GetByIds(ids, trackChanges: false);
            return Ok(companies);
        }

        [HttpPost("collection")]
        public IActionResult CreateCompanyCollection([FromBody]IEnumerable<CompanyForCreateDto> companyCollection)
        {

            var result = _service.CompanyService.CreateCompanyCollection(companyCollection);
            return CreatedAtRoute("CompanyCollection", new { result.ids }, result.companies);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteCompany(Guid companyId)
        {
            _service.CompanyService.DeleteCompany(companyId, trackChanges: false);
            return NoContent();
        }
    }
}
