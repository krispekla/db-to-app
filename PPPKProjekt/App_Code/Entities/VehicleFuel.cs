namespace PPPKProjekt.App_Code.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VehicleFuel")]
    public partial class VehicleFuel
    {
        public int Id { get; set; }

        public int UserID { get; set; }

        public DateTime Date { get; set; }

        public int Litres { get; set; }

        [Column(TypeName = "money")]
        public decimal FuelPrice { get; set; }

        public virtual Users Users { get; set; }
    }
}
