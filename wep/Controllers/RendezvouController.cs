using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using wep.Models;

namespace wep.Controllers
{
    public class RendezvouController : Controller
    {
        ServisContext _context = new ServisContext();

        public async Task<IActionResult> Index()
        {
            var data = _context.rendezvou.Include(r => r.servis).Include(r => r.user);
            return View(await data.ToListAsync());
        }
        public IActionResult Create()
        {
            ViewData["ServisID"] = new SelectList(_context.servis, "ServisID", "ServisName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RandezvouTime,ServisID")]  Rendezvou rendezvou)
        {
            if (!ModelState.IsValid)
            {
                // Output model state errors to the console or log
                string msg = "";
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    msg += error.ErrorMessage + "\n";
                    Console.WriteLine(error.ErrorMessage);
                }

                TempData["msj"] = "invalid model state,\n"+msg;
                return RedirectToAction("Index");
            }

            // Now you know ModelState is valid, you can proceed
            var servis = await _context.servis.FirstOrDefaultAsync(s => s.ServisID == rendezvou.ServisID);
            var user = await _context.user.FirstOrDefaultAsync(s => s.UserId == 1);

            if (servis == null)
            {
                TempData["msj"] = "invalid ServisID!";
                return RedirectToAction("Index");
            }
            
            if(user == null)
            {
                TempData["msj"] = "invalid userID!";
                return RedirectToAction("Index");
            }

            DateTime selectedTime = DateTime.Now;
            if (!DateTime.TryParse(Request.Form["Time"].ToString(), out selectedTime))
            {
                TempData["msj"] = "invalid Time!";
                return RedirectToAction("Index");
            }

            var customTime = new DateTime(rendezvou.RandezvouTime.Year, rendezvou.RandezvouTime.Month, rendezvou.RandezvouTime.Day, selectedTime.Hour, selectedTime.Minute, 0);
            rendezvou.RandezvouTime = customTime;
            rendezvou.servis = servis;
            rendezvou.user = user;

            _context.Add(rendezvou);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Delete(int? id)
        {
            if (id is null)
            {
                TempData["msj"] = "Lütfen dataları düzgün girin";
                return RedirectToAction("Index");
            }
            var rendezvou = _context.rendezvou.Find(id);
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
            _context.rendezvou.Remove(rendezvou);
            _context.SaveChanges();
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
            var rendezvou = _context.rendezvou.Include(r => r.servis).Include(r => r.user).FirstOrDefault(x => x.RendezvouID == id);

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
            var Rendezvou = _context.rendezvou.Find(id);
            if (Rendezvou is null)
            {
                TempData["msj"] = "ID ler eşleşmiyor";
                return RedirectToAction("Index");
            }
            ViewData["ServisID"] = new SelectList(_context.servis, "ServisID", "ServisName", Rendezvou.ServisID);
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
            R.UserID = 1;
            if (ModelState.IsValid)
            {
                _context.rendezvou.Update(R);
                _context.SaveChanges();
                TempData["msj"] = R.RendezvouID + " Günceleme yapıldı";
                return RedirectToAction("Index");
            }
            TempData["msj"] = R.RendezvouID + " Günceleme işleme başarsiz";
            return RedirectToAction("Index");

        }

    }
}
