using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPPKProjekt.App_Code
{
    public interface IRepo
    {
        List<Driver> GetAllDrivers();
        int InsertDriver(Driver driver);
        int UpdateDriver(Driver driver);
        int DeleteDriver(int id);

        List<Vehicle> GetAllVehicles();
        int InsertVehicle(Vehicle vehicle);
        int UpdateVehicle(Vehicle vehicle);
        int DeleteVehicle(int id);
        List<TravelOrder> GetAllTravelOrder();
    }
}