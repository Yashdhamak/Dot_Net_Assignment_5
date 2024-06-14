using Microsoft.AspNetCore.Mvc;
using YourNamespace.Models;
using OfficeOpenXml;
using AutoMapper;

namespace Employee_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportExportController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public ImportExportController(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            using var package = new ExcelPackage(stream);
            var worksheet = package.Workbook.Worksheets[0];

            var rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++)
            {
                var employee = new EmployeeBasicDetails
                {
                    FirstName = worksheet.Cells[row, 2].Value?.ToString().Trim(),
                    LastName = worksheet.Cells[row, 3].Value?.ToString().Trim(),
                    Email = worksheet.Cells[row, 4].Value?.ToString().Trim(),
                    Mobile = worksheet.Cells[row, 5].Value?.ToString().Trim(),
                    ReportingManagerName = worksheet.Cells[row, 6].Value?.ToString().Trim(),
                    DateOfBirth = worksheet.Cells[row, 7].Value?.ToString().Trim(),
                    DateOfJoining = worksheet.Cells[row, 8].Value?.ToString().Trim(),

                };

                _context.EmployeeBasicDetails.Add(employee);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export(ExcelWorksheet worksheet)
        {
            var employees = await _context.EmployeeBasicDetails.ToListAsync();

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Employees");
            worksheet.Cells[1, 1].Value = "Sr.No";
            worksheet.Cells[1, 2].Value = "First Name";
            worksheet.Cells[1, 3].Value = "Last Name";
            worksheet.Cells[1, 4].Value = "Email";
            worksheet.Cells[1, 5].Value = "Phone No";
            worksheet.Cells[1, 6].Value = "Reporting Manager Name";
            worksheet.Cells[1, 7].Value = "Date of Birth";
            worksheet.Cells[1, 8].Value = "Date of Joining";

            for (int i = 0; i < employees.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = i + 1;
                worksheet.Cells[i + 2, 2].Value = employees[i].FirstName;
                worksheet.Cells[i + 2, 3].Value = employees[i].LastName;
                worksheet.Cells[i + 2, 4].Value = employees[i].Email;
                worksheet.Cells[i + 2, 5].Value = employees[i].Mobile;
                worksheet.Cells[i + 2, 6].Value = employees[i].ReportingManagerName;
                worksheet.Cells[i + 2, 7].Value = employees[i].DateOfBirth.ToShortDateString();
                worksheet.Cells[i + 2, 8].Value = employees[i].DateOfJoining.ToShortDateString();
            }

            var stream = new MemoryStream();
            package.SaveAs(stream);
            stream.Position = 0;
            string excelName = $"EmployeeList-{DateTime.Now:yyyyMMddHHmmssfff}.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

    }
}



