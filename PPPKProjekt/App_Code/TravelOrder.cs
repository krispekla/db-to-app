using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPPKProjekt.App_Code
{
    public enum OrderStatus
    {
        Open = 1,
        Closed = 2,
        Future = 3
    }

    public class TravelOrder
    {
        public int Id { get; set; }
        public int OrderStatus { get; set; }
        public int VehicleID { get; set; }
        public int DriverID { get; set; }
        public long VehicleStartKM { get; set; }
        public long? VehicleEndKM { get; set; }
        public int? Distance { get; set; }
        public String StartingCity { get; set; }
        public String FinishCity { get; set; }
        public int? TotalDays { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime StartingDate { get; set; }



        public override string ToString()
        {
            return Id.ToString();
        }
    }
}

