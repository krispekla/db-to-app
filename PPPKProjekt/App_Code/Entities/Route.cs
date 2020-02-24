namespace PPPKProjekt.App_Code.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Route")]
    public partial class Route
    {
        public int Id { get; set; }

        public int? TravelOrderID { get; set; }

        public DateTime? DateStart { get; set; }

        public DateTime? DateEnd { get; set; }

        [StringLength(50)]
        public string StartCoordinate { get; set; }

        [StringLength(50)]
        public string EndCoordinate { get; set; }

        public int? DistanceCrossed { get; set; }

        public int? AverageSpeed { get; set; }

        public int? FuelConsumption { get; set; }

        public virtual TravelOrder TravelOrder { get; set; }
    }
}
