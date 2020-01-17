using PPPKProjekt.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PPPKProjekt
{
    public partial class Vozila : System.Web.UI.Page
    {
        private IRepo repo = new Repo(ConfigurationManager.ConnectionStrings["cs"].ConnectionString);
        private static List<Vehicle> vehicles = new List<Vehicle>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAllVehicles();
                DisplayAllVehicles();
                FillVehicleType();
            }
        }

        private void FillVehicleType()
        {
            ddlType.DataSource = Enum.GetNames(typeof(VehicleType));
            ddlType.DataBind();
        }

        private void GetAllVehicles()
        {
            ddlVehicles.AppendDataBoundItems = true;
            vehicles.Clear();
            ddlVehicles.Items.Clear();
            vehicles = repo.GetAllVehicles();
            ddlVehicles.DataSource = vehicles;
            ddlVehicles.DataTextField = "Plate";
            ddlVehicles.DataBind();
        }

        private void DisplayAllVehicles()
        {
            ddlType.SelectedIndex =  vehicles[ddlVehicles.SelectedIndex].VehicleTypeId - 1;
            lbPlate.Text = vehicles[ddlVehicles.SelectedIndex].Plate;
            lbBrand.Text = vehicles[ddlVehicles.SelectedIndex].Brand;
            lbYear.Value = ((DateTime)vehicles[ddlVehicles.SelectedIndex].Year).ToString("yyyy");
            cbIsAvailable.Enabled = vehicles[ddlVehicles.SelectedIndex].IsAvailable;
            lbMilleage.Text = vehicles[ddlVehicles.SelectedIndex].Milleage.ToString();

            gvVehicles.DataSource = vehicles;
            gvVehicles.DataBind();
        }

        protected void ddlVehicles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (vehicles.Count <= 0) return;
            DisplayAllVehicles();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }

        private void ClearAllFields()
        {
            ddlType.SelectedIndex = 1;
            lbPlate.Text = "";
            lbBrand.Text = "";
            lbYear.Value = "";
            cbIsAvailable.Checked = true;
            lbMilleage.Text = ""; 
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = vehicles[ddlVehicles.SelectedIndex].Id;

                if (id <= 0) return;

                int result = repo.DeleteVehicle(id);
                if (result == 200)
                {
                    GetAllVehicles();
                    ToggleEditMode();
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

        protected void btnEditToggle_Click(object sender, EventArgs e)
        {
            ToggleEditMode();
        }

        private void ToggleEditMode()
        {
            lblInfo.Text = "";
            ddlType.Enabled = !ddlType.Enabled;
            lbPlate.Enabled = !lbPlate.Enabled;
            lbBrand.Enabled = !lbBrand.Enabled;
            lbYear.Disabled = !lbYear.Disabled;
            cbIsAvailable.Enabled = !cbIsAvailable.Enabled;
            lbMilleage.Enabled = !lbMilleage.Enabled;
            btnSave.Enabled = !btnSave.Enabled;
            btnAddVehicle.Enabled = !btnAddVehicle.Enabled;
            DisplayAllVehicles();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Vehicle vehicle = new Vehicle();
                vehicle.Id = vehicles[ddlVehicles.SelectedIndex].Id;
                vehicle.VehicleTypeId = ddlType.SelectedIndex;
                vehicle.Plate = lbPlate.Text;
                vehicle.Brand = lbBrand.Text;
                vehicle.Year = new DateTime(Convert.ToInt32(lbYear.Value), 1, 1);
                vehicle.IsAvailable = cbIsAvailable.Checked;
                vehicle.Milleage = long.Parse(lbMilleage.Text);

                int result = repo.UpdateVehicle(vehicle);
                if (result == 200)
                {
                    GetAllVehicles();
                    ToggleEditMode();
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

        protected void btnAddVehicle_Click(object sender, EventArgs e)
        {
            try
            {
                Vehicle vehicle = new Vehicle();
                vehicle.VehicleTypeId = ddlType.SelectedIndex;
                vehicle.Plate = lbPlate.Text;
                vehicle.Brand = lbBrand.Text;
                vehicle.Year = new DateTime(Convert.ToInt32(lbYear.Value), 1, 1);
                vehicle.IsAvailable = cbIsAvailable.Checked;
                vehicle.Milleage = long.Parse(lbMilleage.Text);

                int result = repo.InsertVehicle(vehicle);
                if (result == 200)
                {
                    GetAllVehicles();
                    ToggleEditMode();
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
    }
}