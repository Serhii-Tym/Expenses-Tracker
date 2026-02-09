using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Expenses_Tracker
{
    public partial class Create_Expenses : Form
    {
        string dbPath = "expenses.txt";

        public Create_Expenses()
        {
            InitializeComponent();

            this.button1.Click += new System.EventHandler(this.SaveExpense_Click);
            this.button2.Click += new System.EventHandler(this.Cancel_Click);
        }

        private void SaveExpense_Click(object sender, EventArgs e)
        {
            string description = textBox1.Text;
            string amountText = textBox2.Text;

            if (string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(amountText))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(amountText, out decimal amount))
            {
                MessageBox.Show("Please enter a valid number for the amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show($"Are you sure you want to save this expense?\n\n{description}: £{amount}",
                                                  "Confirm Save",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (StreamWriter sw = File.AppendText(dbPath))
                    {
                        sw.WriteLine($"{description}|{amount}");
                    }

                    MessageBox.Show("Expense saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving data: " + ex.Message);
                }
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to cancel? Unsaved data will be lost.",
                                                  "Confirm Cancel",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
