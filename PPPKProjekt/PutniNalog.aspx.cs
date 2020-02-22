using PPPKProjekt.App_Code;
using PPPKProjekt.App_Code.TableObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PPPKProjekt
{
    public partial class PutniNalog : System.Web.UI.Page
    {
        private IRepo repo = new Repo(ConfigurationManager.ConnectionStrings["cs"].ConnectionString);
        private static List<TravelOrder> _orders;
        private static List<Vehicle> _vehicles;
        private static List<Driver> _drivers;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var enumValues = System.Enum.GetValues(typeof(OrderStatus));

                foreach (var item in enumValues)
                {
                    ddlFilterOrders.Items.Add(item.ToString());
                    ddlOrderStatus.Items.Add(item.ToString());
                }
                ddlFilterOrders.Items.Add("All");
                ddlFilterOrders.SelectedIndex = 3;
                InitPage();
            }
        }

        private void InitPage()
        {
            _vehicles = repo.GetAllVehicles();
            _drivers = repo.GetAllDrivers();
            _orders = repo.GetAllTravelOrder();

            ddlSelectedOrder.DataSource = _orders;
            ddlSelectedOrder.DataTextField = "Id";
            ddlSelectedOrder.DataValueField = "Id";
            ddlSelectedOrder.DataBind();
            ddlVehicle.DataSource = _vehicles.Where(x => x.IsAvailable);
            ddlVehicle.DataBind();

            ddlDriver.DataSource = _drivers;
            ddlDriver.DataBind();

            SetTravelOrderList();
        }

        protected void ddlFilterOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTravelOrderList();
        }

        private void SetTravelOrderList()
        {
            gvTransportOrder.DataSource = null;
            gvTransportOrder.DataBind();
            List<TravelObjectGVFormatted> temp = new List<TravelObjectGVFormatted>();
            foreach (TravelOrder item in _orders)
            {
                temp.Add(new TravelObjectGVFormatted
                {
                    Id = item.Id,
                    OrderStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), item.OrderStatus.ToString()),
                    Vehicle = _vehicles.FirstOrDefault(x => x.Id == item.VehicleID).ToString(),
                    Driver = _drivers.FirstOrDefault(y => y.Id == item.VehicleID).ToString(),
                    VehicleStartKM = item.VehicleStartKM,
                    VehicleEndKM = item.VehicleEndKM,
                    Distance = item.Distance,
                    StartingCity = item.StartingCity,
                    FinishCity = item.FinishCity,
                    TotalDays = item.TotalDays,
                    TotalPrice = item.TotalPrice,
                    StartingDate = item.StartingDate
                });
            }
            if (ddlFilterOrders.SelectedIndex + 1 == 4)
            {
                gvTransportOrder.DataSource = temp;
            }
            else
            {
                gvTransportOrder.DataSource = temp.Where(x => (int)(OrderStatus)Enum.Parse(typeof(OrderStatus), x.OrderStatus.ToString(), true) == ddlFilterOrders.SelectedIndex + 1);
            }
            gvTransportOrder.DataBind();
        }

        protected void ddlSelectedOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayAllDrivers();
            ddlVehicle.DataSource = _vehicles;
            ddlVehicle.DataBind();
            ddlVehicle.SelectedIndex = _vehicles.SingleOrDefault(x => x.Id == _orders.SingleOrDefault(y => y.Id == int.Parse(ddlSelectedOrder.SelectedValue)).VehicleID).Id - 1;
            ddlDriver.SelectedIndex = _drivers.SingleOrDefault(x => x.Id == _orders.SingleOrDefault(y => y.Id == int.Parse(ddlSelectedOrder.SelectedValue)).DriverID).Id - 1;
        }

        private void DisplayAllDrivers()
        {
            ddlOrderStatus.SelectedIndex = _orders.SingleOrDefault(x => x.Id == int.Parse(ddlSelectedOrder.SelectedValue)).OrderStatus - 1;

            lbStartCity.Text = _orders[ddlSelectedOrder.SelectedIndex].StartingCity;
            lbFinishCity.Text = _orders[ddlSelectedOrder.SelectedIndex].FinishCity;
            lbTotalDays.Value = _orders[ddlSelectedOrder.SelectedIndex].TotalDays.ToString();
            tbStartingDate.Text = _orders[ddlSelectedOrder.SelectedIndex].StartingDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            SetTravelOrderList();
        }

        protected void btnEditToggle_Click(object sender, EventArgs e)
        {
            ToggleEditMode();
        }

        private void ToggleEditMode()
        {
            lblInfo.Text = "";
            ddlOrderStatus.Enabled = !ddlOrderStatus.Enabled;
            ddlVehicle.Enabled = !ddlVehicle.Enabled;
            ddlDriver.Enabled = !ddlDriver.Enabled;
            lbStartCity.Enabled = !lbStartCity.Enabled;
            lbFinishCity.Enabled = !lbFinishCity.Enabled;
            lbTotalDays.Disabled = !lbTotalDays.Disabled;
            tbStartingDate.Enabled = !tbStartingDate.Enabled;

            btnSave.Enabled = !btnSave.Enabled;
            btnAddOrder.Enabled = !btnAddOrder.Enabled;
            DisplayAllDrivers();
        }

        protected void btnOnlyAvailable_Click(object sender, EventArgs e)
        {
            ddlVehicle.DataSource = _vehicles.Where(x => x.IsAvailable);
            ddlVehicle.DataBind();
        }

        private void ClearAllFields()
        {
            ddlOrderStatus.SelectedIndex = 0;
            ddlVehicle.SelectedIndex = 0;
            ddlDriver.SelectedIndex = 0;
            lbStartCity.Text = "";
            lbFinishCity.Text = "";
            lbTotalDays.Value = "";
            tbStartingDate.Text = "";
            lblInfo.Text = "";
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }

        protected void btnAddOrder_Click(object sender, EventArgs e)
        {
            try
            {
                TravelOrder trOrder = new TravelOrder
                {
                    OrderStatus = ddlOrderStatus.SelectedIndex + 1,
                    VehicleID = (_vehicles.Where(x => x.IsAvailable)).ToList().ElementAt(ddlVehicle.SelectedIndex).Id,
                    DriverID = _drivers[ddlDriver.SelectedIndex + 1].Id,
                    VehicleStartKM = (_vehicles.Where(x => x.IsAvailable)).ToList().ElementAt(ddlVehicle.SelectedIndex).Milleage,
                    StartingCity = lbStartCity.Text,
                    FinishCity = lbFinishCity.Text,
                    TotalDays = int.Parse(lbTotalDays.Value),
                    StartingDate = DateTime.Parse(tbStartingDate.Text.ToString())
                };


                int result = repo.InsertTravelOrder(trOrder);
                if (result == 200)
                {
                    InitPage();
                    ToggleEditMode();
                    ClearAllFields();
                    lblInfo.Text = "Uspjesno dodan zapis";
                }
            }
            catch (SqlException ex)
            {
                lblInfo.Text = "SQLException: " + ex.Message;
            }
            catch (Exception ex)
            {
                lblInfo.Text = "Exception: " + ex.Message;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(ddlSelectedOrder.SelectedValue.ToString());

                if (id <= 0) return;

                int result = repo.DeleteTravelOrder(id);
                if (result == 200)
                {
                    InitPage();
                    ToggleEditMode();
                    ClearAllFields();
                    lblInfo.Text = "Uspjesno izbrisan zapis";
                }
            }
            catch (SqlException ex)
            {
                lblInfo.Text = "SQLException: " + ex.Message;
            }
            catch (Exception ex)
            {
                lblInfo.Text = "Exception: " + ex.Message;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                TravelOrder trOrder = new TravelOrder
                {
                    Id = int.Parse(ddlSelectedOrder.SelectedValue),
                    OrderStatus = ddlOrderStatus.SelectedIndex + 1,
                    VehicleID = (_vehicles.Where(x => x.IsAvailable)).ToList().ElementAt(ddlVehicle.SelectedIndex).Id,
                    DriverID = _drivers[ddlDriver.SelectedIndex + 1].Id,
                    VehicleStartKM = (_vehicles.Where(x => x.IsAvailable)).ToList().ElementAt(ddlVehicle.SelectedIndex).Milleage,
                    StartingCity = lbStartCity.Text,
                    FinishCity = lbFinishCity.Text,
                    TotalDays = int.Parse(lbTotalDays.Value),
                    StartingDate = DateTime.Parse(tbStartingDate.Text.ToString(), CultureInfo.GetCultureInfo("hr-HR"))
                };


                int result = repo.UpdateTravelOrder(trOrder);
                if (result == 200)
                {
                    InitPage();
                    ToggleEditMode();
                    ClearAllFields();
                    lblInfo.Text = "Uspjesno promijenjen zapis";
                }
            }
            catch (SqlException ex)
            {
                lblInfo.Text = "SQLException: " + ex.Message;
            }
            catch (Exception ex)
            {
                lblInfo.Text = "Exception: " + ex.Message;
            }
        }
    }
}