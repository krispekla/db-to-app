using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPPKProjekt.App_Code
{
    enum VehicleType
    {
        Auto = 1,
        Motor = 2,
        Zrakoplov = 3,
        Autobus = 4
    }

    public class Vehicle
    {
        public int Id { get; set; }
        public int VehicleTypeId { get; set; }
        public String Plate { get; set; }
        public String Brand { get; set; }
        public DateTime? Year { get; set; }
        public bool IsAvailable { get; set; }
        public long Milleage { get; set; }

        public Vehicle()
        {

        }
        public Vehicle(int id)
        {
            this.Id = id;
        }

        public override string ToString()
        {
            return Brand + " " + Plate;
        }
    }
}