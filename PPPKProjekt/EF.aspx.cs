using PPPKProjekt.App_Code.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PPPKProjekt
{
    public partial class EF : System.Web.UI.Page
    {
        private static List<Vehicle> _vehicles = new List<Vehicle>();
        private static List<Service> _services = new List<Service>();
        private static List<ServiceItem> _serviceItems = new List<ServiceItem>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitVehicles();
            }
        }

        private void InitVehicles()
        {
            using (var context = new PPPKContext())
            {
                _vehicles = context.Vehicle.ToList();
                _services = context.Service.ToList();
                _serviceItems = context.ServiceItem.ToList();

                gvVehicles.DataSource = _vehicles;
                gvVehicles.DataBind();

                ddlVehicle.DataSource = _vehicles;
                ddlVehicle.DataTextField = "Plate";
                ddlVehicle.DataValueField = "Id";
                ddlVehicle.DataBind();
                ddlVehicle.SelectedIndex = 0;

                ddlService.AppendDataBoundItems = false;
                ddlService.DataSource = null;
                ddlService.DataBind();

                ddlService.DataSource = _services.Where(x => x.VehicleID == _vehicles.ElementAt(ddlVehicle.SelectedIndex).Id).ToList();
                ddlService.DataValueField = "Id";
                ddlService.DataTextField = "Date";
                ddlService.DataBind();

                ddlServiceItem.AppendDataBoundItems = false;
                ddlServiceItem.DataSource = null;
                ddlServiceItem.DataBind();

                if (ddlService.Items.Count > 0)
                {

                    ddlServiceItem.DataSource = _serviceItems.Where(x => x.ServiceID == _services.ElementAt(ddlService.SelectedIndex).Id).ToList();
                    ddlServiceItem.DataValueField = "Id";
                    ddlServiceItem.DataTextField = "ServiceName";
                    ddlServiceItem.DataBind();

                    gvServiceItems.DataSource = _serviceItems.Where(x => x.ServiceID == _services.ElementAt(ddlService.SelectedIndex).Id).ToList();
                    gvServiceItems.DataBind();
                    SetFields();
                }
                else
                {
                    ddlServiceItem.AppendDataBoundItems = false;
                    ddlServiceItem.DataSource = null;
                    ddlServiceItem.DataBind();
                    gvServiceItems.DataSource = null;
                    gvServiceItems.DataBind();
                    ClearFields();
                }
            }
        }



        protected void ddlVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlService.AppendDataBoundItems = false;
            ddlServiceItem.AppendDataBoundItems = false;
            ddlService.DataSource = null;
            ddlServiceItem.DataSource = null;
            ddlServiceItem.Items.Clear();
            ddlService.DataBind();
            ddlServiceItem.DataBind();

            ddlService.DataSource = _services.Where(x => x.VehicleID == _vehicles.ElementAt(ddlVehicle.SelectedIndex).Id).ToList();
            ddlService.DataValueField = "Id";
            ddlService.DataTextField = "Date";
            ddlService.DataBind();

            if (ddlService.Items.Count > 0)
            {
                ddlService.SelectedIndex = 0;
                int serviceId = _services.FirstOrDefault(y => y.VehicleID == _vehicles.ElementAt(ddlVehicle.SelectedIndex).Id).Id;
                ddlServiceItem.DataSource = _serviceItems.Where(x => x.ServiceID == serviceId).ToList();
                ddlServiceItem.DataValueField = "Id";
                ddlServiceItem.DataTextField = "ServiceName";
                ddlServiceItem.DataBind();

                gvServiceItems.DataSource = _serviceItems.Where(x => x.ServiceID == serviceId).ToList();
                gvServiceItems.DataBind();
            }
            else
            {
                ddlServiceItem.AppendDataBoundItems = false;
                ddlServiceItem.DataSource = null;
                ddlServiceItem.DataBind();
                gvServiceItems.DataSource = null;
                gvServiceItems.DataBind();
                ClearFields();

            }
        }

        protected void ddlService_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlServiceItem.AppendDataBoundItems = false;
            ddlServiceItem.DataSource = null;
            ddlServiceItem.Items.Clear();
            ddlServiceItem.DataBind();

            gvServiceItems.Columns.Clear();
            gvServiceItems.DataSource = null;
            gvServiceItems.DataBind();

            int serviceId = _services.FirstOrDefault(y => y.VehicleID == _vehicles.ElementAt(ddlVehicle.SelectedIndex).Id).Id;
            ddlServiceItem.DataSource = _serviceItems.Where(x => x.ServiceID == int.Parse(ddlService.SelectedValue)).ToList();
            ddlServiceItem.DataValueField = "Id";
            gvServiceItems.DataSource = _serviceItems.Where(x => x.ServiceID == int.Parse(ddlService.SelectedValue)).ToList();

            ddlServiceItem.DataBind();
            gvServiceItems.DataBind();
        }

        protected void ddlServiceItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFields();
        }

        private void SetFields()
        {
            ServiceItem temp = _serviceItems.FirstOrDefault(x => x.Id == int.Parse(ddlServiceItem.SelectedValue));
            if (temp == null)
            {
                ClearFields();
                return;
            }
            txtServiceName.Text = temp.ServiceName;
            txtPrice.Text = temp.Price.ToString();
        }

        private void ClearFields()
        {
            txtServiceName.Text = "";
            txtPrice.Text = "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (var context = new PPPKContext())
            {
                ServiceItem s = context.ServiceItem.Find(int.Parse(ddlServiceItem.SelectedValue));

                s.Price = Decimal.Parse(txtPrice.Text);
                s.ServiceName = txtServiceName.Text;
                s.ServiceID = int.Parse(ddlService.SelectedValue);

                context.SaveChanges();
                context.Dispose();
            }
            InitVehicles();
        }

        protected void btnAddService_Click(object sender, EventArgs e)
        {
            using (var context = new PPPKContext())
            {
                context.ServiceItem.Add(new ServiceItem
                {
                    Price = Decimal.Parse(txtPrice.Text),
                    ServiceName = txtServiceName.Text,
                    ServiceID = int.Parse(ddlService.SelectedValue)
                });
                context.SaveChanges();
                context.Dispose();
            }
            InitVehicles();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            using (var context = new PPPKContext())
            {
                int deleteId = int.Parse(ddlServiceItem.SelectedValue);
                ServiceItem temp = context.ServiceItem.FirstOrDefault(x => x.Id == deleteId);
                context.ServiceItem.Remove(temp);
                context.SaveChanges();
                context.Dispose();
            }
            InitVehicles();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        protected void btnGenerateHTML_Click(object sender, EventArgs e)
        {
            string fileName = @"D:\reportVehicle.html";

            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    w.WriteLine("<!DOCTYPE html>");
                    w.WriteLine("<html>");
                    w.WriteLine("<head>");
                    w.WriteLine("<title>Report</title>");
                    w.WriteLine("</head>");
                    w.WriteLine("<body>");
                    w.WriteLine("<H1>Report vozila</H1>");
                    w.WriteLine("<br>");

                    w.WriteLine("<div>Id vehicle: " + ddlVehicle.SelectedValue);
                    w.WriteLine("</div>");
                    w.WriteLine("<br>");

                    w.WriteLine("<div>Current milles crossed: " + _vehicles.FirstOrDefault(x => x.Id == int.Parse(ddlVehicle.SelectedValue)).Milleage.ToString());
                    w.WriteLine("</div>");
                    w.WriteLine("<br>");
                    w.WriteLine("<div>Average speed: -");
                    w.WriteLine("</div>");

                    foreach (Service item in _services.Where(x => x.VehicleID == int.Parse(ddlVehicle.SelectedValue)).ToList())
                    {
                        w.WriteLine("<h3>Service - " + item.Date.ToString());
                        w.WriteLine("</h3>");
                        w.WriteLine("<br>");

                        foreach (ServiceItem it in _serviceItems.Where(x => x.ServiceID == item.Id).ToList())
                        {
                            w.WriteLine("<span>Name: " + it.ServiceName);
                            w.WriteLine("</span>");
                            w.WriteLine("<br>");

                            w.WriteLine("<span>Price: " + it.Price.ToString());
                            w.WriteLine("</span>");
                            w.WriteLine("<br>");

                        }
                        w.WriteLine("<br>");
                        w.WriteLine("<br>");
                        w.WriteLine("<br>");

                    }

                    w.WriteLine("</body>");
                    w.WriteLine("</html>");
                }
            }
        }

        protected void btnAddNewService_Click(object sender, EventArgs e)
        {
            using (var context = new PPPKContext())
            {
                context.Service.Add(new Service
                {
                    Date = DateTime.Now,
                    VehicleID = int.Parse(ddlVehicle.SelectedValue)
                });
                context.SaveChanges();
                context.Dispose();
            }
            InitVehicles();
        }
    }
}