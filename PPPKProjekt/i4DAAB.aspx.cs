using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using PPPKProjekt.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PPPKProjekt
{
    public partial class i4DAAB : System.Web.UI.Page
    {
        private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        private static SqlDatabase db = new SqlDatabase(cs);
        private static List<TravelOrder> orders = new List<TravelOrder>();
        private static List<Route> routes = new List<Route>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        private void InitPage()
        {
            string sqlCommand = "SELECT * FROM TravelOrder; SELECT * FROM Route";
            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);

            DataSet ds = new DataSet();
            ds = db.ExecuteDataSet(dbCommand);
            ds.Tables[0].TableName = "TravelOrder";
            ds.Tables[1].TableName = "Route";

            orders.Clear();
            routes.Clear();

            foreach (DataRow rowDrzava in ds.Tables["TravelOrder"].Rows)
            {
                TravelOrder tempOrder = new TravelOrder();

                tempOrder.Id = int.Parse(rowDrzava["Id"].ToString());
                tempOrder.OrderStatus = int.Parse(rowDrzava["OrderStatus"].ToString());
                tempOrder.VehicleID = int.Parse(rowDrzava["VehicleID"].ToString());
                tempOrder.DriverID = int.Parse(rowDrzava["UserID"].ToString());
                tempOrder.VehicleStartKM = long.Parse(rowDrzava["Vehicle_km_start"].ToString());
                tempOrder.VehicleEndKM = Utils.ParseNullable<long>(rowDrzava["Vehicle_km_end"].ToString());
                tempOrder.StartingCity = rowDrzava["Starting_city"].ToString();
                tempOrder.FinishCity = rowDrzava["Finish_city"].ToString();
                tempOrder.TotalDays = int.Parse(rowDrzava["Total_days"].ToString());
                tempOrder.StartingDate = DateTime.Parse(rowDrzava["StartingDate"].ToString());
                tempOrder.StartingDate = DateTime.Parse(rowDrzava["StartingDate"].ToString());

                orders.Add(tempOrder);
            }
            foreach (DataRow rowRoute in ds.Tables["Route"].Rows)
            {
                Route tempRoute = new Route();
                tempRoute.Id = int.Parse(rowRoute["Id"].ToString());
                tempRoute.TravelOrder = orders.Find(x => x.Id == int.Parse(rowRoute["TravelOrderID"].ToString()));
                tempRoute.DateStart = DateTime.Parse(rowRoute["DateStart"].ToString());
                tempRoute.DateEnd = DateTime.Parse(rowRoute["DateEnd"].ToString());
                tempRoute.StartCoordinate = rowRoute["StartCoordinate"].ToString();
                tempRoute.EndCoordinate = rowRoute["EndCoordinate"].ToString();
                tempRoute.DistanceCrossed = int.Parse(rowRoute["DistanceCrossed"].ToString());
                tempRoute.AvgSpeed = int.Parse(rowRoute["AverageSpeed"].ToString());
                tempRoute.FuelConsumption = int.Parse(rowRoute["FuelConsumption"].ToString());

                routes.Add(tempRoute);
            }

            SetField();
        }

        private void SetField()
        {
            ddlTravelOrders.Items.Clear();
            ddlRoutes.Items.Clear();
            ddlTravelOrders.DataSource = orders;
            ddlTravelOrders.DataBind();

            ddlRoutes.DataSource = routes.Where(x => x.TravelOrder.Id == int.Parse(ddlTravelOrders.SelectedValue)).ToList();
            ddlRoutes.DataBind();
            SetFieldsValue();
        }

        private void SetFieldsValue()
        {
            if (String.IsNullOrEmpty(ddlRoutes.SelectedValue))
            {
                ClearFieldsValue();
                return;
            }
            txtStartTime.Text = routes.SingleOrDefault(x => x.Id == int.Parse(ddlRoutes.SelectedValue.ToString())).DateStart.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            txtEndTime.Text = routes.SingleOrDefault(x => x.Id == int.Parse(ddlRoutes.SelectedValue.ToString())).DateEnd.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            txtStartCood.Text = routes.SingleOrDefault(x => x.Id == int.Parse(ddlRoutes.SelectedValue.ToString())).StartCoordinate;
            txtEndCood.Text = routes.SingleOrDefault(x => x.Id == int.Parse(ddlRoutes.SelectedValue.ToString())).EndCoordinate;
            txtDistance.Value = routes.SingleOrDefault(x => x.Id == int.Parse(ddlRoutes.SelectedValue.ToString())).DistanceCrossed.ToString();
            txtAvgspeed.Value = routes.SingleOrDefault(x => x.Id == int.Parse(ddlRoutes.SelectedValue.ToString())).AvgSpeed.ToString();
            txtFuelConsumption.Value = routes.SingleOrDefault(x => x.Id == int.Parse(ddlRoutes.SelectedValue.ToString())).FuelConsumption.ToString();
        }

        private void ClearFieldsValue()
        {
            txtStartTime.Text = "";
            txtEndTime.Text = "";
            txtStartCood.Text = "";
            txtEndCood.Text = "";
            txtDistance.Value = "";
            txtAvgspeed.Value = "";
            txtFuelConsumption.Value = "";
        }

        protected void ddlTravelOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlRoutes.Items.Clear();
            ddlRoutes.DataSource = routes.Where(x => x.TravelOrder.Id == int.Parse(ddlTravelOrders.SelectedValue)).ToList();
            ddlRoutes.DataBind();
            SetFieldsValue();
        }

        protected void ddlRoutes_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFieldsValue();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFieldsValue();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (ddlRoutes.SelectedIndex == -1) return;

            int routeId = int.Parse(ddlRoutes.SelectedValue);

            if (db.ExecuteNonQuery("deleteRoute", int.Parse(ddlRoutes.SelectedValue)) > 0)
            {
                lblInfo.Text = "Ruta uspjesno obrisana";
                InitPage();
            }
            else
                lblInfo.Text = "Brisanje rute nije uspjelo";


        }

        protected void btnAddOrder_Click(object sender, EventArgs e)
        {

            if (db.ExecuteNonQuery("insertRoute",
                int.Parse(ddlTravelOrders.SelectedValue),
                DateTime.Parse(txtStartTime.Text.ToString()),
                DateTime.Parse(txtEndTime.Text.ToString()),
                txtStartCood.Text,
                txtEndCood.Text,
                int.Parse(txtDistance.Value),
                int.Parse(txtAvgspeed.Value),
                int.Parse(txtFuelConsumption.Value)
                ) > 0)
            {
                lblInfo.Text = "Ruta uspjesno dodana";
                InitPage();
            }
            else
                lblInfo.Text = "Dodavanje rute nije uspjelo";

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ddlRoutes.SelectedIndex != -1)
            {
                int routeId = int.Parse(ddlRoutes.SelectedValue);

                if (db.ExecuteNonQuery("updateRoute",
                    int.Parse(ddlRoutes.SelectedValue),
                    DateTime.Parse(txtStartTime.Text.ToString()),
                    DateTime.Parse(txtEndTime.Text.ToString()),
                    txtStartCood.Text,
                    txtEndCood.Text,
                    int.Parse(txtDistance.Value),
                    int.Parse(txtAvgspeed.Value),
                    int.Parse(txtFuelConsumption.Value)
                    ) > 0)
                {
                    lblInfo.Text = "Ruta uspjesno updateana";
                    InitPage();
                }
                else
                    lblInfo.Text = "Update rute nije uspjelo";

            }
        }
    }
}