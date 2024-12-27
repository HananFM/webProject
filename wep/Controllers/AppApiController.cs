using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
using wep.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace wep.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppApiController : ControllerBase
    {
        private readonly ServisContext _context;

        // Constructor with dependency injection
        public AppApiController(ServisContext context)
        {
            _context = context;
        }

        // GET: api/<AppApiController>
        [HttpGet("Employees")]
        public List<Employee> Get()
        {
            var employee = _context.employee.ToList();            
            return employee;
        }

        [HttpGet("ServicesByFees")]
        public List<Servis> getLinq()
        {
            var Servis1 = (from ktp in _context.servis
                             orderby ktp.ServisFee descending
                             select ktp).ToList();

            return Servis1;
        }

        // GET api/<AppApiController>/5
        [HttpGet("Employee/{id}")]
        public ActionResult<Employee> Get(int id)
        {
            var employee = _context.employee.FirstOrDefault(x => x.EmployeeID == id);
            if (employee is null)
            {
                return NoContent();
            }

            return employee;
        }

        // POST api/<AppApiController>
        [HttpPost("AddEmployee")]
        public ActionResult Post([FromBody] Employee e)
        {
            _context.employee.Add(e);
            _context.SaveChanges();
            return Ok(e.EmployeeName + "Adli Employee Eklendi");
        }

        // PUT api/<AppApiController>/5
        [HttpPut("EditEmployee/{id}")]
        public ActionResult Put(int id, [FromBody] Employee e)
        {
            var employee = _context.employee.FirstOrDefault(x => x.EmployeeID == id);
            if (employee is null)
            {
                return NotFound();
            }
            employee.EmployeeName = e.EmployeeName;
            employee.EmployeeSurname = e.EmployeeSurname;
            _context.employee.Update(employee);
            _context.SaveChanges();
            return Ok(e.EmployeeName + "adli Employee Guncellendi");

        }

        // DELETE api/<AppApiController>/5
        [HttpDelete("DeleteEmployee/{id}")]
        public ActionResult Delete(int id)
        {
            var employee = _context.employee.Include(x => x.servis).FirstOrDefault(x => x.EmployeeID == id);
            if (employee is null)
            {
                return NotFound();
            }
            if (employee.servis.Count > 0)
            {
                return NotFound();

            }
            _context.employee.Remove(employee);
            _context.SaveChanges();
            return Ok();

        }
    }
}
