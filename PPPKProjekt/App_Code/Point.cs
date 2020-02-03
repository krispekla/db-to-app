using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PPPKProjekt.App_Code
{
    public class Point
    {
        public int Id { get; set; }
        public String City { get; set; }
        public String Street { get; set; }
        public int HouseNumber { get; set; }

        public Point()
        {

        }
        public Point(int id)
        {
            this.Id = id;
        }
    }
}