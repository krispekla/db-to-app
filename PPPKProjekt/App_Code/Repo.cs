using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Transactions;

namespace PPPKProjekt.App_Code
{
    public class Repo : IRepo
    {
        private readonly string cs;

        public Repo(String connectionString)
        {
            this.cs = connectionString;
        }

        public int DeleteDriver(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"delete from Users where Id = @param1";
                    cmd.Parameters.Add("@param1", SqlDbType.Int).Value = id;
                    try
                    {

                        int r = cmd.ExecuteNonQuery();
                        if (r == 0) return 400;

                        return 200;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

        }

        public List<Driver> GetAllDrivers()
        {
            List<Driver> list = new List<Driver>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "select * from Users";
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.HasRows)
                        {
                            while (r.Read())
                            {
                                list.Add(new Driver
                                {
                                    Id = (int)r["Id"],
                                    Name = r["Name"].ToString(),
                                    Lastname = r["Surname"].ToString(),
                                    Mobile = r["Mobile"].ToString(),
                                    DrivingLicence = r["Driving_License"].ToString()
                                });
                            }
                        }
                    }
                }


                return list;
            }
        }

        public int InsertDriver(Driver driver)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"insert into Users(Name, Surname, Mobile, Driving_License) VALUES(@param1,@param2,@param3, @param4)";
                    cmd.Parameters.Add("@param1", SqlDbType.VarChar, 50).Value = driver.Name;
                    cmd.Parameters.Add("@param2", SqlDbType.NVarChar).Value = driver.Lastname;
                    cmd.Parameters.Add("@param3", SqlDbType.NVarChar).Value = driver.Mobile;
                    cmd.Parameters.Add("@param4", SqlDbType.NVarChar).Value = driver.DrivingLicence;
                    try
                    {

                        int r = cmd.ExecuteNonQuery();
                        if (r == 0) return 400;

                        return 200;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
        }

        public int UpdateDriver(Driver driver)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update Users set Name = @param2, Surname = @param3, Mobile = @param4, Driving_License = @param5 where Id = @param1";
                    cmd.Parameters.Add("@param1", SqlDbType.Int).Value = driver.Id;
                    cmd.Parameters.Add("@param2", SqlDbType.NVarChar).Value = driver.Name;
                    cmd.Parameters.Add("@param3", SqlDbType.NVarChar).Value = driver.Lastname;
                    cmd.Parameters.Add("@param4", SqlDbType.NVarChar).Value = driver.Mobile;
                    cmd.Parameters.Add("@param5", SqlDbType.NVarChar).Value = driver.DrivingLicence;
                    try
                    {

                        int r = cmd.ExecuteNonQuery();
                        if (r == 0) return 400;

                        return 200;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
        }

        public List<Vehicle> GetAllVehicles()
        {
            List<Vehicle> lista = new List<Vehicle>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_getAllVehicles";

                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.HasRows)
                        {
                            while (r.Read())
                            {
                                lista.Add(new Vehicle
                                {
                                    Id = (int)r["Id"],
                                    Brand = r["Brand"].ToString(),
                                    IsAvailable = (bool)r["Vehicle_Status"],
                                    Milleage = (long)r["Milleage"],
                                    Plate = r["Plate"].ToString(),
                                    VehicleTypeId = (int)r["VehicleTypeId"],
                                    Year = (DateTime)r["Production_Year"]
                                });
                            }
                        }
                    }
                }
                return lista;
            }
        }
        public int InsertVehicle(Vehicle vehicle)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_insertVehicle";

                    cmd.Parameters.Add(new SqlParameter("@vehicleType", SqlDbType.Int) { Value = vehicle.VehicleTypeId });
                    cmd.Parameters.Add(new SqlParameter("@plate", SqlDbType.NVarChar, 50) { Value = vehicle.Plate });
                    cmd.Parameters.Add(new SqlParameter("@brand", SqlDbType.NVarChar, 50) { Value = vehicle.Brand });
                    cmd.Parameters.Add(new SqlParameter("@year", SqlDbType.Date) { Value = vehicle.Year });
                    cmd.Parameters.Add(new SqlParameter("@isAvailable", SqlDbType.Bit) { Value = vehicle.IsAvailable });
                    cmd.Parameters.Add(new SqlParameter("@milleage", SqlDbType.BigInt) { Value = vehicle.Milleage });

                    try
                    {

                        int r = cmd.ExecuteNonQuery();
                        if (r == 0) return 400;

                        return 200;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
        }
        public int UpdateVehicle(Vehicle vehicle)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_updateVehicle";

                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = vehicle.Id });
                    cmd.Parameters.Add(new SqlParameter("@vehicleType", SqlDbType.Int) { Value = vehicle.VehicleTypeId });
                    cmd.Parameters.Add(new SqlParameter("@plate", SqlDbType.NVarChar, 50) { Value = vehicle.Plate });
                    cmd.Parameters.Add(new SqlParameter("@brand", SqlDbType.NVarChar, 50) { Value = vehicle.Brand });
                    cmd.Parameters.Add(new SqlParameter("@year", SqlDbType.Date) { Value = vehicle.Year });
                    cmd.Parameters.Add(new SqlParameter("@isAvailable", SqlDbType.Bit) { Value = vehicle.IsAvailable });
                    cmd.Parameters.Add(new SqlParameter("@milleage", SqlDbType.BigInt) { Value = vehicle.Milleage });


                    try
                    {

                        int r = cmd.ExecuteNonQuery();
                        if (r == 0) return 400;

                        return 200;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public int DeleteVehicle(int id)
        {

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_deleteVehicle";



                    SqlParameter param = new SqlParameter("@id", SqlDbType.Int) { Value = id };

                    cmd.Parameters.Add(param);

                    try
                    {
                        int r = cmd.ExecuteNonQuery();
                        if (r == 0) return 400;

                        return 200;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

        }

        public List<TravelOrder> GetAllTravelOrder()
        {
            List<TravelOrder> list = new List<TravelOrder>();

            using (TransactionScope trScope = new TransactionScope())
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd1 = con.CreateCommand();
                    using (cmd1)
                    {
                        cmd1.CommandText = "select * from TravelOrder";
                        cmd1.CommandType = CommandType.Text;
                        using (SqlDataReader r = cmd1.ExecuteReader())
                        {
                            if (r.HasRows)
                            {
                                while (r.Read())
                                {
                                    list.Add(new TravelOrder
                                    {
                                        Id = (int)r["Id"],
                                        OrderStatus = Utils.ParseNullable<int>(r["OrderStatus"].ToString()),
                                        VehicleID = Utils.ParseNullable<int>(r["VehicleID"].ToString()),
                                        DriverID = Utils.ParseNullable<int>(r["UserID"].ToString()),
                                        VehicleStartKM = Utils.ParseNullable<int>(r["Vehicle_km_start"].ToString()),
                                        VehicleEndKM = Utils.ParseNullable<int>(r["Vehicle_km_end"].ToString()),
                                        Distance = Utils.ParseNullable<int>(r["Distance_crossed"].ToString()),
                                        StartingCity = r["Starting_city"].ToString(),
                                        FinishCity = r["Finish_city"].ToString(),
                                        TotalDays = Utils.ParseNullable<int>(r["Total_days"].ToString()),
                                        TotalPrice = Utils.ParseNullable<Decimal>(r["Total_price"].ToString()),
                                        StartingDate = DateTime.Parse(r["StartingDate"].ToString()),
                                    });
                                };
                            }
                        }

                    }
                }
                trScope.Complete();
            }

            return list;
        }


        public int InsertTravelOrder(TravelOrder trOrder)
        {
            int result;
            using (TransactionScope trScope = new TransactionScope())
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd1 = con.CreateCommand();
                    using (cmd1)
                    {
                        cmd1.CommandText = "insert into TravelOrder(OrderStatus, VehicleID, UserID, Vehicle_km_start, Starting_city, Finish_city, Total_days, StartingDate)" +
                            " VALUES(@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8)";
                        cmd1.CommandType = CommandType.Text;
                        cmd1.Parameters.Add("@param1", SqlDbType.VarChar, 50).Value = trOrder.OrderStatus;
                        cmd1.Parameters.Add("@param2", SqlDbType.NVarChar).Value = trOrder.VehicleID;
                        cmd1.Parameters.Add("@param3", SqlDbType.NVarChar).Value = trOrder.DriverID;
                        cmd1.Parameters.Add("@param4", SqlDbType.NVarChar).Value = trOrder.VehicleStartKM;
                        cmd1.Parameters.Add("@param5", SqlDbType.NVarChar).Value = trOrder.StartingCity;
                        cmd1.Parameters.Add("@param6", SqlDbType.NVarChar).Value = trOrder.FinishCity;
                        cmd1.Parameters.Add("@param7", SqlDbType.NVarChar).Value = trOrder.TotalDays;
                        cmd1.Parameters.Add("@param8", SqlDbType.DateTime).Value = trOrder.StartingDate;

                        try
                        {
                            result = cmd1.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                trScope.Complete();
            }
            if (result == 0) return 400;

            return 200;
        }

        public int DeleteTravelOrder(int id)
        {
            int result;
            using (TransactionScope trScope = new TransactionScope())
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = @"delete from TravelOrder where Id = @param1";
                        cmd.Parameters.Add("@param1", SqlDbType.Int).Value = id;
                        try
                        {

                            result = cmd.ExecuteNonQuery();

                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                trScope.Complete();
            }
            if (result == 0) return 400;

            return 200;
        }

        public int UpdateTravelOrder(TravelOrder trOrder)
        {
            int r;
            using (TransactionScope trScope = new TransactionScope())
            {

                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlCommand cmd1 = con.CreateCommand())
                    {
                        cmd1.CommandText = "update TravelOrder set OrderStatus = @param1, VehicleID = @param2, UserID = @param3," +
                        " Vehicle_km_start = @param4, Starting_city = @param5, Finish_city = @param6, Total_days = @param7, StartingDate = @param8 where Id = @param9";

                        cmd1.CommandType = CommandType.Text;
                        cmd1.Parameters.Add("@param1", SqlDbType.VarChar, 50).Value = trOrder.OrderStatus;
                        cmd1.Parameters.Add("@param2", SqlDbType.NVarChar).Value = trOrder.VehicleID;
                        cmd1.Parameters.Add("@param3", SqlDbType.NVarChar).Value = trOrder.DriverID;
                        cmd1.Parameters.Add("@param4", SqlDbType.NVarChar).Value = trOrder.VehicleStartKM;
                        cmd1.Parameters.Add("@param5", SqlDbType.NVarChar).Value = trOrder.StartingCity;
                        cmd1.Parameters.Add("@param6", SqlDbType.NVarChar).Value = trOrder.FinishCity;
                        cmd1.Parameters.Add("@param7", SqlDbType.NVarChar).Value = trOrder.TotalDays;
                        cmd1.Parameters.Add("@param8", SqlDbType.DateTime).Value = trOrder.StartingDate;
                        cmd1.Parameters.Add("@param9", SqlDbType.Int).Value = trOrder.Id;
                        try
                        {
                            r = cmd1.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }

                trScope.Complete();
            }
            if (r == 0) return 400;

            return 200;
        }
    }

}