using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Drawing;
using wep.Models;

namespace wep.Controllers
{
    
    public class ServisController : Controller
    {
        private readonly ServisContext _context;

        // Constructor with dependency injection
        public ServisController(ServisContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //public IIncludableQueryable<Servis,Employee> getServices()
        //{
        //    var data = _context.servis.Include(s => s.employee);
        //    return data;
        //}

        // GET: Kitaps
        public async Task<IActionResult> Index()
        {
            var data = _context.servis.Include(s => s.employee);
            //    return data;
            return View(await data.ToListAsync());
        }

        // GET: Kitaps/Details/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.servis == null)
            {
                return NotFound();
            }

            var Servis = await _context.servis
                .Include(s => s.employee)
                .Include(s => s.rendezvous)
                .ThenInclude(r => r.user)
                .FirstOrDefaultAsync(m => m.ServisID == id);
            if (Servis == null)
            {
                return NotFound();
            }

            return View(Servis);
        }
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.employee, "EmployeeID", "EmployeeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> Create([Bind("ServisID,ServisName,ServisFee,EmployeeID")] Servis Servis)
        {
            if (!ModelState.IsValid)
            {
                // Output model state errors to the console or log
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                // Ensure ViewData is populated for the dropdown list
                ViewData["EmployeeID"] = new SelectList(_context.employee, "EmployeeID", "EmployeeIDAd", Servis.EmployeeID);
                return View(Servis);
            }

            // Now you know ModelState is valid, you can proceed
            var employee = await _context.employee.FirstOrDefaultAsync(e => e.EmployeeID == Servis.EmployeeID);

            if (employee == null)
            {
                ModelState.AddModelError("ServisID", "The selected employee does not exist.");
                ViewData["EmployeeID"] = new SelectList(_context.employee, "EmployeeID", "EmployeeIDAd", Servis.EmployeeID);
                return View(Servis);
            }

            Servis.employee = employee;

            _context.Add(Servis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.servis == null)
            {
                return NotFound();
            }

            var servis = await _context.servis.FindAsync(id);
            if (servis == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.employee, "EmployeeID", "EmployeeName", servis.EmployeeID);
            return View(servis);
        }

        // POST: Kitaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ServisID,ServisName,ServisFee,EmployeeID")] Servis Servis)
        {
            if (id != Servis.ServisID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Servis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!servisExists(Servis.ServisID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.employee, "EmployeeID", "EmployeeName", Servis.EmployeeID);
            return View(Servis);
        }

        // GET: Kitaps/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.servis == null)
            {
                return NotFound();
            }

            var servis = await _context.servis
                .Include(s => s.employee)
                .Include(s => s.rendezvous)
                .FirstOrDefaultAsync(s => s.ServisID == id);
            if (servis == null)
            {
                return NotFound();
            }


            return View(servis);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.servis == null)
            {
                return Problem("Entity set 'ServisContext.Servis'  is null.");
            }
            var servis = await _context.servis.FindAsync(id);
            if (servis == null)
            {
                TempData["msj"] = "Çalışan Bulunmadı";
                return RedirectToAction("Index");
            }

            if (servis.rendezvous?.Count > 0)
            {
                TempData["msj"] = "Çalışan ait Servis var,once Servisleri siliniz";
                return RedirectToAction("Index");
            }
            _context.servis.Remove(servis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool servisExists(int id)
        {
            return (_context.servis?.Any(s => s.ServisID == id)).GetValueOrDefault();
        }
    }

}

