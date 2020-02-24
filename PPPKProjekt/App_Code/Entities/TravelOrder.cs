namespace PPPKProjekt.App_Code.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TravelOrder")]
    public partial class TravelOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TravelOrder()
        {
            Bills = new HashSet<Bills>();
            Route = new HashSet<Route>();
        }

        public int Id { get; set; }

        public int? OrderStatus { get; set; }

        public int? VehicleID { get; set; }

        public int? UserID { get; set; }

        public long? Vehicle_km_start { get; set; }

        public long? Vehicle_km_end { get; set; }

        public int? Distance_crossed { get; set; }

        [StringLength(50)]
        public string Starting_city { get; set; }

        [StringLength(50)]
        public string Finish_city { get; set; }

        [Column(TypeName = "money")]
        public decimal? Total_price { get; set; }

        public int? Total_days { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

        public DateTime? StartingDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bills> Bills { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Route> Route { get; set; }

        public virtual Users Users { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
