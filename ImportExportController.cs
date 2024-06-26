using Microsoft.AspNetCore.Mvc;
using Employee_Management_System.Data;
using Employee_Management_System.Models;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportExportController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ImportExportController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("ExportToExcel")]
        public async Task<IActionResult> ExportToExcel()
        {
            var employees = await _context.EmployeeBasicDetails.Include(e => e.Address).ToListAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Employees");

                // Headers
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Salutory";
                worksheet.Cells[1, 3].Value = "First Name";
                worksheet.Cells[1, 4].Value = "Middle Name";
                worksheet.Cells[1, 5].Value = "Last Name";
                worksheet.Cells[1, 6].Value = "Nick Name";
                worksheet.Cells[1, 7].Value = "Email";
                worksheet.Cells[1, 8].Value = "Mobile";
                worksheet.Cells[1, 9].Value = "Employee ID";
                worksheet.Cells[1, 10].Value = "Role";
                worksheet.Cells[1, 11].Value = "Reporting Manager UID";
                worksheet.Cells[1, 12].Value = "Reporting Manager Name";
                worksheet.Cells[1, 13].Value = "Address Line 1";
                worksheet.Cells[1, 14].Value = "Address Line 2";
                worksheet.Cells[1, 15].Value = "City";
                worksheet.Cells[1, 16].Value = "State";
                worksheet.Cells[1, 17].Value = "Country";
                worksheet.Cells[1, 18].Value = "Postal Code";
                worksheet.Cells[1, 19].Value = "Date Of Joining";

                // Data
                for (int i = 0; i < employees.Count; i++)
                {
                    var row = i + 2;
                    worksheet.Cells[row, 1].Value = employees[i].Id;
                    worksheet.Cells[row, 2].Value = employees[i].Salutory;
                    worksheet.Cells[row, 3].Value = employees[i].FirstName;
                    worksheet.Cells[row, 4].Value = employees[i].MiddleName;
                    worksheet.Cells[row, 5].Value = employees[i].LastName;
                    worksheet.Cells[row, 6].Value = employees[i].NickName;
                    worksheet.Cells[row, 7].Value = employees[i].Email;
                    worksheet.Cells[row, 8].Value = employees[i].Mobile;
                    worksheet.Cells[row, 9].Value = employees[i].EmployeeID;
                    worksheet.Cells[row, 10].Value = employees[i].Role;
                    worksheet.Cells[row, 11].Value = employees[i].ReportingManagerUId;
                    worksheet.Cells[row, 12].Value = employees[i].ReportingManagerName;
                    worksheet.Cells[row, 13].Value = employees[i].Address.Line1;
                    worksheet.Cells[row, 14].Value = employees[i].Address.Line2;
                    worksheet.Cells[row, 15].Value = employees[i].Address.City;
                    worksheet.Cells[row, 16].Value = employees[i].Address.State;
                    worksheet.Cells[row, 17].Value = employees[i].Address.Country;
                    worksheet.Cells[row, 18].Value = employees[i].Address.PostalCode;
                    worksheet.Cells[row, 19].Value = employees[i].DateOfJoining;
                }

                var stream = new MemoryStream(package.GetAsByteArray());

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employees.xlsx");
            }
        }
    }
}
