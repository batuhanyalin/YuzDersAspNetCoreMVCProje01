using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YuzDersAspNetCoreMVCProje.Models;

namespace YuzDersAspNetCoreMVCProje.Controllers
{
    public class BirimController : Controller
    {
        Context con = new Context();
        [Authorize]
        public IActionResult Index()
        {
            var degerler = con.Birims.ToList();
            return View(degerler);
        }
        public IActionResult Sil(int id)
        {
            var deger = con.Birims.Find(id);
            con.Birims.Remove(deger);
            con.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Birim p)
        {
            con.Birims.Add(p);
            con.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Guncelle(int id)
        {
            var deger = con.Birims.Find(id);
            return View("Guncelle", deger);
        }
        public IActionResult GuncelleYap(Birim p)
        {
            var deger = con.Birims.Find(p.BirimID);
            deger.BirimAd = p.BirimAd;
            con.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult BirimDetay(int id)
        {
            var deger = con.Personels.Where(m => m.BirimID == id).ToList();
            var brmad = con.Birims.Where(m => m.BirimID == id).Select(y => y.BirimAd).FirstOrDefault();
            ViewBag.brm = brmad;
            return View(deger);
        }
    }
}
