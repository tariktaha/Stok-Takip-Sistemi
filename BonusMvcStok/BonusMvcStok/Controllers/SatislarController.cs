using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BonusMvcStok.Models.Entity;

namespace BonusMvcStok.Controllers
{
    public class SatislarController : Controller
    {
        // GET: Satislar
        DbMvcStokEntities db = new DbMvcStokEntities();
        public ActionResult Index()
        {
            var satislar = db.tblsatislar.ToList();
            return View(satislar);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            //Ürünler
            List<SelectListItem> urun = (from x in db.tblurunler.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad,
                                            Value = x.id.ToString()
                                        }).ToList();
            ViewBag.drop1 = urun; //Sayfalar arası veri taşımak için

            //Personeller
            List<SelectListItem> per = (from x in db.tblpersonel.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad +" "+ x.soyad,
                                            Value = x.id.ToString()
                                        }).ToList();
            ViewBag.drop2 = per; //Sayfalar arası veri taşımak için

            //Müşteriler
            List<SelectListItem> must = (from x in db.tblmusteri.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad +" "+ x.soyad,
                                            Value = x.id.ToString()  
                                        }).ToList();
            ViewBag.drop3= must; //Sayfalar arası veri taşımak için
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(tblsatislar p)
        {
            var urun= db.tblurunler.Where(x => x.id == p.tblurunler.id).FirstOrDefault();
            var musteri= db.tblmusteri.Where(x => x.id == p.tblmusteri.id).FirstOrDefault();
            var personel= db.tblpersonel.Where(x => x.id == p.tblpersonel.id).FirstOrDefault();
            p.tblurunler = urun;
            p.tblmusteri = musteri;
            p.tblpersonel = personel;
            p.tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            tblsatislar tblsatislar = db.tblsatislar.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}