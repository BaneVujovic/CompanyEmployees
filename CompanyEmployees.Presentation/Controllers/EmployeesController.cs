using CompanyEmployees.Presentation.ActionFilters;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObject;
using Shared.RequestFeatures;
using System.Text.Json;

namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    //[ApiController]
    public class EmployeesController:ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public EmployeesController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        [HttpHead]
        //Paging
        [HttpGet]
        public async Task<IActionResult> GetEmployeesForCompany(Guid companyId, [FromQuery]EmployeeParameters employeeParameters)
        {
            var pageResult = await _serviceManager.EmployeeService.GetEmployeesAsync(companyId, employeeParameters, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pageResult.metaData));
            //var employees = await _serviceManager.EmployeeService.GetEmployeesAsync(companyId, employeeParameters, trackChanges: false);

            return Ok(pageResult.employees);
        }

        [HttpGet("id:guid", Name = "GetEmployeeForCompany")]
        public async Task<IActionResult> GetEmployeeForCompany(Guid companyId, Guid employeeId)
        {
            var employee = await _serviceManager.EmployeeService.GetEmployeeAsync(companyId, employeeId, trackChanges: false);

            return Ok(employee);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateEmployeeForCompany
        (Guid companyId, [FromBody] EmployeeForCreationDto employee)
        {
            var employeeToReturn = await _serviceManager.EmployeeService.CreateEmployeeForCompanyAsync(companyId, employee,
                trackChanges: false);

            return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeeToReturn.Id },
                employeeToReturn);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid employeeId)
        {
            await _serviceManager.EmployeeService.DeleteEmployeeForCompanyAsync(companyId, employeeId, trackChanges: false);

            return NoContent();
        }

        [HttpPut("{employeeId:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid employeeId, [FromBody]EmployeeForUpdateDto employee)
        {
            //if (employee is null)
            //    return BadRequest("Objekat EmployeeForUpdateDto je Null!");

            //if (!ModelState.IsValid)
            //    return UnprocessableEntity(ModelState);

            await _serviceManager.EmployeeService.UpdateEmployeeForCompanyAsync(companyId, employeeId, employee, compTrackChanges: false, empTrackChanges: true);

            return NoContent();
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(Guid companyId, Guid id, [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc)
        {
            if (patchDoc is null)
                return BadRequest("Objekat patchDoc poslat sa klijenta je Null.");

            var result = await _serviceManager.EmployeeService.GetEmployeeForPatchAsync(companyId, id, compTrackChanges: false, empTrackChanges: true);

            patchDoc.ApplyTo(result.employeeToPatch, ModelState);

            TryValidateModel(result.employeeToPatch);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _serviceManager.EmployeeService.SaveChangesForPatchAsync(result.employeeToPatch, result.employeeEntity);

            return NoContent();
        }
    }
}
