
namespace RareCrewTest.Models
{
    public class EmployeeSummary
    {
        public string? EmployeeName { get; set; }
        public double TotalHours { get; set; }
    }

    public class EmployeeTimeCalculator
    {
        public static IEnumerable<EmployeeSummary> GetEmployeeSummaries(IEnumerable<Employee> employees)
        {
            return employees
                .GroupBy(e => e.EmployeeName)
                .Select(g => new EmployeeSummary
                {
                    EmployeeName = g.Key,
                    TotalHours = Math.Ceiling(g.Sum(e => e.Duration))
                })
                .ToList();
        }
    }
}
