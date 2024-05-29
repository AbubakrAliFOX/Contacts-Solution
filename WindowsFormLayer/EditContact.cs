using ContactsBusinessLayer;
using CountriesBussinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormLayer
{
    public partial class EditContact : Form
    {
        public enum enMode { addNew = 0, update = 1 };

        private enMode _mode;

        int _contactID;
        clsContact _contact;
        public EditContact(int contactID)
        {
            InitializeComponent();

            _contactID = contactID;

            if (_contactID == -1)
            {
                _mode = enMode.addNew;
            } else
            {
                _mode = enMode.update;

            }
        }

        private void _fillCountriesComboBox ()
        {
            DataTable dtCountries = clsCountry.getAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {
                cbCountries.Items.Add(row["CountryName"]);
            }
        }

        private void _loadData ()
        {
            _fillCountriesComboBox();

            cbCountries.SelectedIndex = 0;

            if (_mode == enMode.addNew)
            {
                lblMode.Text = "Add new contact";
                _contact = new clsContact();
                return;
            }

            _contact = clsContact.find(_contactID);

            if (_contact == null)
            {
                MessageBox.Show("This form will be closed because no contact has been found");
                this.Close();
            }

            lblMode.Text = "Edit contact with ID = " + _contactID;
            lblContactID = _contactID.ToString();
            txtFirstName.Text = _contact.firstName;
        }
    }
}
