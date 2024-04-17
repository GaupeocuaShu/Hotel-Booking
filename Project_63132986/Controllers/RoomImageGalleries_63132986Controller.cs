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
    public class RoomImageGalleries_63132986Controller : Controller
    {
        private Project_63132986Entities1 db = new Project_63132986Entities1();
 

        // GET: RoomImageGalleries_63132986
        public ActionResult Index(int ?ID)
        {
            if (ID != null) ViewBag.RoomID = ID;
            return View();
        }


        // GET: RoomImageGalleries_63132986/Create
        public ActionResult Create(int ID)
        {
            ViewBag.RoomID = ID;
            return View();
        }

        // POST: RoomImageGalleries_63132986/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(int RoomID, HttpPostedFileBase Image)
        {
            string postedFileName;
            postedFileName = System.IO.Path.GetFileName(Image.FileName);
            var path = Server.MapPath("~/assets/images/" + postedFileName);
            Image.SaveAs(path);
            RoomImageGallery gallery = new RoomImageGallery();
            gallery.RoomID = RoomID;
            gallery.RoomImage = postedFileName;
            db.RoomImageGalleries.Add(gallery);
            db.SaveChanges();
            return RedirectToAction("Index/"+RoomID);
        }




        // GET: RoomImageGalleries_63132986/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var RoomImageGallery = db.RoomImageGalleries.Where(x => x.ID == id).FirstOrDefault();
            if (RoomImageGallery != null)
            {
                db.RoomImageGalleries.Remove(RoomImageGallery);
                db.SaveChanges();
                return Json(new { status = "success", message = "Delete succesfully" }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { status = "unsuccess" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetList(int id)
        {
            var Images = db.RoomImageGalleries.Where(e=>e.RoomID== id).Select(e => new {e.ID, e.RoomImage,e.RoomID}).ToList();
            return Json(new { data = Images }, JsonRequestBehavior.AllowGet);           
        }
        
    }
}
