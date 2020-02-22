using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPPKProjekt.App_Code
{
    public class VehicleFuel
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public int Litres { get; set; }
        public decimal? FuelPrice { get; set; }
    }
}