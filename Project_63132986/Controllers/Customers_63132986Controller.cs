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
    public class Customers_63132986Controller : Controller
    {
        private Project_63132986Entities1 db = new Project_63132986Entities1();

        // GET: Customers_63132986
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }


        // GET: Customers_63132986/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CustomerName,PhoneNumber,DateOfBirth,CustomerAddress,Sex")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers_63132986/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CustomerName,PhoneNumber,DateOfBirth,CustomerAddress,Sex")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers_63132986/Delete/5
        [HttpPost]
        public ActionResult Delete(string id)
        {
            var customer = db.Customers.Where(x => x.ID == id).FirstOrDefault();
            if (customer != null)
            {
                db.Customers.Remove(customer);
                db.SaveChanges();
                return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { status = "unsuccess" }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetList()
        {
            var customers = db.Customers.ToList();
            List<Customers_63132986> customerList = new List<Customers_63132986>();
            customers.ForEach(e =>
            {
                Customers_63132986 cus = new Customers_63132986();
                cus.CustomerName = e.CustomerName;
                cus.DateOfBirth = e.DateOfBirth.ToString("dd-MM-yyyy");
                cus.CustomerAddress = e.CustomerAddress;
                cus.ID = e.ID;
                cus.PhoneNumber = e.PhoneNumber;
                cus.Sex = e.Sex;
                customerList.Add(cus);
            }
            );
            return Json(new { data = customerList.ToList() }, JsonRequestBehavior.AllowGet);
        }
    }
}
