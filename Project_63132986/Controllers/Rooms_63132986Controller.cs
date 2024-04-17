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
    public class Rooms_63132986Controller : Controller
    {
        private Project_63132986Entities1 db = new Project_63132986Entities1();
     

        // GET: Rooms_63132986
        public ActionResult Index()
        {
            return View();
        }


        // GET: Rooms_63132986/Create
        public ActionResult Create()
        {
            ViewBag.RoomTypeID = new SelectList(db.RoomTypes, "ID", "RoomTypeName");

            return View();
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string RoomFeature, string RoomDescription,[Bind(Include = "ID,RoomName,RoomTypeID,RoomStatus")] Room room)
        {
            if (ModelState.IsValid)
            {
                room.RoomFeature = RoomFeature;
                room.RoomDescription = RoomDescription; 
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoomTypeID = new SelectList(db.RoomTypes, "ID", "RoomTypeName", room.RoomTypeID);
            return View(room);
        }

        // GET: Rooms_63132986/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomTypeID = new SelectList(db.RoomTypes, "ID", "RoomTypeName", room.RoomTypeID);
            return View(room);
        }
        [HttpPost,ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string RoomFeature, string RoomDescription, [Bind(Include = "ID,RoomName,RoomTypeID,RoomStatus")] Room room)
        {
            if (ModelState.IsValid)
            {
                room.RoomFeature = RoomFeature;
                room.RoomDescription = RoomDescription;
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoomTypeID = new SelectList(db.RoomTypes, "ID", "RoomTypeName", room.RoomTypeID);
            return View(room);
        }

        // GET: Employees_63132986/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var room = db.Rooms.Where(x => x.ID == id).FirstOrDefault();
            if (room != null)
            {
                db.Rooms.Remove(room);
                db.SaveChanges();
                return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { status = "unsuccess" }, JsonRequestBehavior.AllowGet);
        }
  
        // Get Room List
        public ActionResult GetList()
        {
            var rooms = db.Rooms.ToList();
            List<Rooms_63135986> roomList = new List<Rooms_63135986>();
            rooms.ForEach(e =>
            {
                Rooms_63135986 room = new Rooms_63135986();
                room.ID = e.ID;
                room.RoomName = e.RoomName;
                room.RoomStatus = e.RoomStatus; 
                room.RoomType = e.RoomType.RoomTypeName;
                roomList.Add(room);
            }
            );
            return Json(new { data = roomList.ToList() }, JsonRequestBehavior.AllowGet);
        }

        // Change Room Status 
        [HttpPost]
        public ActionResult ChangeStatus(int id)
        {
            var room = db.Rooms.Where(x => x.ID == id).FirstOrDefault();
            if (room != null)
            {
                try
                {

                    room.RoomStatus = !room.RoomStatus;
                    db.SaveChanges();
                    return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new { status = "unsuccess" }, JsonRequestBehavior.AllowGet);

                }
            }
            else
                return Json(new { status = "unsuccess" }, JsonRequestBehavior.AllowGet);
        }
    }
}
