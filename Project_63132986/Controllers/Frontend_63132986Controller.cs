using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_63132986.Models;
using System.Data.Entity;
namespace Project_63132986.Controllers
{
    public class Frontend_63132986Controller : Controller
    {
        private Project_63132986Entities1 db = new Project_63132986Entities1();

        public bool isBooked(int roomID)
        {
            var bookedRooms = db.BookedRooms.Select(e=>e.RoomID).ToList(); 
            foreach(var id in bookedRooms)
            {
                if (id == roomID) return true; 
            }
            return false;
        }

        public List<Room> getDistinctRoom()
        {
            var roomTypes = db.RoomTypes.Select(e => e.ID).ToList();
            var rooms = db.Rooms.Include(e => e.RoomType).ToList();
            List<Room> exclusiveRoom = new List<Room>(); 
            for(int i = 0; i < roomTypes.Count(); i++)
            {
                for(int j = 0; j < rooms.Count(); j++)
                {
                    if(rooms[j].RoomTypeID == roomTypes[i])
                    {                      
                        exclusiveRoom.Add(rooms[j]);
                        break;
                    } 
                }
            }
            return exclusiveRoom;
        }

        // GET: Frontend_63132986
        public ActionResult Index()
        {

            var banners = db.SliderBannerImages.ToList();
            var rooms = getDistinctRoom();
            var services = db.HotelServices.ToList();
            dynamic roomAndServices = new ExpandoObject();
            roomAndServices.Rooms = rooms;
            roomAndServices.Services = services;
            roomAndServices.Banners = banners;
            return View(roomAndServices);
        }

        public ActionResult NotFound()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BookFormSend(BookedRoom bookedRoom)
        {
            return Json(new { data = bookedRoom }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FrontendBookedRoom()
        {
            return View();
        }


        public ActionResult Profile()
        {
            return View(); 
        }

        public ActionResult GetList(string id)
        {
            int userID = int.Parse(id); 
            var bookedRooms = db.BookedRooms.Where(e=> e.UserID == userID).ToList();
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
                /*
                room.BookDate = e.BookDate.ToString("MM-dd-yyyy");
                room.CheckinType = e.CheckinType;
                room.CheckoutDay = e.CheckoutDay.ToString("MM-dd-yyyy");
                room.CheckinStatus = e.CheckinStatus;
                */
                bookedRoomList.Add(room);
            });
            return Json(new { data = bookedRoomList.ToList() }, JsonRequestBehavior.AllowGet);
        }

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
        public ActionResult CancelRoomRequest(string id,DateTime date)
        {

            int bookID = int.Parse(id);
            var room = db.BookedRooms.Find(bookID);
            room.BookingStatus = "Pending";
            room.CancelDay = date;
            db.SaveChanges();
            return Json(new { status = "success",
                message= "Your Cancel Booked Room Request Has Been Sent<br/>.Your booking room status will be updated in 24 hours ",
                bookingStatus ="Pending",
                
            }, JsonRequestBehavior.AllowGet);

        }
    }
}