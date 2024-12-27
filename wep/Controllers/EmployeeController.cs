using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wep.Models;

namespace wep.Controllers
{
    
    public class EmployeeController : Controller
    {
        private readonly ServisContext _context;

        // Constructor with dependency injection
        public EmployeeController(ServisContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var employee = _context.employee.ToList();
            return View(employee);
        }

        [Authorize(Roles = "admin")]
        public IActionResult EmployeeAdd()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        public IActionResult EmployeeSave(Employee E)
        {
            if (ModelState.IsValid)
            {
                _context.employee.Add(E);
                _context.SaveChanges();
                TempData["msj"] = E.EmployeeName + " adlı Çalışan eklendi";
                return RedirectToAction("Index");
            }
            TempData["msj"] = "Lütfen Dataları düzgün giriniz";
            return RedirectToAction("EmployeeEkle");
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EmployeeDelete(int? id)
        {
            if (id == null || _context.employee == null)
            {
                return NotFound();
            }

            var employee = await _context.employee
                .Include(e => e.servis)
                .FirstOrDefaultAsync(s => s.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int EmployeeID)
        {
            if (_context.employee == null)
            {
                TempData["msj"] = "Lütfen dataları düzgün girin";
                return RedirectToAction("Index");
            }
            var employee = await _context.employee.Include(e => e.servis).FirstOrDefaultAsync(e => e.EmployeeID == EmployeeID);
            if (employee is null)
            {
                TempData["msj"] = "Çalışan Bulunmadı";
                return RedirectToAction("Index");
            }

            if (employee.servis?.Count > 0)
            {
                TempData["msj"] = "Çalışan ait Servis var,once Servisleri siliniz";
                return RedirectToAction("Index");

            }
            _context.employee.Remove(employee);
            _context.SaveChanges();
            TempData["msj"] = employee.EmployeeName + "Adli çalışan silindi";
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "admin")]
        public IActionResult EmployeeDetails(int? id)
        {
            if (id is null)
            {
                TempData["msj"] = "Lütfen dataları düzgün girin";
                return RedirectToAction("Index");
            }
            var employee = _context.employee.Include(x => x.servis).First(x => x.EmployeeID == id);

            if (employee is null)
            {
                TempData["msj"] = "Çalışan Bulunmadi";
                return RedirectToAction("Index");
            }
            return View(employee);

        }
        [Authorize(Roles = "admin")]
        public IActionResult EmployeeEdit(int? id)
        {
            if (id is null)
            {
                TempData["msj"] = "Lütfen dataları düzgün girin";
                return RedirectToAction("Index");
            }
            var employee = _context.employee.Find(id);
            if (employee is null)
            {
                TempData["msj"] = "ID ler eşleşmiyor";
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        [HttpPost]
        public IActionResult EmployeeEdit(int? id, Employee E)
        {
            if (id is null)
            {
                TempData["msj"] = "Lütfen dataları düzgün girin";
                return RedirectToAction("Index");
            }
            if (id != E.EmployeeID)
            {
                TempData["msj"] = "ID ler eşleşmiyor";
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                _context.employee.Update(E);
                _context.SaveChanges();
                TempData["msj"] = E.EmployeeName + "Adli çalışan günceleme yapıldı";
                return RedirectToAction("Index");
            }
            TempData["msj"] = E.EmployeeName + "günceleme işleme başarsiz";
            return RedirectToAction("Index");

        }


    }

}
