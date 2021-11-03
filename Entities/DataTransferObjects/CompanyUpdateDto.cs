using System.Collections.Generic;

namespace Entities.DataTransferObjects
{
    public class CompanyUpdateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public IEnumerable<PostEmployeeDto> Employees { get; set; }
    }
}
