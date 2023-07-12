using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YuzDersAspNetCoreMVCProje.Models;

namespace YuzDersAspNetCoreMVCProje.Controllers
{
    public class PersonelController : Controller
    {
        Context con = new Context();
        [Authorize]
        public IActionResult Index()
        {
            var degerler = con.Personels.Include(x => x.Birim).ToList();
            return View(degerler);
        }
        public IActionResult Sil(int id)
        {
            var deger = con.Personels.Find(id);
            con.Personels.Remove(deger);
            con.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Ekle()
        {
            List<SelectListItem> deger = (from x in con.Birims.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.BirimAd,
                                              Value = x.BirimID.ToString()

                                          }).ToList();
            ViewBag.dgr = deger;
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Personel p)
        {
            var birim = con.Birims.Where(m => m.BirimID == p.Birim.BirimID).FirstOrDefault();
            p.Birim = birim;
            con.Personels.Add(p);
            con.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Guncelle(int id)
        {
            List<SelectListItem> birim = (from x in con.Birims.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.BirimAd,
                                              Value = x.BirimID.ToString()
                                          }).ToList();
            var deger = con.Personels.Find(id);
            ViewBag.dgr = birim;
            return View("Guncelle", deger);
        }
        public IActionResult GuncelleYap(Personel p)
        {
            var brm = con.Birims.Where(m => m.BirimID == p.Birim.BirimID).FirstOrDefault();
            var deger = con.Personels.Find(p.PersonelID);
            deger.PersonelAd = p.PersonelAd;
            deger.PersonelSoyad = p.PersonelSoyad;
            deger.PersonelSehir = p.PersonelSehir;
            deger.BirimID = brm.BirimID;
            con.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
