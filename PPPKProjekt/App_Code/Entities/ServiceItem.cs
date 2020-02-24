namespace PPPKProjekt.App_Code.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ServiceItem")]
    public partial class ServiceItem
    {
        public int Id { get; set; }

        public int ServiceID { get; set; }

        [Required]
        [StringLength(50)]
        public string ServiceName { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public virtual Service Service { get; set; }
    }
}
