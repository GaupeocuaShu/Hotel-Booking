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
    using System.ComponentModel.DataAnnotations;

    public partial class Employee
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is requried")]

        public string EmployeeName { get; set; }
        [Required(ErrorMessage = "This field is requried")]

        public System.DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "This field is requried")]

        public string Email { get; set; }
        [Required(ErrorMessage = "This field is requried")]

        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "This field is requried")]

        public bool Sex { get; set; }
        [Required(ErrorMessage = "This field is requried")]

        public string EmployeeAddress { get; set; }
        public Nullable<int> PositionID { get; set; }
        public string Avatar { get; set; }

        [Required(ErrorMessage = "This field is requried")]

        public virtual Position Position { get; set; }
    }
}
