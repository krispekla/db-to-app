using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace PPPKProjekt.App_Code
{
    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Mobile { get; set; }
        public string DrivingLicence { get; set; }

        public Driver()
        {

        }
        public Driver(int id)
        {
            this.Id = id;
        }

        public override string ToString()
        {
            return Name + " " + Lastname;
        }
    }
}