using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Security.Policy;

namespace CountriesDataAccessLayer
{
    public class CountriesDataAccess
    {
        public static bool findByName(string name, ref int ID, ref string code, ref string phoneCode)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select * from Countries where CountryName = @CountryName";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@CountryName", name);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ID = (int)reader["CountryID"];
                    if (reader["Code"] != DBNull.Value)
                    {
                        code = (string)reader["Code"];
                    }
                    else
                    {
                        code = "";
                    }

                    if (reader["PhoneCode"] != DBNull.Value)
                    {
                        phoneCode = (string)reader["PhoneCode"];
                    }
                    else
                    {
                        phoneCode = "";
                    }
                }

                reader.Close();
            }
            catch
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        
        public static bool countryExists(string name) 
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "select Found = 1 from Countries where CountryName = @CountryName";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@CountryName", name);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                isFound = reader.HasRows;
                reader.Close();
            }
            catch
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
    
        public static int addNewCountry (string name, string code, string phoneCode)
        {
            int CountryID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"INSERT INTO Countries (CountryName, Code, PhoneCode)
                            VALUES (@CountryName, @Code, @PhoneCode);
                            Select SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@CountryName", name);

            if (code != "")
            {
                cmd.Parameters.AddWithValue("@Code", code);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Code", System.DBNull.Value);
            }

            if (phoneCode != "")
            {
                cmd.Parameters.AddWithValue("@PhoneCode", phoneCode);
            }
            else
            {
                cmd.Parameters.AddWithValue("@PhoneCode", System.DBNull.Value);
            }

            try
            {
                connection.Open();
                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    CountryID = insertedID;
                }

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return CountryID;
        }
        
        public static bool updateCountry (int ID, string name, string code, string phoneCode)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"UPDATE Countries
                            SET CountryName = @CountryName,
                                Code = @Code,
                                PhoneCode = @PhoneCode
                            WHERE CountryID = @CountryID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@CountryName", name);
            cmd.Parameters.AddWithValue("@CountryID", ID);

            if (code != "")
            {
                cmd.Parameters.AddWithValue("@Code", code);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Code", System.DBNull.Value);
            }

            if (phoneCode != "")
            {
                cmd.Parameters.AddWithValue("@PhoneCode", phoneCode);
            }
            else
            {
                cmd.Parameters.AddWithValue("@PhoneCode", System.DBNull.Value);
            }

            try
            {
                connection.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
    
        public static DataTable getAllCountries ()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "Select * from Countries";

            SqlCommand cmd = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }

                reader.Close();
            }

            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
    }
}
