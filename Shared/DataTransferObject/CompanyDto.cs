namespace Shared.DataTransferObject
{
    [Serializable]
    public record CompanyDto(Guid Id, string Name, string FullAddress);
    //public record CompanyForCreateDto(string Name, string Address, string Country);
    public record CompanyForCreateDto(string Name, string Address, string Country, IEnumerable<EmployeeForCreationDto> Employees);

    public record CompanyForUpdateDto(string Name, string Address, string Country, IEnumerable<EmployeeForCreationDto> Employees);
}
