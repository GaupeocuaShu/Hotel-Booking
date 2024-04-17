using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_63132986.Models
{
    public class BookedRoom_63132986
    {
        public int ID { get; set; }
        public int RoomID { get; set; }
        public bool CheckinType { get; set; }
        public string CheckinDay { get; set; }
        public string CheckoutDay { get; set; }
        public string CancelDay { get; set; }
        public int? UserID { get; set; }
        public string CustomerID { get; set; }
        public string BookDate { get; set; }
        public bool PaymentStatus { get; set; }
        public bool CheckinStatus { get; set; }
        public string BookingStatus { get; set; }
        public string RoomName { get; set; }
        public string UserName { get; set; }
    }
}