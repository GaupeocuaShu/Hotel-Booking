﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Project_63132986Entities1 : DbContext
    {
        public Project_63132986Entities1()
            : base("name=Project_63132986Entities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<HotelService> HotelServices { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomImageGallery> RoomImageGalleries { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<ServiceImage> ServiceImages { get; set; }
        public virtual DbSet<UserInfo> UserInfoes { get; set; }
        public virtual DbSet<BookedRoom> BookedRooms { get; set; }
        public virtual DbSet<ServiceUsage> ServiceUsages { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<SliderBannerImage> SliderBannerImages { get; set; }
    }
}