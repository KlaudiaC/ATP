using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TenisDBUser
{
    public partial class usunOlimp : Form
    {
        string connString;
        ATPForm form;
        SqlDataAdapter dataAdapter;
        SqlCommandBuilder commandBuilder;
        DataSet DS;
        SqlConnection conn;
        public usunOlimp(string con, ATPForm f)
        {
            connString = con;
            form = f;
            InitializeComponent();
            this.ControlBox = false;
            usunButton.Enabled = false;
            conn = new SqlConnection(connString);
            conn.Open();
            dataAdapter =
                   new SqlDataAdapter("SELECT * FROM OLIMPIADY", conn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);

            DS = new DataSet();
            dataAdapter.Fill(DS, "OLIMP");
            foreach (DataRow dataRow in DS.Tables["OLIMP"].Rows)
            {
                rokComboBox.Items.Add(dataRow["Rok"].ToString());
            }
        }

        private void anulujButton_Click(object sender, EventArgs e)
        {
            form.enableTabs();
            this.Close();
        }

        private void rokComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            usunButton.Enabled = true;
            dataAdapter =
                   new SqlDataAdapter("SELECT Miejsce FROM OLIMPIADY WHERE Rok = " + rokComboBox.SelectedItem.ToString(), conn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            DataSet t = new DataSet();
            dataAdapter.Fill(t, "LAB");
            foreach (DataRow dataRow in t.Tables["LAB"].Rows)
            {
               myLabel.Text = dataRow["Miejsce"].ToString();
            }
        }

        private void usunButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Czy chcesz usunąć olimpiadę?", "UWAGA",
        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string rok = rokComboBox.SelectedItem.ToString();
                string command = "DELETE FROM OLIMPIADY_WYNIKI WHERE RokTurnieju = " + rok;
                SqlCommand sqlcomm = new SqlCommand(command, conn);
                sqlcomm.ExecuteNonQuery();
                command = "DELETE FROM OLIMPIADY WHERE Rok = " + rok;
                sqlcomm = new SqlCommand(command, conn);
                sqlcomm.ExecuteNonQuery();
                form.refreshDataGridViews();
                form.enableTabs();
                this.Close();
            }
            else if (result == DialogResult.No)
            {
                form.enableTabs();
                this.Close();
            }
        }
    }
}
