using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class CompanyUpdateDto
    {
        [Required(ErrorMessage = "Company name cannot be empty")]
        [MaxLength(50, ErrorMessage = "length cannot be more than 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Company address cannt be empty")]
        [MaxLength(60, ErrorMessage = "length cannot be more than 60 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Position is a required field.")]
        [MaxLength(50, ErrorMessage = "Length cannot be more than 50 characters.")]
        public string Country { get; set; }
        public IEnumerable<PostEmployeeDto> Employees { get; set; }
    }
}
