using PPPKProjekt.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PPPKProjekt
{
    public partial class i3XML : System.Web.UI.Page
    {
        private static string cs = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        private static List<TravelOrder> orders = new List<TravelOrder>();
        private static List<Route> routes = new List<Route>();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnImportXML_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            ds.ReadXml(MapPath("App_Data/routes.xml"));

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

        protected void btnExportXML_Click(object sender, EventArgs e)
        {
            ExportXML();
        }

        private void ExportXML()
        {
            DataSet ds = GenerateDataSet();

            ds.WriteXml(MapPath("App_Data/routesExport.xml"), XmlWriteMode.WriteSchema);

            lblInfo.Text = "XML datoteka exportana!";
        }

        protected void btnCreateXML_Click(object sender, EventArgs e)
        {
            CreateXML();
        }

        private void CreateXML()
        {
            SqlConnection con = new SqlConnection(cs);
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TravelOrder; SELECT * FROM Route", con);

            DataSet ds = new DataSet("TravelOrderRoutes");
            da.Fill(ds);

            ds.Tables[0].TableName = "TravelOrder";
            ds.Tables[1].TableName = "Route";

            DataRelation relacija = new DataRelation("relacija",
                ds.Tables[0].Columns["Id"], ds.Tables[1].Columns["TravelOrderID"]);
            relacija.Nested = true;
            ds.Relations.Add(relacija);

            ds.WriteXml(MapPath("App_Data/routes.xml"), XmlWriteMode.WriteSchema);

            lblInfo.Text = "XML datoteka kreirana!";
            btnCreateXML.Enabled = false;
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

        public DataSet GenerateDataSet()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("TravelOrder");


            ds.Tables.Add(dt);
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("OrderStatus",typeof(int));
            dt.Columns.Add("VehicleID", typeof(int));
            dt.Columns.Add("UserID", typeof(int));
            dt.Columns.Add("Vehicle_km_start", typeof(long));
            dt.Columns.Add("Starting_city", typeof(String));
            dt.Columns.Add("Finish_city", typeof(String));
            dt.Columns.Add("Total_days", typeof(int));
            dt.Columns.Add("Created", typeof(DateTime));
            dt.Columns.Add("Modified", typeof(DateTime));
            dt.Columns.Add("StartingDate", typeof(DateTime));
            foreach (TravelOrder order in orders)
            {
                DataRow dr = dt.Rows.Add();
                dr.SetField("Id", order.Id);
                dr.SetField("OrderStatus", order.OrderStatus);
                dr.SetField("VehicleID", order.VehicleID);
                dr.SetField("UserID", order.DriverID);
                dr.SetField("Vehicle_km_start", order.VehicleStartKM);
                dr.SetField("Starting_city", order.StartingCity);
                dr.SetField("Finish_city", order.FinishCity);
                dr.SetField("Total_days", order.TotalDays);
                dr.SetField("StartingDate", order.StartingDate);
            }

            DataTable dtRoute = new DataTable("Route");


            ds.Tables.Add(dtRoute);
            dtRoute.Columns.Add("Id", typeof(int));
            dtRoute.Columns.Add("TravelOrderID", typeof(int));
            dtRoute.Columns.Add("DateStart", typeof(DateTime));
            dtRoute.Columns.Add("DateEnd", typeof(DateTime));
            dtRoute.Columns.Add("StartCoordinate", typeof(String));
            dtRoute.Columns.Add("EndCoordinate", typeof(String));
            dtRoute.Columns.Add("DistanceCrossed", typeof(int));
            dtRoute.Columns.Add("AverageSpeed", typeof(int));
            dtRoute.Columns.Add("FuelConsumption", typeof(int));

            foreach (Route route in routes)
            {
                DataRow dr = dtRoute.Rows.Add();
                dr.SetField("Id", route.Id);
                dr.SetField("TravelOrderID", route.TravelOrder.Id);
                dr.SetField("DateStart", route.DateStart);
                dr.SetField("DateEnd", route.DateEnd);
                dr.SetField("StartCoordinate", route.StartCoordinate);
                dr.SetField("EndCoordinate", route.EndCoordinate);
                dr.SetField("DistanceCrossed", route.DistanceCrossed);
                dr.SetField("AverageSpeed", route.AvgSpeed);
                dr.SetField("FuelConsumption", route.FuelConsumption);
            }

            DataRelation relacija = new DataRelation("relacija",
             ds.Tables[0].Columns["Id"], ds.Tables[1].Columns["TravelOrderID"]);
            relacija.Nested = true;
            ds.Relations.Add(relacija);

            return ds;
        }
    }
}