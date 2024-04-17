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
    public class ServiceImages_63132986Controller : Controller
    {
        private Project_63132986Entities1 db = new Project_63132986Entities1();

        // GET: ServiceImages_63132986
        public ActionResult Index(int? ID)
        {
            if (ID != null) ViewBag.ServiceID = ID;
            return View();
        }

        // GET: ServiceImages_63132986/Create
        public ActionResult Create(int ID)
        {
            ViewBag.ServiceID = ID;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int ServiceID, HttpPostedFileBase Image)
        {
            string postedFileName;
            postedFileName = System.IO.Path.GetFileName(Image.FileName);
            var path = Server.MapPath("~/assets/images/" + postedFileName);
            Image.SaveAs(path);
            ServiceImage gallery = new ServiceImage();
            gallery.ServiceID = ServiceID;
            gallery.ServiceImage1 = postedFileName;
            db.ServiceImages.Add(gallery);
            db.SaveChanges();
            return RedirectToAction("Index/" + ServiceID);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            var serviceImage = db.ServiceImages.Where(x => x.ID == id).FirstOrDefault();
            if (serviceImage != null)
            {
                db.ServiceImages.Remove(serviceImage);
                db.SaveChanges();
                return Json(new { status = "success", message = "Delete succesfully" }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { status = "unsuccess" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetList(int id)
        {
            var Images = db.ServiceImages.
                Where(e => e.ServiceID == id).
                Select(e => new { e.ID, e.ServiceImage1, e.ServiceID }).
                ToList();
            return Json(new { data = Images }, JsonRequestBehavior.AllowGet);
        }
    }
}
