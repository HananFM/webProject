using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wep.Models;

namespace wep.Controllers
{
    public class RendezvouController : Controller
    {
        ServisContext dp = new ServisContext();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RendezvouDelete(int? id)
        {
            if (id is null)
            {
                TempData["msj"] = "Lütfen dataları düzgün girin";
                return RedirectToAction("Index");
            }
            var employee = dp.employee.Find(id);
            if (employee is null)
            {
                TempData["msj"] = "Randevu Bulunmadı";
                return RedirectToAction("Index");
            }
            var kayit = dp.employee.Include(x => x.servis).Where(x => x.EmployeeID == id).ToList();
            if (kayit[0].servis.Count > 0)
            {
                TempData["msj"] = " ";
                return RedirectToAction("Index");

            }
            dp.employee.Remove(employee);
            dp.SaveChanges();
            TempData["msj"] = employee.EmployeeName + "Randevou silindi";
            return RedirectToAction("Index");
        }


    }
}
