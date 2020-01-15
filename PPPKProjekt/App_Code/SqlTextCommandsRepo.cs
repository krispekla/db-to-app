using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PPPKProjekt.App_Code
{
    public class SqlTextCommandsRepo : IRepo
    {
        private readonly string cs;

        public SqlTextCommandsRepo(String connectionString)
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
    }
}