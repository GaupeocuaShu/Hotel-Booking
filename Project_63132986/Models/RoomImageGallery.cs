//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project_63132986.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RoomImageGallery
    {
        public int ID { get; set; }
        public int RoomID { get; set; }
        public string RoomImage { get; set; }
    
        public virtual Room Room { get; set; }
    }
}
