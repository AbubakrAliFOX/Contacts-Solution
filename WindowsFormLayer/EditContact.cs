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
            lblContactID.Text = _contactID.ToString();
            txtFirstName.Text = _contact.firstName;
            txtLastName.Text = _contact.lastName;
            txtEmail.Text = _contact.email;
            txtPhone.Text = _contact.phone;
            txtAddress.Text = _contact.address;
            dtpDate.Value = _contact.dateOfBirth;

            if (_contact.imgPath != "")
            {
                pictureBox1.Load(_contact.imgPath);
            }

            LLRemoveImg.Visible = (_contact.imgPath != "");

            //cbCountries.SelectedIndex = cbCountries.FindString(clsCountry.find(_contact.countryID).name);
        }

        private void EditContact_Load(object sender, EventArgs e)
        {
            _loadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int countryID = clsCountry.findByName(cbCountries.Text).ID;

            _contact.firstName = txtFirstName.Text;
            _contact.lastName = txtLastName.Text;
            _contact.email = txtEmail.Text;
            _contact.phone = txtPhone.Text;
            _contact.address = txtAddress.Text;
            _contact.dateOfBirth = dtpDate.Value;
            _contact.countryID = countryID;
        }
    }
}
