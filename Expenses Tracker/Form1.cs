using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Expenses_Tracker
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            Create_Expenses createForm = new Create_Expenses();
            this.Hide();
            createForm.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Manage_Expenses manageForm = new Manage_Expenses();
            this.Hide();
            manageForm.ShowDialog();
            this.Show();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Main Menu";
        }
    }
}