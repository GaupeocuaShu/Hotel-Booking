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
    public class Users_63132986Controller : Controller
    {
        private Project_63132986Entities1 db = new Project_63132986Entities1();

        // GET: Users_63132986
        public ActionResult Index()
        {
            return View(db.UserInfoes.ToList());
        }


        // GET: Users_63132986/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var user= db.UserInfoes.Where(x => x.ID == id).FirstOrDefault();
            if (user != null)
            {
                db.UserInfoes.Remove(user);
                db.SaveChanges();
                return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { status = "unsuccess" }, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult GetList()
        {
            var UserInfoes = db.UserInfoes.Select
                (e=> new {e.ID, e.UserName,e.Email,e.PhoneNumber,e.UserRole});
            return Json(new { data = UserInfoes.ToList() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChangeRole(int id,string role)
        {
            var user = db.UserInfoes.Find(id);
            user.ReUserPassword = user.UserPassword;
            user.UserRole = role;
            db.SaveChanges();
            return Json(new { status ="success"}, JsonRequestBehavior.AllowGet);
        }
    }
}
