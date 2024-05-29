using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactsBusinessLayer;


namespace WindowsFormLayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _RefreshContactsList();
        }

        private void _RefreshContactsList ()
        {
            dgv.DataSource = clsContact.getAllContacts();
        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {
            EditContact editContact = new EditContact(-1);
            editContact.ShowDialog();
        }
    }
}
