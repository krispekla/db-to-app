using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPPKProjekt.App_Code.TableObjects
{
    public class TravelObjectGVFormatted
    {
        public int Id { get; set; }
        public String Driver { get; set; }
        public String Vehicle { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public long VehicleStartKM { get; set; }
        public long? VehicleEndKM { get; set; }
        public int? Distance { get; set; }
        public String StartingCity { get; set; }
        public String FinishCity { get; set; }
        public int? TotalDays { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime StartingDate { get; set; }
    }
}