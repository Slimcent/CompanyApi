namespace Entities.Pagination
{
    public class EmployeeParameters : PageBase
    {
        public EmployeeParameters()
        {
            OrderBy = "name"; // For Sorting
        }
        public uint MinAge { get; set; } // Age filter Begins
        public uint MaxAge { get; set; } = int.MaxValue;

        public bool ValidAgeRange => MaxAge > MinAge; // Age Filter Ends

        public string SearchTerm { get; set; } // Serching

    }
}
