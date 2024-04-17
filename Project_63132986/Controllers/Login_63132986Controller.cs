using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Project_63132986.Models;

namespace Project_63132986.Controllers
{
    public class Login_63132986Controller : Controller
    {
        Project_63132986Entities1 db = new Project_63132986Entities1();
        // GET: Login_63132986
        public ActionResult Index()
        {
            return View(db.UserInfoes.ToList());
        }
        // Trang đăng ký
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost] 
        public ActionResult Signup(UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                if (db.UserInfoes.Any(x => x.UserName == userInfo.UserName || x.Email == userInfo.Email))
                {
                    ViewBag.Notification = "This account has already existed";
                    return View();
                }
                else
                {
                    userInfo.UserRole = "User";
                    db.UserInfoes.Add(userInfo);
                    db.SaveChanges();
                    TempData["Notification"] = "Create Account Successfully";
                    return RedirectToAction("Login", "Login_63132986");
                }
            }
            return View(userInfo);
        }

        // Trang đăng xuất
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Frontend_63132986");
        }
        // Trang đăng nhập 
        [HttpGet]
        public ActionResult Login()
        {
            
            ViewBag.Status = "Success";
            ViewBag.Notification = TempData["Notification"];
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(UserInfo userInfo)
        {
            var checkLogin = db.UserInfoes.Where(x => x.UserName == userInfo.UserName
            && x.UserPassword == userInfo.UserPassword).FirstOrDefault();
            if (checkLogin != null)
            {
                Session["role"] = checkLogin.UserRole;
                Session["userID"] = checkLogin.ID;
                Session["username"] = userInfo.UserName.ToString();
                if (checkLogin.UserRole == "User") return RedirectToAction("Index", "Frontend_63132986");
                else return RedirectToAction("Index", "Dashboard_63132986");
            }
            else
            {
                ViewBag.Notification = "Wrong username or password";
                return View();
            }
        }

        // Trang quên mật khẩu 
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(string Email)
        {
            var account = db.UserInfoes.Where(x => x.Email == Email).FirstOrDefault();
            // Nếu tài khoản tổn tại 
            if (account != null)
            {
                // Tạo reset code 
                string resetCode = Guid.NewGuid().ToString();
                // gửi mail tới user 
                SendEmalToUser(account.Email, resetCode, "ResetPassword");
               
                account.ResetPasswordCode = resetCode;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();

            }
            // Nếu tài khoản không tồn tại 
            else {
                ViewBag.Notification = "Account is not existed";
       
            }; return View();
        }

        // Gửi mail tới user 

        public void SendEmalToUser(string userEmail,string code, string emailType)
        {
            
            var from = new MailAddress("nguyenngocanhthufpt77@gmail.com");
            var to = new MailAddress(userEmail);
            var fromEmailPassword = "pjav wkmq ruoi bmbz";
            string subject = " ";
            string body = " ";
            string link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, "/Login_63132986/ResetPassword?code="+code);
            if(emailType == "ResetPassword")
            {
                subject = "Reset Password Link";
                body = "Link Reset Password <a href = "+link+" > " +link + "</a>";
            }

            // Configurate sending email

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(from.Address, fromEmailPassword),
            };
            
            // Sending email 

            var email = new MailMessage(from, to)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            smtp.Send(email);
        }


        // Trang reset mật khẩu
        [HttpGet]
        public ActionResult ResetPassword(string code)
        {
            var user = db.UserInfoes.Where(x => x.ResetPasswordCode == code).FirstOrDefault(); 
            if(user != null)
            {
                ResetPasswordModel_63132986 model = new ResetPasswordModel_63132986();
                model.ResetCode = code;
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult ResetPassword(ResetPasswordModel_63132986 model)
        {
     
            var user = db.UserInfoes.Where(x => x.ResetPasswordCode == model.ResetCode).FirstOrDefault();
            if(user != null)
            {
                user.UserPassword = model.NewPassword;
                user.ResetPasswordCode = " ";
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                TempData["Notification"] = "New Password updated successfully"; 

            }
            else
            {
                ViewBag.Notification = "Something is invalid";
            }
            return RedirectToAction("Login", "Login_63132986");
        }

    }
}