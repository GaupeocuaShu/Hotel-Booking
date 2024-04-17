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

    public partial class HotelService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HotelService()
        {
            this.ServiceImages = new HashSet<ServiceImage>();
            this.ServiceUsages = new HashSet<ServiceUsage>();
        }
    
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is requried")]

        public string ServiceName { get; set; }
        [Required(ErrorMessage = "This field is requried")]

        public decimal Price { get; set; }
        [Required(ErrorMessage = "This field is requried")]

        public string ServiceDescription { get; set; }
        [Required(ErrorMessage = "This field is requried")]

        public string ServiceIcon { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceImage> ServiceImages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceUsage> ServiceUsages { get; set; }
    }
}