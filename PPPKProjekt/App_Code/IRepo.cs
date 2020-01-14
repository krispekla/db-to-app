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
    }
}