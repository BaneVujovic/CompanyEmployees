using CompanyEmployees.Presentation.ActionFilters;
using CompanyEmployees.Presentation.Extensions;
using CompanyEmployees.Presentation.ModelBinders;
using Entities.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObject;

namespace CompanyEmployees.Presentation.Controllers
{
    //ApiVersion ne radi ako ne postoji atribut [ApiControler]
    //Mozemo zakomentarisati ApiVersioning jer smo ovu funkcionalnost prosirili u ServiceExtensions klasi
    //[ApiVersion("1.0")]
    //https://localhost:7154/api/companies
    [Route("api/companies")]
    //Da bi radili nasi (custom) exceptions-i moramo zakomentarisati [ApiController] atribut ->
    //-> koji se okida mnogo ranije nego sto se izvrse neki djelovi koda
    [ApiController]
    [ResponseCache(CacheProfileName = "120SecondsDuration")]
    public class CompaniesController : ApiControllerBase
    {
        private readonly IServiceManager _service;

        public CompaniesController(IServiceManager service) => _service = service;

        [HttpGet]
        [Authorize(Roles ="Manager")]
        public async Task<IActionResult> GetCompanies()
        {
            //throw new Exception("Izuzetak, testiranje!");
            //var companies = await _service.CompanyService.GetAllCompaniesAsync(trackChanges: false);

            var baseResult = _service.CompanyService.GetAllCompanies(trackChanges: false);
            //var companies = ((ApiOkResponse<IEnumerable<CompanyDto>>)baseResult).Result;
            var companies = baseResult.GetResult<IEnumerable<CompanyDto>>();


            return Ok(companies);
        }

        [HttpGet("{id:guid}", Name ="CompanyById")]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var baseResult = _service.CompanyService.GetCompany(id, trackChanges: false);
            if (!baseResult.Success)
                return ProcessError(baseResult);
            //var company = await _service.CompanyService.GetCompanyAsync(id, trackChanges: false);

            //var company = ((ApiOkResponse<CompanyDto>)baseResult).Result;

            var company = baseResult.GetResult<CompanyDto>();

            return Ok(company);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreateDto company)
        {
            //Dodavanjem Action filtera za validaciju, nema potrebe za ovim dijelom koda
            //if (company == null)
            //{
            //    return BadRequest("CompanyForCreateDto je null!");
            //}

            var createdCompany = await _service.CompanyService.CreateCompanyAsync(company);
            return CreatedAtRoute("CompanyById", new
            {
                id = createdCompany.Id
            }, createdCompany);
        }

        [HttpGet("collection/({ids})", Name = "CompanyCollection")]
        public async Task<IActionResult> GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            var companies = await _service.CompanyService.GetByIdsAsync(ids, trackChanges: false);
            return Ok(companies);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateCompanyCollection([FromBody]IEnumerable<CompanyForCreateDto> companyCollection)
        {

            var result = await _service.CompanyService.CreateCompanyCollectionAsync(companyCollection);
            return CreatedAtRoute("CompanyCollection", new { result.ids }, result.companies);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCompany(Guid companyId)
        {
            await _service.CompanyService.DeleteCompanyAsync(companyId, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody]CompanyForUpdateDto company)
        {
            //Dodavanjem Action filtera za validaciju, nema potrebe za ovim dijelom koda
            //if (company == null)
            //    return BadRequest("Objekat CompanyForUpdateDto je Null!");

            await _service.CompanyService.UpdateCompanyAsync(id, company, trackChanges: true);

            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetCompaniesOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }
    }
}
