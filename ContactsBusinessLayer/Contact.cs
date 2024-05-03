using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsContact
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int ID { set; get; }
        public string firstName { set; get; }
        public string lastName { set; get; }
        public string email { set; get; }
        public string phone { set; get; }
        public string address { set; get; }
        public DateTime dateOfBirth { set; get; }
        public string imgPath { set; get; }
        public int countryID { set; get; }
        public clsContact()

        {
            this.ID = -1;
            this.firstName = "";
            this.lastName = "";
            this.email = "";
            this.phone = "";
            this.address = "";
            this.dateOfBirth = DateTime.Now;
            this.countryID = -1;
            this.imgPath = "";
            Mode = enMode.AddNew;

        }
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
            Mode = enMode.Update;

        }
        public static clsContact find(int ID)
        {
            string firstName = "", lastName = "", email = "", phone = "", address = "", imgPath = "";
            DateTime dateOfBirth = DateTime.Now;
            int countryID = -1;

            if (clsContactsDataAccess.getContactByID(ID, ref firstName, ref lastName, ref email, ref phone, ref address, ref dateOfBirth, ref countryID, ref imgPath))
            {
                return new clsContact(ID, firstName, lastName, email, phone, address, dateOfBirth, countryID, imgPath);
            }
            else
            {
                return null;
            }
        }

        public static bool delete(int ID)
        {
            return clsContactsDataAccess.deleteContactByID(ID);
        }

        private bool _addNewContact ()
        {
            this.ID = clsContactsDataAccess.addNewContact(this.firstName, this.lastName, this.email,
                this.phone, this.address, this.dateOfBirth, this.countryID, this.imgPath);
            return (this.ID != -1);
        }

        private bool _updateContact()
        {
            return clsContactsDataAccess.updateContact(this.ID, this.firstName, this.lastName, this.email,
                this.phone, this.address, this.dateOfBirth, this.countryID, this.imgPath);
        }

        public bool save ()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_addNewContact())
                    {
                        Mode = enMode.Update;
                        return true;
                    } else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _updateContact();
                
            }

            return false;
        }

        public static DataTable getAllContacts ()
        {
            return clsContactsDataAccess.getAllContacts();
        } 

        public static bool contactExists (int ID)
        {
            return clsContactsDataAccess.contactExists(ID);
        }
    }
}
