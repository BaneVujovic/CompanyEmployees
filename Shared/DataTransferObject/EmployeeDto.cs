using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject
{
    public record EmployeeDto(Guid Id, string Name, int Age, string Position);
    public record EmployeeForCreationDto:EmployeeForManipulation;
    public record EmployeeForUpdateDto:EmployeeForManipulation;

    public abstract record EmployeeForManipulation {
        [Required(ErrorMessage = "Ime zaposlenog je obavezno polje!")]
        [MaxLength(30, ErrorMessage = "Maksimalna duzina Imena je 30 karaktera!")]
        public string? Name { get; init; }

        [Range(18, int.MaxValue, ErrorMessage = "Godine su obavezo polje i ne mogu biti manje od 18!")]
        public int Age { get; init; }

        [Required(ErrorMessage = "Pozicija zaposlenog je obavezno polje!")]
        [MaxLength(20, ErrorMessage = "Maksimalna duzina Pozicije je 20 karaktera!")]
        public string? Position { get; init; }
    }
}
