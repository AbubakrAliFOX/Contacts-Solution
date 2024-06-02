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

        private void contextMenuEdit_Click(object sender, EventArgs e)
        {
            EditContact newForm = new EditContact((int)dgv.CurrentRow.Cells[0].Value);
            newForm.ShowDialog();
            _RefreshContactsList();
        }

        private void contextMenuDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete contact [" + dgv.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //Perform Delele and refresh
                if (clsContact.delete((int)dgv.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Contact Deleted Successfully.");
                    _RefreshContactsList();
                }

                else
                    MessageBox.Show("Contact is not deleted.");


            }


        }
    }
}
