namespace Shared.DataTransferObject
{
    [Serializable]
    public record CompanyDto(Guid Id, string Name, string FullAddress);
}
