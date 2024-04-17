using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Project_63132986.Models;

namespace Project_63132986.Controllers
{
    public class Employees_63132986Controller : Controller
    {
        private Project_63132986Entities1 db = new Project_63132986Entities1();

        // GET: Employees_63132986
        public ActionResult Index()
        {
            return View();

        }

        // GET: Employees_63132986/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);

            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees_63132986/Create
        public ActionResult Create()
        {
            ViewBag.PositionID = new SelectList(db.Positions, "ID", "PositionName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,EmployeeName,DateOfBirth,Email,PhoneNumber,Sex,EmployeeAddress,PositionID,Avatar")] 
        Employee employee)
        {
            var Avatar = Request.Files["Avatar"];
            var path = "";
            string postedFileName = "";
            try
            {
                postedFileName = System.IO.Path.GetFileName(Avatar.FileName);
                path = Server.MapPath("~/assets/images/" + postedFileName);
                Avatar.SaveAs(path);
            }
            catch { }
            if (ModelState.IsValid)
            {
                employee.Avatar = postedFileName;
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PositionID = new SelectList(db.Positions, "ID", "PositionName", employee.PositionID);
            return View(employee);
        }

        // GET: Employees_63132986/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.PositionID = new SelectList(db.Positions, "ID", "PositionName", employee.PositionID);
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EmployeeName,DateOfBirth,Email,PhoneNumber,Sex," +
            "EmployeeAddress,PositionID,Avatar")] Employee employee)
        {
            var Avatar = Request.Files["Avatar"];
            Employee emp = db.Employees.Find(employee.ID);
            if (Avatar.ContentLength>0)
            {
                var path = "";
                string postedFileName = "";
                try
                {
                    postedFileName = System.IO.Path.GetFileName(Avatar.FileName);
                    path = Server.MapPath("~/assets/images/" + postedFileName);
                    Avatar.SaveAs(path);
                    emp.Avatar = postedFileName;
                }
                catch { }
            }
            if (ModelState.IsValid)
            {          
                emp.PhoneNumber = employee.PhoneNumber;
                emp.PositionID = employee.PositionID;
                emp.DateOfBirth = employee.DateOfBirth;
                emp.Email = employee.Email;
                emp.Sex = employee.Sex;
                emp.EmployeeName = employee.EmployeeName;
                emp.EmployeeAddress = employee.EmployeeAddress;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PositionID = new SelectList(db.Positions, "ID", "PositionName", employee.PositionID);
            return View(employee);
        }

        // GET: Employees_63132986/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var employee = db.Employees.Where(x => x.ID == id).FirstOrDefault();
            if (employee != null)
            {
                db.Employees.Remove(employee);
                db.SaveChanges();
                return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { status = "unsuccess" }, JsonRequestBehavior.AllowGet);
        }


        // Get Employees List 
        public ActionResult GetList()
        {
            var employees = db.Employees.ToList();
            List<Employees_63132986> employeeList = new List<Employees_63132986>();
            employees.ForEach(e =>
                {
                    Employees_63132986 emp = new Employees_63132986();
                    emp.EmployeeName = e.EmployeeName;
                    emp.DateOfBirth = e.DateOfBirth.ToString("dd-MM-yyyy");
                    emp.Email = e.Email;
                    emp.EmployeeAddress = e.EmployeeAddress;
                    emp.Position = e.Position.PositionName;
                    emp.ID = e.ID;
                    emp.PhoneNumber = e.PhoneNumber;
                    emp.Sex = e.Sex;
                    employeeList.Add(emp);
                }
            );
            return Json(new { data = employeeList.ToList()}, JsonRequestBehavior.AllowGet);
        }
    }
}
