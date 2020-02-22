using PPPKProjekt.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PPPKProjekt
{
    public partial class i3XML : System.Web.UI.Page
    {
        string cs = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        List<TravelOrder> orders = new List<TravelOrder>();
        List<Route> routes = new List<Route>();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnImportXML_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            ds.ReadXml(MapPath("App_Data/routes.xml"));

            foreach (DataRow rowDrzava in ds.Tables["TravelOrder"].Rows)
            {
                TravelOrder tempOrder = new TravelOrder();

                tempOrder.Id = int.Parse(rowDrzava["Id"].ToString());
                tempOrder.OrderStatus = int.Parse(rowDrzava["OrderStatus"].ToString());
                tempOrder.VehicleID = int.Parse(rowDrzava["VehicleID"].ToString());
                tempOrder.DriverID = int.Parse(rowDrzava["UserID"].ToString());
                tempOrder.VehicleStartKM = long.Parse(rowDrzava["Vehicle_km_start"].ToString());
                tempOrder.VehicleEndKM =  long.Parse(rowDrzava["Vehicle_km_end"].ToString());
                tempOrder.Distance = int.Parse(rowDrzava["Distance_crossed"].ToString());
                tempOrder.StartingCity = rowDrzava["Starting_city"].ToString();
                tempOrder.FinishCity = rowDrzava["Finish_city"].ToString();
                tempOrder.TotalPrice = decimal.Parse(rowDrzava["Total_price"].ToString());
                tempOrder.TotalDays = int.Parse(rowDrzava["Total_days"].ToString());
                tempOrder.StartingDate = DateTime.Parse(rowDrzava["StartingDate"].ToString());

                orders.Add(tempOrder);
                foreach (DataRow rowRoute in ds.Tables["Route"].Rows)
                {
                    Route tempRoute = new Route();
                    tempRoute.Id = int.Parse(rowRoute["Id"].ToString());
                    tempRoute.TravelOrder = tempOrder;
                    tempRoute.DateStart = DateTime.Parse(rowRoute["DateStart"].ToString());
                    tempRoute.DateEnd = DateTime.Parse(rowRoute["DateEnd"].ToString());
                    tempRoute.StartCoordinate = rowRoute["StartCoordinate"].ToString();
                    tempRoute.EndCoordinate = rowRoute["EndCoordinate"].ToString();
                    tempRoute.DistanceCrossed = int.Parse(rowRoute["DistanceCrossed"].ToString());
                    tempRoute.AvgSpeed = int.Parse(rowRoute["AverageSpeed"].ToString());
                    tempRoute.FuelConsumption = int.Parse(rowRoute["FuelConsumption"].ToString());

                    routes.Add(tempRoute);
                }
            }

            SetField();
        }

        private void SetField()
        {
            ddlTravelOrders.DataSource = orders;
            ddlTravelOrders.DataBind();

            ddlRoutes.DataSource = routes.Where(x => x.TravelOrder.Id == int.Parse(ddlTravelOrders.SelectedValue));
            ddlRoutes.DataBind();
        }

        protected void btnExportXML_Click(object sender, EventArgs e)
        {

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

            lblInfo.Text = "XML datoteka spremljena!";
        }
    }
}