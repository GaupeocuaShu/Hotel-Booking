using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_63132986.Models;

namespace Project_63132986.Controllers
{
    public class HotelServices_63132986Controller : Controller
    {
        private Project_63132986Entities1 db = new Project_63132986Entities1();

        // GET: HotelServices_63132986
        public ActionResult Index()
        {
            return View();
        }


        // GET: HotelServices_63132986/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string ServiceIcon,[Bind(Include = "ID,ServiceName,Price,ServiceDescription")] HotelService hotelService)
        {
            if (ModelState.IsValid)
            {
                hotelService.ServiceIcon = ServiceIcon;
                db.HotelServices.Add(hotelService);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hotelService);
        }

        // GET: HotelServices_63132986/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelService hotelService = db.HotelServices.Find(id);
            if (hotelService == null)
            {
                return HttpNotFound();
            }
            return View(hotelService);
        }

        // POST: HotelServices_63132986/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ServiceName,Price,ServiceDescription,ServiceIcon")] HotelService hotelService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotelService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotelService);
        }

        // GET: HotelServices_63132986/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var service = db.HotelServices.Where(x => x.ID == id).FirstOrDefault();
            if (service != null)
            {
                db.HotelServices.Remove(service);
                db.SaveChanges();
                return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { status = "unsuccess" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetList()
        {
            var services = db.HotelServices.Select(e=> new { e.ID,e.Price,e.ServiceIcon,e.ServiceName}).ToList();
            return Json(new { data = services }, JsonRequestBehavior.AllowGet);
        }
    }
}
