using System;
using System.Data;
using System.Data.SqlClient;
using CountriesDataAccessLayer;

namespace CountriesBussinessLayer
{
    public class clsCountry
    {
        public int ID { set; get; }
        public string name { set; get; }
        public string code { set; get; }
        public string phoneCode { set; get; }
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public clsCountry()

        {
            this.ID = -1;
            this.name = "";
            this.code = "";
            this.phoneCode = "";
            Mode = enMode.AddNew;

        }

        private clsCountry(int ID, string name, string code, string phoneCode)
        {
            this.ID = ID;
            this.name = name;
            this.code = code;
            this.phoneCode = phoneCode;
            Mode = enMode.Update;
        }

        public static clsCountry findByName(string name)
        {
            int ID = -1;
            string code = "";
            string phoneCode = "";

            if (CountriesDataAccess.findByName(name, ref ID, ref code, ref phoneCode))
            {
                return new clsCountry(ID, name, code, phoneCode);
            }
            else
            {
                return null;
            }
        }

        public static bool countryExists(string name)
        {
            return CountriesDataAccess.countryExists(name);
        }

        private bool _addNewCountry ()
        {
            this.ID = CountriesDataAccess.addNewCountry(this.name, this.code, this.phoneCode);
            return (this.ID != -1);
        }

        private bool _updateCountry ()
        {
            return CountriesDataAccess.updateCountry(this.ID, this.name, this.code, this.phoneCode);
        }

        public bool save ()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_addNewCountry())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _updateCountry();

            }

            return false;
        }

        public static bool delete (string Name)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"DELETE FROM Countries
                                WHERE CountryName = @CountryName;";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@CountryName", Name);

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

        public static DataTable getAllCountries()
        {
            return CountriesDataAccess.getAllCountries();
        }
    }
}
