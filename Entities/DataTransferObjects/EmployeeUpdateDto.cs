using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class EmployeeUpdateDto : EmployeeManipulationDto

    {
        [Required(ErrorMessage = "Employee name cannot be empty")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }

        [Range(18, int.MaxValue, ErrorMessage = "Age is required and it can't be lower than 18")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Position cannot be empty")]
        [MaxLength(20, ErrorMessage = "Length cannot exceed 20 characters")]
        public string Position { get; set; }
    }
}
