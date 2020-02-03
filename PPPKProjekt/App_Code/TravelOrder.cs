using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPPKProjekt.App_Code
{
    public class TravelOrder
    {
        public int Id { get; set; }
        public int OrderStatus { get; set; }
        public Vehicle Vehicle { get; set; }
        public Driver Driver { get; set; }
        public int VehicleStartKM { get; set; }
        public int? VehicleEndKM { get; set; }
        public int? Distance { get; set; }
        public  Point StartingPoint { get; set; }
        public  Point FinnishPoint { get; set; }
        public int TotalDays { get; set; }
        public  decimal? TotalPrice { get; set; }
        public DateTime StartingDate { get; set; }
        public List<Status> Statuses { get; set; }
    }
}

