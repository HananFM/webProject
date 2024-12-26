using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using wep.Models;

namespace wep.Controllers
{
    [Authorize]

    public class RendezvouController : Controller
    {
        private readonly ServisContext _context;
        private readonly UserManager<UserDetails> _userManager;

        // Constructor with dependency injection
        public RendezvouController(ServisContext context, UserManager<UserDetails> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (User.IsInRole("admin"))
            {
                var data = _context.rendezvou.Include(r => r.servis).Include(r => r.servis.employee).Include(r => r.user);
                return View(await data.ToListAsync());
            }
            else if (user != null && User.IsInRole("client"))
            {
                var data = _context.rendezvou.Where(r => r.UserID == user.Id).Include(r => r.servis).Include(r => r.user);
                return View(await data.ToListAsync());
            }
            else//return empty list
            {
                var data = _context.rendezvou.Where(r => r.UserID == null).Include(r => r.servis).Include(r => r.user);
                return View(await data.ToListAsync());
            }
        }

        #region "Create"
        public IActionResult Create()
        {
            return Create(null, "");
        }

        private IActionResult Create(Rendezvou rendezvou, string msg)
        {
            // Fetch the list of services and pass it to the view 
            ViewBag.ServisList = _context.servis.ToList();
            if (rendezvou != null)
            {
                TempData["msj"] = msg;
                ViewData["ServisID"] = new SelectList(_context.servis, "ServisID", "ServisName", rendezvou.ServisID);
                return View(rendezvou);
            }
            else
            {
                ViewData["ServisID"] = new SelectList(_context.servis, "ServisID", "ServisName");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RandezvouTime,ServisID")] Rendezvou rendezvou)
        {
            //check model state
            if (!ModelState.IsValid)
            {
                // Output model state errors to the console or log
                string msg = "";
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    msg += error.ErrorMessage + "\n";
                    Console.WriteLine(error.ErrorMessage);
                }

                return Create(rendezvou, "invalid model state,\n" + msg);
            }

            //check servis
            var servis = await _context.servis.FirstOrDefaultAsync(s => s.ServisID == rendezvou.ServisID);
            if (servis == null)
            {
                return Create(rendezvou, "invalid ServisID!");
            }

            //check user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Create(rendezvou, "invalid user!");
            }

            //check time
            DateTime selectedTime = DateTime.Now;
            if (!DateTime.TryParse(Request.Form["Time"].ToString(), out selectedTime))
            {
                return Create(rendezvou, "invalid Time!");
            }
            var customTime = new DateTime(rendezvou.RandezvouTime.Year, rendezvou.RandezvouTime.Month, rendezvou.RandezvouTime.Day, selectedTime.Hour, selectedTime.Minute, 0);
            rendezvou.RandezvouTime = customTime;

            //check servis availability
            var list = _context.rendezvou.FirstOrDefault(r => r.ServisID == rendezvou.ServisID && r.RandezvouTime == customTime);
            if (list != null)
            {
                return Create(rendezvou, "Sorry, servis is not avaiable at selected time. try another time please!");
            }

            //save changes
            rendezvou.servis = servis;
            rendezvou.user = user;
            _context.Add(rendezvou);
            await _context.SaveChangesAsync();
            TempData["msj"] = "Saved Successfully!";
            return RedirectToAction(nameof(Index));

        }
        #endregion

        public IActionResult Delete(int? id)
        {
            if (id is null)
            {
                TempData["msj"] = "Lütfen dataları düzgün girin";
                return RedirectToAction("Index");
            }
            var rendezvou = _context.rendezvou.Include(r => r.servis).Include(r => r.servis.employee).Include(r => r.user).FirstOrDefault(r => r.RendezvouID == id);
            if (rendezvou is null)
            {
                TempData["msj"] = "Randevu Bulunmadı";
                return RedirectToAction("Index");
            }

            return View(rendezvou);
        }

        [HttpPost]
        public IActionResult RendezvouDelete(Rendezvou rendezvou)
        {

            if (rendezvou is null)
            {
                TempData["msj"] = "Randevu Bulunmadı";
                return RedirectToAction("Index");
            }


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
            var rendezvou = _context.rendezvou.Include(r => r.servis).Include(r => r.servis.employee).Include(r => r.user).FirstOrDefault(x => x.RendezvouID == id);

            if (rendezvou is null)
            {
                TempData["msj"] = "Rendezvou Bulunmadi";
                return RedirectToAction("Index");
            }

            return View(rendezvou);

        }

        #region "Edit"
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["msj"] = "Lütfen dataları düzgün girin";
                return RedirectToAction("Index");
            }

            var rendezvou = _context.rendezvou.Find(id);
            if (rendezvou == null)
            {
                TempData["msj"] = "ID ler eşleşmiyor";
                return RedirectToAction("Index");
            }

            return Edit(id.Value, rendezvou, "");
        }

        private IActionResult Edit(int id, Rendezvou rendezvou, string msg)
        {
            if (msg != "")
            {
                TempData["msj"] = msg;
            }

            ViewData["ServisID"] = new SelectList(_context.servis, "ServisID", "ServisName", rendezvou.ServisID);
            ViewBag.Services = _context.servis.ToList(); // Pass the services to the view
            return View(rendezvou);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Rendezvou rendezvou)
        {
            //check id
            if (id is null)
            {
                return Edit(id.Value, rendezvou, "Lütfen dataları düzgün girin");
            }
            if (id != rendezvou.RendezvouID)
            {
                return Edit(id.Value, rendezvou, "ID ler eşleşmiyor");
            }

            //check model state
            if (!ModelState.IsValid)
            {
                string msg = "";
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    msg += error.ErrorMessage + "\n";
                    Console.WriteLine(error.ErrorMessage);
                }
                return Edit(id.Value, rendezvou, "invalid model state,\n" + msg);
            }

            //check user
            string userID = "";
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Edit(id.Value, rendezvou, "Invalid User!");
            }
            rendezvou.user = user;

            //check time
            DateTime selectedTime = DateTime.Now;
            if (!DateTime.TryParse(Request.Form["Time"].ToString(), out selectedTime))
            {
                return Edit(id.Value, rendezvou, "Invalid Time!");
            }
            var customTime = new DateTime(rendezvou.RandezvouTime.Year, rendezvou.RandezvouTime.Month, rendezvou.RandezvouTime.Day, selectedTime.Hour, selectedTime.Minute, 0);
            rendezvou.RandezvouTime = customTime;

            //check servis availability
            var list = _context.rendezvou.FirstOrDefault(r => r.ServisID == rendezvou.ServisID && r.RandezvouTime == customTime);
            if (list != null)
            {
                return Edit(id.Value, rendezvou, "Sorry, servis is not avaiable at selected time. try another time please!");
            }

            //save
            _context.rendezvou.Update(rendezvou);
            _context.SaveChanges();
            TempData["msj"] = rendezvou.RendezvouID + " Günceleme yapıldı";
            return RedirectToAction("Index");
        }
        #endregion

    }
}
