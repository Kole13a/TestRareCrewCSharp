
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.ImageSharp;
using RareCrewTest.Models;

namespace RareCrewTest.Services
{
    public class EmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Employee>>("https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==");
            return response;
        }

        public async Task GenerateEmployeeWorkDistributionChartAsync()
        {
            var employees = await GetEmployeesAsync();
            var employeeSummaries = EmployeeTimeCalculator.GetEmployeeSummaries(employees);

            double totalHours = employeeSummaries.Sum(e => e.TotalHours);

            var plotModel = new PlotModel { Title = "Employee Work Time Pie Chart" };
            var pieSeries = new PieSeries { StrokeThickness = 2.0, AngleSpan = 360, StartAngle = 0 };

            int sliceIndex = 0;
            foreach (var summary in employeeSummaries)
            {
                double percentage = (summary.TotalHours / totalHours) * 100;
                pieSeries.Slices.Add(new PieSlice(summary.EmployeeName ?? "Unknown", percentage)
                {
                    IsExploded = true,
                    Fill = GetColorForIndex(sliceIndex)
                });

                sliceIndex++;
            }

            plotModel.Series.Add(pieSeries);

            int width = 600;
            int height = 400;
            double resolution = 96;

            var pngExporter = new PngExporter(width, height, resolution);

            using (var stream = File.Create("wwwroot/images/employee_work_pie_chart.png"))
            {
                pngExporter.Export(plotModel, stream);
            }
        }

        private OxyColor GetColorForIndex(int index)
        {
            double hue = (index * 60) % 360; 
            return OxyColor.FromRgb(
                (byte)(Math.Sin(hue * Math.PI / 180) * 127 + 128),
                (byte)(Math.Sin((hue + 120) * Math.PI / 180) * 127 + 128),
                (byte)(Math.Sin((hue + 240) * Math.PI / 180) * 127 + 128)
            );
        }
    }
}
