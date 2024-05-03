using System;
using System.Data;
using System.Net;
using System.Security.Policy;
using ContactsBusinessLayer;
using CountriesBussinessLayer;

namespace Contacts_Solution
{
    internal class Program
    {
        static void findContact(int ID)
        {
            clsContact contact1 = clsContact.find(ID);

            if (contact1 != null)
            {
                Console.WriteLine($"Contact ID: {contact1.ID}");
                Console.WriteLine($"Name: {contact1.firstName} {contact1.lastName}");
                Console.WriteLine($"Email: {contact1.email}");
                Console.WriteLine($"Phone: {contact1.phone}");
                Console.WriteLine($"Address: {contact1.address}");
                Console.WriteLine($"Country ID: {contact1.countryID}");
            }
            else
            {
                Console.WriteLine($"Contact {ID} not found");
            }
        }

        static void addNewContact()
        {
            clsContact Contact1 = new clsContact();

            Contact1.firstName = "Fadi";
            Contact1.lastName = "Maher";
            Contact1.email = "A@a.com";
            Contact1.phone = "010010";
            Contact1.address = "address1";
            Contact1.dateOfBirth = new DateTime(1977, 11, 6, 10, 30, 0);
            Contact1.countryID = 1;
            Contact1.imgPath = "";

            if (Contact1.save())
            {

                Console.WriteLine("Contact Added Successfully with id=" + Contact1.ID);
            } else
            {
                Console.WriteLine("Contact with id=" + Contact1.ID + "Not Added");
            }
        }

        static void updateContact(int ID)
        {
            clsContact Contact1 = clsContact.find(ID);

            if (Contact1 != null)
            {
                Contact1.firstName = "Hazer";
                Contact1.lastName = "Maher";
                Contact1.email = "A@a.com";
                Contact1.phone = "010010";
                Contact1.address = "address1";
                Contact1.dateOfBirth = new DateTime(1977, 11, 6, 10, 30, 0);
                Contact1.countryID = 1;
                Contact1.imgPath = "";

                if (Contact1.save())
                {

                    Console.WriteLine("Contact updated Successfully with id=" + Contact1.ID);
                }
                else
                {
                    Console.WriteLine("Contact with id=" + Contact1.ID + "Not updated");
                }
            }
            else
            {
                Console.WriteLine("Contact with id=" + Contact1.ID + "Not Found");
            }
        }

        static void deleteContact(int ID)
        {
            if(clsContact.contactExists(ID))
            {
                if (clsContact.delete(ID))
                {
                    Console.WriteLine("Contact with id=" + ID + " Deleted");
                }
                else
                {
                    Console.WriteLine("Contact with id=" + ID + " Not Deleted");
                }
            } else
            {
                Console.WriteLine("Contact with id=" + ID + " Not Found");

            }
        }

        
        static void listContacts ()
        {
            DataTable dataTable = clsContact.getAllContacts();

            Console.WriteLine("Cotacts Data:");
                
            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"{row["ContactID"]}: {row["FirstName"]}, {row["LastName"]}");
            }
        }

        static void findCountryByName (string Name)
        {
            clsCountry country1 = clsCountry.findByName(Name);

            if (country1 != null)
            {
                Console.WriteLine($"Country ID: {country1.ID}");
                Console.WriteLine($"Name: {country1.name}");
            }
            else
            {
                Console.WriteLine($"Country {Name} not found");
            }
        }

        static void Main(string[] args)
        {
            //findContact(9);
            //addNewContact();
            //updateContact(9);
            //deleteContact(7);
            //listContacts();
            findCountryByName("Germany");
        }
    }
}
