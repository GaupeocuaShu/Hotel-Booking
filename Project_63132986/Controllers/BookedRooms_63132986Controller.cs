    using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_63132986.Models;

namespace Project_63132986.Controllers
{
    public class BookedRooms_63132986Controller : Controller
    {
        private Project_63132986Entities1 db = new Project_63132986Entities1();

        // GET: BookedRooms_63132986
        public ActionResult Index()
        {
       
            return View();
        }

        // GET: BookedRooms_63132986/Details/5
        public ActionResult Details(int bookedID, string customerID, int? userID, int roomID)
        {
            var customer = db.Customers.Find(customerID);
               
            var user = db.UserInfoes.Find(userID);
            var room = db.Rooms.Find(roomID);
            BookedRoom bookedRoom = db.BookedRooms.Find(bookedID);
            var services = db.ServiceUsages.Where(e => e.BookedRoomID == bookedID).Include(e => e.HotelService).ToList();
            var invoice = db.Invoices.Where(e => e.BookedRoomID == bookedID).FirstOrDefault();
            
            dynamic bookedInfo = new ExpandoObject();
            bookedInfo.customer = customer;
            bookedInfo.user = user;
            bookedInfo.room = room;
            bookedInfo.bookedRoom = bookedRoom;
            bookedInfo.services = services;
            bookedInfo.invoice = invoice;
   

            TempData["bookedInfo"] = bookedInfo;

            if (bookedRoom == null)
            {
                return HttpNotFound();
            }
            return View(bookedInfo);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookedRoom bookedRoom)
        {
            if(bookedRoom.UserID == null) return Json(new { status = "unsuccess",
            navigation = "/Login_63132986/Login"}, JsonRequestBehavior.AllowGet);
           
            var totalPrice = Request["TotalPrice"]; 
            var totalBookedDay = Request["TotalBookedDay"]; 
            var bookedRoomID =  db.BookedRooms.Add(bookedRoom).ID;
            var invoice = new Invoice();
            invoice.BookedRoomID = bookedRoomID;
            invoice.TotalBookedDay = int.Parse(totalBookedDay);
            invoice.TotalPrice = decimal.Parse(totalPrice);
            db.Invoices.Add(invoice);
            db.SaveChanges();       
            var servicesRequest = Request["ServiceID[]"];
            if (servicesRequest != null)
            {
                var services = servicesRequest.Split(',');
                foreach (var service in services)
                {
                    ServiceUsage serviceUsage = new ServiceUsage();
                    serviceUsage.BookedRoomID = bookedRoom.ID;
                    serviceUsage.ServiceID = int.Parse(service);
                    db.ServiceUsages.Add(serviceUsage);
                    db.SaveChanges();
                }
            }
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }

        // GET: BookedRooms_63132986/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookedRoom bookedRoom = db.BookedRooms.Find(id);
            if (bookedRoom == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "CustomerName", bookedRoom.CustomerID);
            ViewBag.RoomID = new SelectList(db.Rooms, "ID", "RoomName", bookedRoom.RoomID);
            ViewBag.UserID = new SelectList(db.UserInfoes, "ID", "UserName", bookedRoom.UserID);
            return View(bookedRoom);
        }

        // POST: BookedRooms_63132986/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RoomID,CheckinType,CheckinDay,CheckoutDay,UserID,CustomerID,BookDate,PaymentStatus,CheckinStatus")] BookedRoom bookedRoom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookedRoom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "CustomerName", bookedRoom.CustomerID);
            ViewBag.RoomID = new SelectList(db.Rooms, "ID", "RoomName", bookedRoom.RoomID);
            ViewBag.UserID = new SelectList(db.UserInfoes, "ID", "UserName", bookedRoom.UserID);
            return View(bookedRoom);
        }

        // GET: BookedRooms_63132986/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var bookedRoom = db.BookedRooms.Where(x => x.ID == id).FirstOrDefault();
            if (bookedRoom != null)
            {
                var invoice = db.Invoices.Where(e => e.BookedRoomID == id).FirstOrDefault();
                db.Invoices.Remove(invoice);
                var services = db.ServiceUsages.Where(e => e.BookedRoomID == id).ToList();
                for(int i = 0; i < services.Count; i++)
                {
                    db.ServiceUsages.Remove(services[i]);
                }

                db.BookedRooms.Remove(bookedRoom);
                db.SaveChanges();
                return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { status = "unsuccess" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetList()
        {
            var bookedRooms = db.BookedRooms.Where(e => e.BookingStatus != "Cancelled").ToList();
            List<BookedRoom_63132986> bookedRoomList = new List<BookedRoom_63132986>();
            bookedRooms.ForEach(e =>
            {
                BookedRoom_63132986 room = new BookedRoom_63132986();
                room.ID = e.ID;
                room.CustomerID = e.CustomerID == null ? "---  ---  ---" : e.CustomerID;
                room.UserName = e.UserInfo.UserName;
                room.RoomName = e.Room.RoomName;
                room.CheckinDay = e.CheckinDay.ToString("MM-dd-yyyy");
                room.PaymentStatus = e.PaymentStatus;
                room.CheckinStatus = e.CheckinStatus;
                room.UserID = e.UserInfo.ID;
                room.RoomID = e.RoomID;
                room.BookingStatus = e.BookingStatus;
                bookedRoomList.Add(room);
            });
            return Json(new { data = bookedRoomList.ToList() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangeCheckinStatus()
        {
            var isCheckin = bool.Parse(Request["IsCheckin"]);
            if (isCheckin == true)
            {
                var id = int.Parse(Request["id"]);
                var customerID = Request["customerID"];
                var bookedRoom = db.BookedRooms.Find(id);
                bookedRoom.CheckinStatus = !bookedRoom.CheckinStatus;
                bookedRoom.CustomerID = customerID;
                db.SaveChanges();
                return Json(new { status = "success" , message = "Update check-in status successfully" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var id = int.Parse(Request["id"]);
                var bookedRoom = db.BookedRooms.Find(id);
                bookedRoom.CheckinStatus = !bookedRoom.CheckinStatus;
                bookedRoom.CustomerID = null;
                db.SaveChanges();
                return Json(new { status = "success",message = "Update non check-in status successfully"}, JsonRequestBehavior.AllowGet);
            }
            
        }
        [HttpPost]
        public ActionResult ChangePaymentStatus(int id)
        { 
            var bookedRoom = db.BookedRooms.Find(id);
            bookedRoom.PaymentStatus = !bookedRoom.PaymentStatus;
            db.SaveChanges();
            return Json(new { status = "success" }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult FindCustomerID(string id)
        {
            var customerID = db.Customers.Find(id);
            if (customerID != null)
            {
                return Json(new { status = "success", message = "Customer ID is available" }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { status = "unsuccess", message = "Customer ID is not available <br/> You need to add customer in Customer Table" }, 
                    JsonRequestBehavior.AllowGet);

            }
        }

        // Check Required room is available 
        public ActionResult hasAvailableRoom(string roomTypeID, DateTime checkIn, DateTime checkOut)
        {
            int rtypeID = int.Parse(roomTypeID);
            string status ="", message="";

            var roomByRoomTypeInBookedRoom =
                db.BookedRooms.Where( e=> e.Room.RoomTypeID == (rtypeID))
                .ToList();


            var allRoomsByRoomType = db.Rooms.Where(e => e.RoomTypeID == rtypeID).ToList();
            int? availableRoomID = 0;
            string availableRoomName = "Not Available";
            foreach(var room in allRoomsByRoomType)
            {
                bool flag = true; 
                foreach(var bookedRoom in roomByRoomTypeInBookedRoom)
                {
                    if(bookedRoom.CheckinDay.Date == checkIn.Date
                         && bookedRoom.CheckoutDay.Date == checkOut.Date)
                    {
                        if (room.ID == bookedRoom.RoomID) flag = false;
                    }
                    
                    else if(checkIn.Date >= bookedRoom.CheckinDay.Date && checkIn.Date <= bookedRoom.CheckoutDay.Date)
                    {
                        if (room.ID == bookedRoom.RoomID) flag = false;
                    }

                    else if (checkOut.Date >= bookedRoom.CheckinDay.Date && checkOut.Date <= bookedRoom.CheckoutDay.Date)
                    {
                        if (room.ID == bookedRoom.RoomID) flag = false;
                    }
                }
                if (flag == true)
                {
                    availableRoomID = room.ID;
                    availableRoomName = room.RoomName;
                    break;
                }
            }
            if (availableRoomID == 0 )
            {
                status = "unsuccess";
                message = "Please accept our sincere apology, This room is not available at this time, please try another booking time or other rooms, Thanks !";
            }
            else
            {
                status = "success";
                message = "Room is Available";
            }
 
            return Json(new { status =status, message = message,roomName = availableRoomName, roomID = availableRoomID},JsonRequestBehavior.AllowGet);
        }

        // Confirm cancel room request 
        public ActionResult ConfirmCancelRequest(int id) 
        {
            var room = db.BookedRooms.Find(id);
            room.BookingStatus = "Cancelled";
            if (room.CancelDay == null) room.CancelDay = DateTime.Now;
            db.SaveChanges();
            return Json(new { status = "success"}, JsonRequestBehavior.AllowGet);
        }

        // Cancel room page 
        public ActionResult CancelRoom()
        {
            return View(); 
        }

        // GetCancelRoomList 
        public ActionResult GetCancelRoomList()
        {
            var bookedRooms = db.BookedRooms.Where(e => e.BookingStatus == "Cancelled").ToList();
            List<BookedRoom_63132986> bookedRoomList = new List<BookedRoom_63132986>();
            bookedRooms.ForEach(e =>
            {
                BookedRoom_63132986 room = new BookedRoom_63132986();
                room.ID = e.ID;
                room.CustomerID = e.CustomerID == null ? "---  ---  ---" : e.CustomerID;
                room.UserName = e.UserInfo.UserName;
                room.RoomName = e.Room.RoomName;
                room.CancelDay = e.CancelDay.ToString();
                room.UserID = e.UserInfo.ID;
                room.RoomID = e.RoomID;
                room.BookingStatus = e.BookingStatus;
                bookedRoomList.Add(room);
            });
            return Json(new { data = bookedRoomList.ToList() }, JsonRequestBehavior.AllowGet);
        }
    }
}
