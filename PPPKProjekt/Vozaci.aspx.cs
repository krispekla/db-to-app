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
    public partial class Vozaci : System.Web.UI.Page
    {
        private IRepo repo = new SqlTextCommandsRepo(ConfigurationManager.ConnectionStrings["cs"].ConnectionString);
        private static List<Driver> drivers = new List<Driver>();
        private static bool _addNewMode = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAllDrivers();
                DisplayAllDrivers();
            }

        }

        private void GetAllDrivers()
        {
            ddlDrivers.AppendDataBoundItems = true;
            drivers.Clear();
            ddlDrivers.Items.Clear();
            drivers = repo.GetAllDrivers();
            ddlDrivers.DataSource = drivers;
            ddlDrivers.DataTextField = "Name";
            ddlDrivers.DataBind();
        }

        protected void ddlDrivers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drivers.Count <= 0) return;
            DisplayAllDrivers();
        }

        private void DisplayAllDrivers()
        {
            lbName.Text = drivers[ddlDrivers.SelectedIndex].Name;
            lbLastname.Text = drivers[ddlDrivers.SelectedIndex].Lastname;
            lbMobile.Text = drivers[ddlDrivers.SelectedIndex].Mobile;
            lbDriving.Text = drivers[ddlDrivers.SelectedIndex].DrivingLicence;

            gvDrivers.DataSource = drivers;
            gvDrivers.DataBind();
        }

        protected void btnEditToggle_Click(object sender, EventArgs e)
        {
            ToggleEditMode();
        }

        private void ToggleEditMode()
        {
            lblInfo.Text = "";
            lbName.Enabled = !lbName.Enabled;
            lbLastname.Enabled = !lbLastname.Enabled;
            lbMobile.Enabled = !lbMobile.Enabled;
            lbDriving.Enabled = !lbDriving.Enabled;
            btnSave.Enabled = !btnSave.Enabled;
            btnAddDriver.Enabled = !btnAddDriver.Enabled;
            DisplayAllDrivers();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Driver driver = new Driver();
                driver.Id = drivers[ddlDrivers.SelectedIndex].Id;
                driver.Name = lbName.Text;
                driver.Lastname = lbLastname.Text;
                driver.Mobile = lbMobile.Text;
                driver.DrivingLicence = lbDriving.Text;


                int result = repo.UpdateDriver(driver);
                if (result == 200)
                {
                    GetAllDrivers();
                    ToggleEditMode();
                    lblInfo.Text = "Uspjesno promijenjeno";
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

        protected void btnAddDriver_Click(object sender, EventArgs e)
        {
            try
            {
                Driver driver = new Driver();
                driver.Name = lbName.Text;
                driver.Lastname = lbLastname.Text;
                driver.Mobile = lbMobile.Text;
                driver.DrivingLicence = lbDriving.Text;


                int result = repo.InsertDriver(driver);
                if (result == 200)
                {
                    GetAllDrivers();
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

        private void ClearAllFields()
        {
            lbName.Text = "";
            lbLastname.Text = "";
            lbMobile.Text = "";
            lbDriving.Text = "";
            lblInfo.Text = "";
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = drivers[ddlDrivers.SelectedIndex].Id;

                if (id <= 0) return;

                int result = repo.DeleteDriver(id);
                if (result == 200)
                {
                    GetAllDrivers();
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
    }
}