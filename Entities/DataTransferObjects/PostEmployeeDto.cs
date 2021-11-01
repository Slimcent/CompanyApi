namespace Entities.DataTransferObjects
{
    public class PostEmployeeDto : EmployeeManipulationDto

    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
    }
}
