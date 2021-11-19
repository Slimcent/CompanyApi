using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class UserAuthenticationDto
    {
        [Required(ErrorMessage = "User name cannot be empty")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password cannot be empty")]
        public string Password { get; set; }
    }
}
