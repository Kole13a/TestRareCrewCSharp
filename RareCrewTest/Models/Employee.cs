

namespace RareCrewTest.Models
{
    public class Employee
    {
        public string? EmployeeName { get; set; }
        public DateTime EndTimeUtc { get; set; }
        public DateTime StarTimeUtc { get; set; }


        public double Duration
        {
            get
            {
                if (EndTimeUtc > StarTimeUtc)
                {
                    double duration = (EndTimeUtc - StarTimeUtc).TotalHours;
                    return duration > 0 ? duration : 0;
                }
                return 0;
            }
        }

        public Employee(string employeeName)
        {
            EmployeeName = string.IsNullOrEmpty(employeeName) ? "Unknown" : employeeName;
        }
    }
}
