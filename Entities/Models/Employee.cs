using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Employee
    {
        [Column("EmployeeId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Ime zaposlenog je neophodno polje.")]
        [MaxLength(30, ErrorMessage = "Maksimalan broj karaktera je 30.")]
        public string? Name { get; set; }
        [Required(ErrorMessage ="Godine su neophodno polje.")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Pozicija zaposlenog je neophodno polje.")]
        [MaxLength(20, ErrorMessage = "Maksimalan broj karaktera je 20.")]
        public string? Position { get; set; }
        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
