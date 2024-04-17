using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_63132986.Models;
namespace Project_63132986.Controllers
{
    public class Dashboard_63132986Controller : Controller
    {
        private Project_63132986Entities1 db = new Project_63132986Entities1();

        // GET: Dashboard_63132986
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult getBookedRoomsNumber(DateTime date)
        {
            List<Dictionary<string, int>> roomNumberInEachType = new List<Dictionary<string, int>>(); 
            var rooms = db.BookedRooms.Where(e => DbFunctions.TruncateTime(e.BookDate) == DbFunctions.TruncateTime(date)).ToList();
            var roomTypes = db.RoomTypes.ToList(); 
            foreach(var roomType in roomTypes)
            {
                Dictionary<string, int> roomNumber = new Dictionary<string, int>();
                
                roomNumber.Add(roomType.RoomTypeName
                    , rooms.Where(e => e.Room.RoomTypeID == roomType.ID).Count());

                roomNumberInEachType.Add(roomNumber);
            }
            return Json(new { status = "success" ,rooms = roomNumberInEachType },JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult getBookedRoomsNumberInRange(DateTime startDate,DateTime endDate)
        {
            List<Dictionary<string, int>> data = new List<Dictionary<string, int>>();
            var bookedRooms = db.BookedRooms.ToList();
            for (DateTime counter = startDate; counter <= endDate; counter = counter.AddDays(1))
            {
                Dictionary<string, int> pair = new Dictionary<string, int>();
                pair.Add(counter.ToShortDateString(), 
                    bookedRooms.Where(e => e.BookDate.Date== counter.Date)
                    .Count());
                data.Add(pair);
            }
            return Json(new { status = "success", data = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult getRevenueInRange(DateTime startDate, DateTime endDate)
        {
            List<Dictionary<string, decimal>> data = new List<Dictionary<string, decimal>>();
            var invoices = db.Invoices.ToList();
            for (DateTime counter = startDate; counter <= endDate; counter = counter.AddDays(1))
            {
                Dictionary<string, decimal> pair = new Dictionary<string, decimal>();
                pair.Add(counter.ToShortDateString(),
                    invoices
                    .Where(e => e.BookedRoom.BookDate == counter.Date)
                    .Where(e => e.BookedRoom.PaymentStatus == true)
                    .Sum(e => e.TotalPrice));
                data.Add(pair);
            }
            return Json(new { status = "success", data = data }, JsonRequestBehavior.AllowGet);
        }

    }
}