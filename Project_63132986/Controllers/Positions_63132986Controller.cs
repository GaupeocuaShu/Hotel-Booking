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
    public class Positions_63132986Controller : Controller
    {
        private Project_63132986Entities1 db = new Project_63132986Entities1();

        // GET: Positions_63132986
        public ActionResult Index()
        {
            return View();
        }

        // GET: Positions_63132986/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Positions_63132986/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PositionName,Salary")] Position position)
        {
            if (ModelState.IsValid)
            {
                db.Positions.Add(position);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(position);
        }

        // GET: Positions_63132986/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Position position = db.Positions.Find(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            return View(position);
        }

        // POST: Positions_63132986/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PositionName,Salary")] Position position)
        {
            if (ModelState.IsValid)
            {
                db.Entry(position).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(position);
        }

        // GET: Positions_63132986/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var position = db.Positions.Where(x => x.ID == id).FirstOrDefault();
            if (position != null)
            {
                try
                {
                    db.Positions.Remove(position);
                    db.SaveChanges();
                    return Json(new { status = "success",message = "Delete succesfully" }, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(new { status = "unsuccess",
                        message = "Cannot delete because this record is linked to another table"},
                        JsonRequestBehavior.AllowGet);
                }
            
            }
            else
                return Json(new { status = "unsuccess",message ="There is something wrong"}, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetList()
        {
            var positions = db.Positions.Select(e => new { e.ID ,e.PositionName,e.Salary}).ToList();
            return Json(new { data = positions}, JsonRequestBehavior.AllowGet);
        }
    }
}
