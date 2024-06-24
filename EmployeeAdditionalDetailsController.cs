using Microsoft.AspNetCore.Mvc;

using YourNamespace.Models;
using AutoMapper;
using Employee_Management_System.Data;
using Employee_Management_System.Models;

namespace Employee_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAdditionalDetailsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public EmployeeAdditionalDetailsController(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }


       [HttpGet("MakeGetRequest")]
        public async Task<IActionResult> MakeGetRequest()
        {
            var employeeDetails = await _employeeAdditionalDetailsService.GetAllEmployeeAdditionalDetailsAsync();
            return Ok(employeeDetails);
        }

        // MakePostRequest API with EmployeeFilter
        [HttpPost("MakePostRequest")]
        [ServiceFilter(typeof(EmployeeFilter))]
        public async Task<IActionResult> MakePostRequest([FromBody] EmployeeAdditionalDetails employeeDetails)
        {
            if (employeeDetails == null)
            {
                return BadRequest("Employee details are null.");
            }







        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeAdditionalDetails>>> GetAll() => await _context.EmployeeAdditionalDetails.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeAdditionalDetails>> GetById(int id)
        {
            var employee = await _context.EmployeeAdditionalDetails.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeAdditionalDetails>> Create(EmployeeAdditionalDetails employee)
        {
            _context.EmployeeAdditionalDetails.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EmployeeAdditionalDetails employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.EmployeeAdditionalDetails.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.EmployeeAdditionalDetails.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.EmployeeAdditionalDetails.Any(e => e.Id == id);
        }

    }
}
