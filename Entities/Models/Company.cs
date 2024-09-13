using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Company
    {
        [Column("CompanyId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Ime kompanije je neophodno polje.")]
        [MaxLength(60, ErrorMessage = "Maksimalan broj karaktera je 60.")]
        public string? Name { get; set; }
        [Required(ErrorMessage ="Adresa komapnije je neophodno polje.")]
        [MaxLength(60, ErrorMessage ="Maksimalan broj karaktera je 60.")]
        public string? Address { get; set; }
        public string? Country { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
