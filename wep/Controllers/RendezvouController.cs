using Microsoft.AspNetCore.Mvc;

namespace wep.Controllers
{
    public class RendezvouController : Controller
    {
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
            var yazar = k.Yazarlar.Find(id);
            if (yazar is null)
            {
                TempData["msj"] = "Randevu Bulunmadı";
                return RedirectToAction("Index");
            }
            var kayit = k.Yazarlar.Include(x => x.Kitaplar).Where(x => x.YazarID == id).ToList();
            if (kayit[0].Kitaplar.Count > 0)
            {
                TempData["msj"] = "Yazazra ait Kitaplar var,once kitaplari siliniz";
                return RedirectToAction("Index");

            }
            k.Yazarlar.Remove(yazar);
            k.SaveChanges();
            TempData["msj"] = yazar.YazarAd + "adli yazar silindi";
            return RedirectToAction("Index");
        }
    }
}
