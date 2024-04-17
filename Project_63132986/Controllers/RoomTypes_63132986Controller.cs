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
    public class RoomTypes_63132986Controller : Controller
    {
        private Project_63132986Entities1 db = new Project_63132986Entities1();

        // GET: RoomTypes_63132986
        public ActionResult Index()
        {
            return View();
        }

       

        // GET: RoomTypes_63132986/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RoomTypeName,Price")] RoomType roomType)
        {
            if (ModelState.IsValid)
            {
                db.RoomTypes.Add(roomType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(roomType);
        }

        // GET: RoomTypes_63132986/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomType roomType = db.RoomTypes.Find(id);
            if (roomType == null)
            {
                return HttpNotFound();
            }
            return View(roomType);
        }

        // POST: RoomTypes_63132986/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RoomTypeName,Price")] RoomType roomType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(roomType);
        }

        // GET: RoomTypes_63132986/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var roomType = db.RoomTypes.Where(x => x.ID == id).FirstOrDefault();
            if (roomType != null)
            {
                try
                {
                    db.RoomTypes.Remove(roomType);
                    db.SaveChanges();
                    return Json(new { status = "success", message = "Delete succesfully" }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new { status = "unsuccess", message = "Cannot delete because this record is linked to another table" }, JsonRequestBehavior.AllowGet);
                }

            }
            else
                return Json(new { status = "unsuccess" }, JsonRequestBehavior.AllowGet);
        }
        // Get Room Type List
        public ActionResult GetList()
        {
            var roomTypes = db.RoomTypes.Select(e=> new {e.RoomTypeName,e.Price,e.ID}).ToList();
            return Json(new { data = roomTypes }, JsonRequestBehavior.AllowGet);
        }
    }
}
