namespace PPPKProjekt.App_Code.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vehicle")]
    public partial class Vehicle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehicle()
        {
            Service = new HashSet<Service>();
            TravelOrder = new HashSet<TravelOrder>();
        }

        public int Id { get; set; }

        public int VehicleTypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string Plate { get; set; }

        [Required]
        [StringLength(50)]
        public string Brand { get; set; }

        [Column(TypeName = "date")]
        public DateTime Production_Year { get; set; }

        public bool? Vehicle_Status { get; set; }

        public long Milleage { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Service> Service { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TravelOrder> TravelOrder { get; set; }

        public virtual VehicleType VehicleType { get; set; }
    }
}
