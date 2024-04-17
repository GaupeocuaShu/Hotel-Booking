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
    public class SliderBannerImages_63132986Controller : Controller
    {
        private Project_63132986Entities1 db = new Project_63132986Entities1();

        // GET: SliderBannerImages_63132986
        public ActionResult Index()
        {
            return View(db.SliderBannerImages.ToList());
        }

        // GET: SliderBannerImages_63132986/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderBannerImage sliderBannerImage = db.SliderBannerImages.Find(id);
            if (sliderBannerImage == null)
            {
                return HttpNotFound();
            }
            return View(sliderBannerImage);
        }

        // GET: SliderBannerImages_63132986/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase Image,SliderBannerImage bannerImage)
        {
            string postedFileName;
            postedFileName = System.IO.Path.GetFileName(Image.FileName);
            var path = Server.MapPath("~/assets/images/" + postedFileName);
            Image.SaveAs(path);
            bannerImage.BannerImage = postedFileName;
            db.SliderBannerImages.Add(bannerImage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: SliderBannerImages_63132986/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderBannerImage sliderBannerImage = db.SliderBannerImages.Find(id);
            if (sliderBannerImage == null)
            {
                return HttpNotFound();
            }
            return View(sliderBannerImage);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase Image,int ID, SliderBannerImage bannerImage)
        {
            var newBanner = db.SliderBannerImages.Find(ID);
            if (Image != null)
            {
                string postedFileName;
                postedFileName = System.IO.Path.GetFileName(Image.FileName);
                var path = Server.MapPath("~/assets/images/" + postedFileName);
                Image.SaveAs(path);
                newBanner.BannerImage = postedFileName;
             }
            if (ModelState.IsValid)
            {
                newBanner.ImageOrder = bannerImage.ImageOrder;
                newBanner.Link = bannerImage.Link;
                newBanner.Title = bannerImage.Title;
                newBanner.Content = bannerImage.Content;
     
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var sliderBannerImages = db.SliderBannerImages.Where(x => x.ID == id).FirstOrDefault();
            if (sliderBannerImages != null)
            {
                db.SliderBannerImages.Remove(sliderBannerImages);
                db.SaveChanges();
                return Json(new { status = "success", message = "Delete succesfully" }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { status = "unsuccess" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetList()
        {
            var Images = db.SliderBannerImages.ToList();
            return Json(new { data = Images }, JsonRequestBehavior.AllowGet);
        }
    }
}
