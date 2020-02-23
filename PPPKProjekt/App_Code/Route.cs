using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPPKProjekt.App_Code
{
    public class Route
    {
        public int Id { get; set; }
        public TravelOrder TravelOrder { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public String StartCoordinate { get; set; }
        public String EndCoordinate { get; set; }
        public int DistanceCrossed { get; set; }
        public int AvgSpeed { get; set; }
        public int FuelConsumption { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}