using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            return RedirectToAction("EmployeeEkle");
        }

        public IActionResult EmployeeDelete(int? id)
        {
            if (id is null)
            {
                TempData["msj"] = "Lütfen dataları düzgün girin";
                return RedirectToAction("Index");
            }
            var employee = dp.employee.Find(id);
            if (employee is null)
            {
                TempData["msj"] = "Çalışan Bulunmadı";
                return RedirectToAction("Index");
            }
            var kayit = dp.employee.Include(x => x.servis).Where(x => x.EmployeeID == id).ToList();
            if (kayit[0].servis.Count > 0)
            {
                TempData["msj"] = "Çalışan ait Servis var,once Servisleri siliniz";
                return RedirectToAction("Index");

            }
            dp.employee.Remove(employee);
            dp.SaveChanges();
            TempData["msj"] = employee.EmployeeName + "Adli çalışan silindi";
            return RedirectToAction("Index");
        }



        public IActionResult EmployeeDetails(int? id)
        {
            if (id is null)
            {
                TempData["msj"] = "Lütfen dataları düzgün girin";
                return RedirectToAction("Index");
            }
            var employee = dp.employee.Include(x => x.servis).First(x => x.EmployeeID == id);

            if (employee is null)
            {
                TempData["msj"] = "Çalışan Bulunmadi";
                return RedirectToAction("Index");
            }
            return View(employee);

        }

        public IActionResult EmployeeEdit(int? id)
        {
            if (id is null)
            {
                TempData["msj"] = "Lütfen dataları düzgün girin";
                return RedirectToAction("Index");
            }
            var employee = dp.employee.Find(id);
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
                dp.employee.Update(E);
                dp.SaveChanges();
                TempData["msj"] = E.EmployeeName + "Adli çalışan günceleme yapıldı";
                return RedirectToAction("Index");
            }
            TempData["msj"] = E.EmployeeName + "günceleme işleme başarsiz";
            return RedirectToAction("Index");

        }


    }

}
