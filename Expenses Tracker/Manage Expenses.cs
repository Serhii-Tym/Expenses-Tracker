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
    public partial class Manage_Expenses : Form
    {
        string dbPath = "expenses.txt";

        private DataGridView dgvExpenses;
        private Button btnDelete;
        private Button btnExit;

        public Manage_Expenses()
        {
            InitializeComponent();
            SetupUI();
            LoadData();
        }

        private void SetupUI()
        {
            this.Size = new Size(600, 500);
            this.Text = "Manage Expenses";


            dgvExpenses = new DataGridView();
            dgvExpenses.Location = new Point(20, 20);
            dgvExpenses.Size = new Size(540, 350);
            dgvExpenses.ColumnCount = 2;
            dgvExpenses.Columns[0].Name = "Description";
            dgvExpenses.Columns[1].Name = "Amount (£)";
            dgvExpenses.AllowUserToAddRows = false;
            dgvExpenses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvExpenses.MultiSelect = false;
            this.Controls.Add(dgvExpenses);

            btnDelete = new Button();
            btnDelete.Text = "Delete Expense";
            btnDelete.Location = new Point(20, 390);
            btnDelete.Size = new Size(150, 40);
            btnDelete.Click += BtnDelete_Click;
            this.Controls.Add(btnDelete);

            btnExit = new Button();
            btnExit.Text = "Exit Program";
            btnExit.Location = new Point(410, 390);
            btnExit.Size = new Size(150, 40);
            btnExit.Click += BtnExit_Click;
            this.Controls.Add(btnExit);
        }

        private void LoadData()
        {
            dgvExpenses.Rows.Clear();

            if (File.Exists(dbPath))
            {
                string[] lines = File.ReadAllLines(dbPath);
                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        string[] parts = line.Split('|');
                        if (parts.Length == 2)
                        {
                            dgvExpenses.Rows.Add(parts[0], "£" + parts[1]);
                        }
                    }
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvExpenses.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this expense?",
                                                      "Confirm Delete",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    int selectedIndex = dgvExpenses.SelectedRows[0].Index;

                    dgvExpenses.Rows.RemoveAt(selectedIndex);

                    RewriteFile();
                }
            }
            else
            {
                MessageBox.Show("Please select an expense to delete.");
            }
        }

        private void RewriteFile()
        {
            using (StreamWriter sw = new StreamWriter(dbPath))
            {
                foreach (DataGridViewRow row in dgvExpenses.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        string desc = row.Cells[0].Value.ToString();
                        string amt = row.Cells[1].Value.ToString().Replace("£", "");
                        sw.WriteLine($"{desc}|{amt}");
                    }
                }
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit the program?",
                                                  "Confirm Exit",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
