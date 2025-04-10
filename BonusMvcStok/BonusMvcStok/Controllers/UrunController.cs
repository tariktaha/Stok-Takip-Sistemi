﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BonusMvcStok.Models.Entity;

namespace BonusMvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        DbMvcStokEntities db = new DbMvcStokEntities();
        public ActionResult Index(string p)
        {
            var urunler = from x in db.tblurunler select x;
            if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(x => x.ad.Contains(p));
            }
            return View(urunler.ToList());
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> ktg = (from x in db.tblkategori.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad,
                                            Value = x.id.ToString()


                                        }).ToList();
            ViewBag.drop = ktg; //Sayfalar arası veri taşımak için
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(tblurunler t)
        {
            var ktgr = db.tblkategori.Where(x => x.id == t.tblkategori.id).FirstOrDefault();
            t.tblkategori = ktgr;
            db.tblurunler.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> kat = (from x in db.tblkategori.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.ad,
                                             Value = x.id.ToString()
                                         }).ToList();
            var ktgr = db.tblurunler.Find(id);
            ViewBag.urunkategori = kat;
            return View("UrunGetir", ktgr);

        }
        public ActionResult UrunGuncelle(tblurunler p)
        {
            var urun = db.tblurunler.Find(p.id);
            urun.marka = p.marka;
            urun.satisfiyat = p.satisfiyat;
            urun.stok = p.stok;
            urun.alisfiyat = p.alisfiyat;
            urun.ad = p.ad; 
            var ktg = db.tblkategori.Where(x=>x.id==p.tblkategori.id).FirstOrDefault();
            urun.kategori = ktg.id;
            db.SaveChanges();
            return RedirectToAction("Index");
        } 
        public ActionResult UrunSil (tblurunler p1)
        {
            var urunbul = db.tblurunler.Find(p1.id);
            urunbul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
