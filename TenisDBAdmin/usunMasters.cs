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
    public partial class usunMasters : Form
    {
        string connString;
        ATPForm form;
        SqlDataAdapter dataAdapter;
        SqlCommandBuilder commandBuilder;
        DataSet DS;
        SqlConnection conn;
        public usunMasters(string con, ATPForm f)
        {
            connString = con;
            form = f;
            InitializeComponent();
            this.ControlBox = false;
            usunButton.Enabled = false;
            conn = new SqlConnection(connString);
            conn.Open();
            dataAdapter =
                   new SqlDataAdapter("SELECT * FROM MASTERS_TURNIEJE", conn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);

            DS = new DataSet();
            dataAdapter.Fill(DS, "MASTERS");
            foreach (DataRow dataRow in DS.Tables["MASTERS"].Rows)
            {
                nazwaComboBox.Items.Add(dataRow["NazwaTurnieju"].ToString());
            }
        }

        private void anulujButton_Click(object sender, EventArgs e)
        {
            form.enableTabs();
            this.Close();
        }

        private void nazwaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            usunButton.Enabled = true;
        }

        private void usunButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Czy chcesz usunąć turniej?", "UWAGA",
        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string nazwa = nazwaComboBox.SelectedItem.ToString();
                string command = "DELETE FROM MASTERS_WYNIKI WHERE IdTurnieju = (SELECT Id From MASTERS_TURNIEJE WHERE NazwaTurnieju = '"+ nazwa +"') ";
                SqlCommand sqlcomm = new SqlCommand(command, conn);
                sqlcomm.ExecuteNonQuery();
                command = "DELETE FROM MASTERS_TURNIEJE WHERE NazwaTurnieju = '" + nazwa +"'";
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
