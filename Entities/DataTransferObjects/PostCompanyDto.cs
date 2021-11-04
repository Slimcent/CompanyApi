using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class PostCompanyDto
    {
        [Required(ErrorMessage = "Company name cannot be empty")]
        [MaxLength(50, ErrorMessage = "length cannot be more than 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Company address cannt be empty")]
        [MaxLength(60, ErrorMessage = "length cannot be more than 60 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Company Country cannot be empty")]
        [MaxLength(30, ErrorMessage = "Length cannot be nore than 30 characters")]
        public string Country { get; set; }
    }
}
