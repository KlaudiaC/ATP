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
    public partial class usunTrenera : Form
    {
        string connString;
        ATPForm form;
        SqlDataAdapter dataAdapter;
        SqlCommandBuilder commandBuilder;
        DataSet DS;
        SqlConnection conn;
        public usunTrenera(string con, ATPForm f)
        {
            InitializeComponent();
            connString = con;
            form = f;
            this.ControlBox = false;
            usunButton.Enabled = false;
            conn = new SqlConnection(connString);
            conn.Open();
            dataAdapter =
                   new SqlDataAdapter("SELECT * FROM TRENERZY", conn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);

            DS = new DataSet();
            dataAdapter.Fill(DS, "TREN");
            foreach (DataRow dataRow in DS.Tables["TREN"].Rows)
            {
                trenerComboBox.Items.Add(dataRow["Nazwisko"].ToString());
            }
        }

        private void anulujButton_Click(object sender, EventArgs e)
        {
            form.enableTabs();
            this.Close();
        }

        private void trenerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            usunButton.Enabled = true;
        }

        private void usunButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Czy chcesz usunąć trenera?", "UWAGA",
MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string nazwisko = trenerComboBox.SelectedItem.ToString();
                string command = "DELETE FROM TRENERZY WHERE Nazwisko = '" + nazwisko + "'";
                SqlCommand sqlcomm = new SqlCommand(command, conn);
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
