using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPPKProjekt.App_Code
{
    public class ServiceItem
    {
        public int Id { get; set; }
        public int ServiceID { get; set; }
        public String ServiceName { get; set; }
        public decimal? Price { get; set; }
    }
}