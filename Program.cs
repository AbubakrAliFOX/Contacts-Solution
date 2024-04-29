using System;
using System.Data;
using System.Net;
using System.Security.Policy;
using ContactsBusinessLayer;

namespace Contacts_Solution
{
    internal class Program
    {
        static void findContact (int ID)
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
            } else
            {
                Console.WriteLine($"Contact {ID} not found");
            }
        }
        static void Main(string[] args)
        {
            findContact(1);
        }
    }
}
