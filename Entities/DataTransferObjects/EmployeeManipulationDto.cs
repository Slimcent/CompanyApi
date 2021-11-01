using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public abstract class EmployeeManipulationDto
    {
        [Required(ErrorMessage = "Employee name cannot be empty")]
        [MaxLength(30, ErrorMessage = "Length cannot Exceed 30 characters")]
        public string Name { get; set; }

        [Range(18, int.MaxValue, ErrorMessage = "Age is required and must be above 18")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Position is a required field.")]
        [MaxLength(20, ErrorMessage = "Length cannot Exceed 20 characters")]
        public string Position { get; set; }

    }
}
