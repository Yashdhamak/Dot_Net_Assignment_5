using Microsoft.AspNetCore.Mvc;
using Employee_Management_System.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Employee_Management_System.Data;
using Microsoft.Azure.Cosmos;

namespace Employee_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeBasicDetailsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public EmployeeBasicDetailsController(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeBasicDetails>>> GetAll()
        {
            return await _context.EmployeeBasicDetails.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeBasicDetails>> GetById(string id)
        {
            var employee = await _context.EmployeeBasicDetails.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeBasicDetails>> Create(EmployeeBasicDetails employee)
        {
            _context.EmployeeBasicDetails.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, EmployeeBasicDetails employee)
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
        public async Task<IActionResult> Delete(string id)
        {
            var employee = await _context.EmployeeBasicDetails.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.EmployeeBasicDetails.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(string id)
        {
            return _context.EmployeeBasicDetails.Any(e => e.Id == id);
        }
    }
}
