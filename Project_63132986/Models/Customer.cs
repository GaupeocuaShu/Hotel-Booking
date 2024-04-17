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

    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.BookedRooms = new HashSet<BookedRoom>();
        }
        [Required(ErrorMessage = "This field is requried")]

        public string ID { get; set; }
        [Required(ErrorMessage = "This field is requried")]

        public string CustomerName { get; set; }
        [Required(ErrorMessage = "This field is requried")]

        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "This field is requried")]

        public System.DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "This field is requried")]

        public string CustomerAddress { get; set; }
        [Required(ErrorMessage = "This field is requried")]

        public bool Sex { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookedRoom> BookedRooms { get; set; }
    }
}
