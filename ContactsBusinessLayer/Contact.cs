using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsContact
    {
        public int ID { set; get; }
        public string firstName { set; get; }
        public string lastName { set; get; }
        public string email { set; get; }
        public string phone { set; get; }
        public string address { set; get; }
        public DateTime dateOfBirth { set; get; }
        public string imgPath { set; get; }
        public int countryID { set; get; }
        private clsContact(int ID, string FirstName, string LastName,
            string Email, string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)

        {
            this.ID = ID;
            this.firstName = FirstName;
            this.lastName = LastName;
            this.email = Email;
            this.phone = Phone;
            this.address = Address;
            this.dateOfBirth = DateOfBirth;
            this.countryID = CountryID;
            this.imgPath = ImagePath;

        }
        public static clsContact find(int ID)
        {
            string firstName = "", lastName = "", email = "", phone = "", address = "", imgPath = "";
            DateTime dateOfBirth = DateTime.Now;
            int countryID = -1;

            if(clsContactsDataAccess.getContactByID(ID, ref firstName, ref lastName, ref email, ref phone, ref address, ref dateOfBirth, ref countryID, ref imgPath))
            {
                return new clsContact(ID, firstName, lastName, email, phone, address, dateOfBirth, countryID, imgPath);
            } else
            {
                return null;
            }
        }
    }
}
