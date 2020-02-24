namespace PPPKProjekt.App_Code.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bills
    {
        public int Id { get; set; }

        public int TravelOrder { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public virtual TravelOrder TravelOrder1 { get; set; }
    }
}
