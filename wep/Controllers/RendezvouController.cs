using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using wep.Models;

namespace wep.Controllers
{
    public class RendezvouController : Controller
    {
        ServisContext dp = new ServisContext();

        public async Task<IActionResult> Index()
        {
            var data = dp.rendezvou.Include(r => r.servis).Include(r => r.user);
            return View(await data.ToListAsync());
        }
        public IActionResult Create()
        {
            ViewData["ServisID"] = new SelectList(dp.servis, "ServisID", "ServisName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("RandezvouTime,ServisID")]  Rendezvou rendezvou)
        {
            if (rendezvou == null)
            {
                TempData["msj"] = "Lütfen dataları düzgün girin";
                return RedirectToAction("Index");
            }
            Servis s = dp.servis.First(s => s.ServisID == rendezvou.ServisID);
            User u = dp.user.First(u => u.UserId == 1);
            if (s == null)
            {
                TempData["msj"] ="Servis not available!";
                return RedirectToAction("Index");
            }

            if (u == null)
            {
                TempData["msj"] = "invalid User!";
                return RedirectToAction("Index");
            }
            DateTime selectedTime = DateTime.Now;
            if (!DateTime.TryParse(Request.Form["Time"].ToString(),out selectedTime))
            {
                TempData["msj"] = "invalid Time!";
                return RedirectToAction("Index");
            }
            
            var customTime = new DateTime(rendezvou.RandezvouTime.Year, rendezvou.RandezvouTime.Month, rendezvou.RandezvouTime.Day, selectedTime.Hour, selectedTime.Minute, 0);
            rendezvou.RandezvouTime = customTime;
            rendezvou.UserID = 1;
            rendezvou.user = u;
            rendezvou.servis = s;

            //toDo: chek later
            //if (ModelState.IsValid)
            //{
                dp.rendezvou.Add(rendezvou);
                dp.SaveChanges();
                TempData["msj"] = rendezvou.RendezvouID + " Rendezvou Başarıyla oluşturuldu.";
                return RedirectToAction("Index");
            //}

            TempData["msj"] = "Veri oluşturulurken bir hata oluştu.";
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id is null)
            {
                TempData["msj"] = "Lütfen dataları düzgün girin";
                return RedirectToAction("Index");
            }
            var rendezvou = dp.rendezvou.Find(id);
            if (rendezvou is null)
            {
                TempData["msj"] = "Randevu Bulunmadı";
                return RedirectToAction("Index");
            }
            //var kayit = dp.rendezvou.Include(x => x.servis).Where(x => x.RendezvouID == id).ToList();
            //if (kayit[0].servis.Count > 0)
            //{
            //    TempData["msj"] = " ";
            //    return RedirectToAction("Index");

            //}
            dp.rendezvou.Remove(rendezvou);
            dp.SaveChanges();
            TempData["msj"] = rendezvou.RendezvouID + " Randevou silindi";
            return RedirectToAction("Index");
        }

        public IActionResult Details(int? id)
        {
            if (id is null)
            {
                TempData["msj"] = "Lütfen dataları düzgün girin";
                return RedirectToAction("Index");
            }
            var rendezvou = dp.rendezvou.First(x => x.RendezvouID == id);

            if (rendezvou is null)
            {
                TempData["msj"] = "Rendezvou Bulunmadi";
                return RedirectToAction("Index");
            }
            return View(rendezvou);

        }

        public IActionResult Edit(int? id)
        {
            if (id is null)
            {
                TempData["msj"] = "Lütfen dataları düzgün girin";
                return RedirectToAction("Index");
            }
            var Rendezvou = dp.rendezvou.Find(id);
            if (Rendezvou is null)
            {
                TempData["msj"] = "ID ler eşleşmiyor";
                return RedirectToAction("Index");
            }
            return View(Rendezvou);
        }

        [HttpPost]
        public IActionResult Edit(int? id, Rendezvou R)
        {
            if (id is null)
            {
                TempData["msj"] = "Lütfen dataları düzgün girin";
                return RedirectToAction("Index");
            }
            if (id != R.RendezvouID)
            {
                TempData["msj"] = "ID ler eşleşmiyor";
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                dp.rendezvou.Update(R);
                dp.SaveChanges();
                TempData["msj"] = R.RendezvouID + " Günceleme yapıldı";
                return RedirectToAction("Index");
            }
            TempData["msj"] = R.RendezvouID + " Günceleme işleme başarsiz";
            return RedirectToAction("Index");

        }

    }
}
