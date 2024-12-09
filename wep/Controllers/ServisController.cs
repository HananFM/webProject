using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using wep.Models;

namespace wep.Controllers
{
    public class ServisController : Controller
    {
        ServisContext _context = new ServisContext();

        // GET: Kitaps
        public async Task<IActionResult> Index()
        {
            var Context = _context.servis.Include(s => s.employee);
            return View(await Context.ToListAsync());
        }

        // GET: Kitaps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.servis == null)
            {
                return NotFound();
            }

            var Servis = await _context.servis
                .Include(s => s.employee)
                .FirstOrDefaultAsync(m => m.ServisID == id);
            if (Servis == null)
            {
                return NotFound();
            }

            return View(Servis);
        }

        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.employee, "EmployeeID", "EmployeeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServisID,ServisAdi,EmployeeID")] Servis Servis )
        {
            if (ModelState.IsValid)
            {
                _context.Add(Servis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.employee, "EmployeeID", "EmployeeIDAd", Servis.EmployeeID);
            return View(Servis);
        }

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
        public async Task<IActionResult> Edit(int id, [Bind("ServisID,ServisName,EmployeeID")] Servis Servis )
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.servis == null)
            {
                return NotFound();
            }

            var Servis = await _context.servis
                .Include(s => s.employee)
                .FirstOrDefaultAsync(s => s.ServisID == id);
            if (Servis == null)
            {
                return NotFound();
            }

            return View(Servis);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.servis == null)
            {
                return Problem("Entity set 'ServisContext.Servis'  is null.");
            }
            var servis = await _context.servis.FindAsync(id);
            if (servis != null)
            {
                _context.servis.Remove(servis);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool servisExists(int id)
        {
            return (_context.servis?.Any(s => s.ServisID == id)).GetValueOrDefault();
        }
    }

}

