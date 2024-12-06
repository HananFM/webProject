using Microsoft.AspNetCore.Mvc;
using wep.Models;

namespace wep.Controllers
{
    public class EmployeeController : Controller
    {
        ServisContext dp = new ServisContext();

        public IActionResult Index()
        {
            var employee = dp.employee.ToList();
            return View(employee);
        }
        public IActionResult EmployeeAdd()
        {
            return View();
        }
        public IActionResult EmployeeSave(Employee E)
        {
            if (ModelState.IsValid)
            {
                dp.employee.Add(E);
                dp.SaveChanges();
                TempData["msj"] = E.EmployeeName + " adlı Çalışan eklendi";
                return RedirectToAction("Index");
            }
            TempData["msj"] = "Lütfen Dataları düzgün giriniz";
            return RedirectToAction("YazarEkle");
        }
    }
}
