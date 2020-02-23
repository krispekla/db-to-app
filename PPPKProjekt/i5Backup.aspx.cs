using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using PPPKProjekt.App_Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace PPPKProjekt
{
    public partial class i5Backup : System.Web.UI.Page
    {
        private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        private static SqlDatabase db = new SqlDatabase(cs);
        private static IRepo repo = new Repo(cs);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBackup_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            var tables = new[] { "Users", "VehicleType", "Vehicle", "TravelOrder", "Route", "VehicleFuel", "Service", "ServiceItem", "Bills" };

            foreach (var table in tables)
            {
                var query = "SELECT * FROM " + table;

                SqlConnection conn = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable(table);
                da.Fill(dt);

                conn.Close();
                conn.Dispose();

                ds.Tables.Add(dt);
            }

            BackupXML(ds);
        }

        private void BackupXML(DataSet ds)
        {
            XmlWriterSettings xmlPostavke = new XmlWriterSettings();
            xmlPostavke.Indent = true;
            XmlWriter xmlWriter = XmlWriter.Create(Server.MapPath("App_Data/dbBackup.xml"), xmlPostavke);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("backup");

            for (int i = 0; i < ds.Tables.Count; i++)
            {


                for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                {
                    xmlWriter.WriteStartElement(ds.Tables[i].TableName);

                    for (int g = 0; g < ds.Tables[i].Columns.Count; g++)
                    {
                        xmlWriter.WriteStartElement(ds.Tables[i].Columns[g].ColumnName);
                        xmlWriter.WriteString(ds.Tables[i].Rows[j].ItemArray[g].ToString());
                        xmlWriter.WriteEndElement();
                    }
                    xmlWriter.WriteEndElement();
                }

            }

            xmlWriter.WriteEndElement();

            xmlWriter.Close();
            lblInfo.Text = "Uspjesno napravljen backup baze";

        }

        protected void btnCleanDB_Click(object sender, EventArgs e)
        {

            db.ExecuteNonQuery("sp_cleanDatabase");
            lblInfo.Text = "Baza je ociscena";

        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            XmlReader xmlReader = XmlReader.Create(Server.MapPath("App_Data/dbBackup.xml"));
            DataSet ds = new DataSet();
            ds.ReadXml(xmlReader);

            List<TravelOrder> orders = new List<TravelOrder>();
            List<Route> routes = new List<Route>();
            Hashtable foreignKeys = new Hashtable();

            foreignKeys.Add("asd", 3);
            using (SqlConnection con = new SqlConnection(cs))
            {
                try
                {
                    con.Open();
                    foreach (DataRow row in ds.Tables["Users"].Rows)
                    {

                        using (SqlCommand cmd = con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = @"insert into Users(Name, Surname, Mobile, Driving_License) VALUES(@param1,@param2,@param3, @param4); SELECT SCOPE_IDENTITY()";
                            cmd.Parameters.Add("@param1", SqlDbType.VarChar, 50).Value = row["Name"].ToString();
                            cmd.Parameters.Add("@param2", SqlDbType.NVarChar).Value = row["Surname"].ToString();
                            cmd.Parameters.Add("@param3", SqlDbType.NVarChar).Value = row["Mobile"].ToString();
                            cmd.Parameters.Add("@param4", SqlDbType.NVarChar).Value = row["Driving_License"].ToString();

                            var resId = cmd.ExecuteScalar();

                            foreignKeys.Add("Users" + row["Id"].ToString(), resId);
                        }
                    }

                    foreach (DataRow row in ds.Tables["VehicleType"].Rows)
                    {

                        using (SqlCommand cmd = con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = @"insert into VehicleType(Name) VALUES(@param1);SELECT SCOPE_IDENTITY()";
                            cmd.Parameters.Add("@param1", SqlDbType.NVarChar).Value = row["Name"].ToString();

                            var resId = cmd.ExecuteScalar();

                            foreignKeys.Add("VehicleType" + row["Id"].ToString(), resId);
                        }
                    }

                    foreach (DataRow row in ds.Tables["Vehicle"].Rows)
                    {

                        using (SqlCommand cmd = con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = @"INSERT INTO Vehicle(VehicleTypeId, Plate, Brand, Production_Year, Vehicle_Status, Milleage)" +
                                                "VALUES(@param1,@param2,@param3, @param4, @param5, @param6);SELECT SCOPE_IDENTITY()";
                            cmd.Parameters.Add("@param1", SqlDbType.Int).Value = foreignKeys["VehicleType" + row["VehicleTypeId"].ToString()];
                            cmd.Parameters.Add("@param2", SqlDbType.NVarChar).Value = row["Plate"].ToString();
                            cmd.Parameters.Add("@param3", SqlDbType.NVarChar).Value = row["Brand"].ToString();
                            cmd.Parameters.Add("@param4", SqlDbType.Date).Value = DateTime.Parse(row["Production_Year"].ToString());
                            cmd.Parameters.Add("@param5", SqlDbType.Bit).Value = Boolean.Parse(row["Vehicle_Status"].ToString());
                            cmd.Parameters.Add("@param6", SqlDbType.BigInt).Value = int.Parse(row["Milleage"].ToString());

                            var resId = cmd.ExecuteScalar();

                            foreignKeys.Add("Vehicle" + row["Id"].ToString(), resId);
                        }
                    }

                    foreach (DataRow row in ds.Tables["TravelOrder"].Rows)
                    {

                        using (SqlCommand cmd = con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = @" INSERT INTO TravelOrder(OrderStatus, VehicleID, UserID, Vehicle_km_start, Starting_city, Finish_city, Total_days, StartingDate)" +
                                                "VALUES(@param1,@param2,@param3, @param4, @param5, @param6, @param7, @param8); SELECT SCOPE_IDENTITY()";
                            cmd.Parameters.Add("@param1", SqlDbType.Int).Value = int.Parse(row["OrderStatus"].ToString());
                            cmd.Parameters.Add("@param2", SqlDbType.Int).Value = foreignKeys["Vehicle" + row["VehicleID"].ToString()];
                            cmd.Parameters.Add("@param3", SqlDbType.Int).Value = foreignKeys["Users" + row["UserID"].ToString()];
                            cmd.Parameters.Add("@param4", SqlDbType.BigInt).Value = int.Parse(row["Vehicle_km_start"].ToString());
                            cmd.Parameters.Add("@param5", SqlDbType.NVarChar).Value = row["Starting_city"].ToString();
                            cmd.Parameters.Add("@param6", SqlDbType.NVarChar).Value = row["Finish_city"].ToString();
                            cmd.Parameters.Add("@param7", SqlDbType.Int).Value = int.Parse(row["Total_days"].ToString());
                            cmd.Parameters.Add("@param8", SqlDbType.Date).Value = DateTime.Parse(row["StartingDate"].ToString());

                            var resId = cmd.ExecuteScalar();

                            foreignKeys.Add("TravelOrder" + row["Id"].ToString(), resId);
                        }
                    }

                    foreach (DataRow row in ds.Tables["Route"].Rows)
                    {
                        using (SqlCommand cmd = con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = @"INSERT INTO Route(TravelOrderID, DateStart, DateEnd, StartCoordinate, EndCoordinate, DistanceCrossed, AverageSpeed, FuelConsumption)" +
                                                "VALUES(@param1,@param2,@param3, @param4, @param5, @param6, @param7, @param8); SELECT SCOPE_IDENTITY()";
                            cmd.Parameters.Add("@param1", SqlDbType.Int).Value = foreignKeys["TravelOrder" + row["TravelOrderID"].ToString()];
                            cmd.Parameters.Add("@param2", SqlDbType.Date).Value = DateTime.Parse(row["DateStart"].ToString());
                            cmd.Parameters.Add("@param3", SqlDbType.Date).Value = DateTime.Parse(row["DateEnd"].ToString());
                            cmd.Parameters.Add("@param4", SqlDbType.NVarChar).Value = row["StartCoordinate"].ToString();
                            cmd.Parameters.Add("@param5", SqlDbType.NVarChar).Value = row["EndCoordinate"].ToString();
                            cmd.Parameters.Add("@param6", SqlDbType.Int).Value = int.Parse(row["DistanceCrossed"].ToString());
                            cmd.Parameters.Add("@param7", SqlDbType.Int).Value = int.Parse(row["AverageSpeed"].ToString());
                            cmd.Parameters.Add("@param8", SqlDbType.Int).Value = int.Parse(row["FuelConsumption"].ToString());

                            cmd.ExecuteScalar();
                        }
                    }


                    foreach (DataRow row in ds.Tables["Service"].Rows)
                    {

                        using (SqlCommand cmd = con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = @"INSERT INTO Service(Date, VehicleID)" +
                                                "VALUES(@param1,@param2); SELECT SCOPE_IDENTITY()";
                            cmd.Parameters.Add("@param1", SqlDbType.Date).Value = DateTime.Parse(row["Date"].ToString());
                            cmd.Parameters.Add("@param2", SqlDbType.Int).Value = foreignKeys["Vehicle" + row["VehicleID"].ToString()];


                            var resId = cmd.ExecuteScalar();

                            foreignKeys.Add("Service" + row["Id"].ToString(), resId);
                        }
                    }


                    foreach (DataRow row in ds.Tables["ServiceItem"].Rows)
                    {

                        using (SqlCommand cmd = con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = @"INSERT INTO ServiceItem(ServiceID, ServiceName, Price)" +
                                                "VALUES(@param1,@param2,@param3); SELECT SCOPE_IDENTITY()";
                            cmd.Parameters.Add("@param1", SqlDbType.Int).Value = foreignKeys["Service" + row["ServiceID"].ToString()];
                            cmd.Parameters.Add("@param2", SqlDbType.NVarChar).Value = row["ServiceName"].ToString();
                            cmd.Parameters.Add("@param3", SqlDbType.Money).Value = Decimal.Parse(row["Price"].ToString());

                            cmd.ExecuteScalar();
                        }
                    }


                    foreach (DataRow row in ds.Tables["Bills"].Rows)
                    {
                        using (SqlCommand cmd = con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = @"INSERT INTO Bills(Price, TravelOrder)" +
                                                "VALUES(@param1,@param2); SELECT SCOPE_IDENTITY()";
                            cmd.Parameters.Add("@param1", SqlDbType.Money).Value = Decimal.Parse(row["Price"].ToString());
                            cmd.Parameters.Add("@param2", SqlDbType.Int).Value = foreignKeys["TravelOrder" + row["TravelOrder"].ToString()];

                            cmd.ExecuteScalar();
                        }
                    }

                    foreach (DataRow row in ds.Tables["VehicleFuel"].Rows)
                    {
                        using (SqlCommand cmd = con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = @"INSERT INTO VehicleFuel(UserID, Date, Litres, FuelPrice)" +
                                                "VALUES(@param1,@param2,@param3,@param4); SELECT SCOPE_IDENTITY()";
                            cmd.Parameters.Add("@param1", SqlDbType.Int).Value = foreignKeys["Users" + row["UserID"].ToString()];
                            cmd.Parameters.Add("@param2", SqlDbType.Date).Value = DateTime.Parse(row["Date"].ToString());
                            cmd.Parameters.Add("@param3", SqlDbType.Int).Value = int.Parse(row["Litres"].ToString());
                            cmd.Parameters.Add("@param4", SqlDbType.Money).Value = Decimal.Parse(row["FuelPrice"].ToString());

                            cmd.ExecuteScalar();
                        }
                    }

                    lblInfo.Text = "Uspjesno restorana baza";
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}