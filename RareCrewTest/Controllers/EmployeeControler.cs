using Microsoft.AspNetCore.Mvc;
using RareCrewTest.Models;
using RareCrewTest.Services;
using System.Linq;
using System.Threading.Tasks;

namespace RareCrewTest.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {

            var employees = await _employeeService.GetEmployeesAsync();

            var employeeSummaries = EmployeeTimeCalculator.GetEmployeeSummaries(employees);

            return View(employeeSummaries);
        }
    }
}
