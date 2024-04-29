using System;
using System.Data.SqlClient;


namespace ContactsDataAccessLayer
{
    public class clsContactsDataAccess
    {
        public static bool getContactByID (int ID, ref string firstName, ref string lastName, ref string email, ref string phone, ref string address, ref DateTime dateOfBirth, ref int countryID, ref string imgPath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            
            string query = "select * from Contacts where ContactID = @ContactID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@ContactID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    isFound = true;
                    ID = (int)reader["ContactID"];
                    firstName = (string)reader["FirstName"];
                    lastName = (string)reader["LastName"];
                    email = (string)reader["Email"];
                    phone = (string)reader["Phone"];
                    address = (string)reader["Address"];
                    countryID = (int)reader["CountryID"];
                    dateOfBirth = (DateTime)reader["DateOfBirth"];
                    imgPath = (string)reader["ImagePath"];
                } else
                {
                    isFound = false;
                }

                reader.Close();
            } catch
            {
                isFound = false;
            } finally
            {
                connection.Close();
            }

            return isFound;
        }
    }
}
