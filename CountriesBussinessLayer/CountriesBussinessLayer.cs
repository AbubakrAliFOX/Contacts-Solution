﻿using System;
using System.Data;
using CountriesDataAccessLayer;

namespace CountriesBussinessLayer
{
    public class clsCountry
    {
        public int ID { set; get; }
        public string name { set; get; }
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public clsCountry()

        {
            this.ID = -1;
            this.name = "";
            Mode = enMode.AddNew;

        }

        private clsCountry(int ID, string name)
        {
            this.ID = ID;
            this.name = name;
            Mode = enMode.Update;
        }

        public static clsCountry findByName(string name)
        {
            int ID = -1;

            if (CountriesDataAccess.findByName(name, ref ID))
            {
                return new clsCountry(ID, name);
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
    }
}