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
    public partial class UsunZawodnika : Form
    {
        string connString;
        ATPForm form;
        SqlDataAdapter dataAdapter;
        SqlCommandBuilder commandBuilder;
        DataSet DS;
        SqlConnection conn;
        public UsunZawodnika(string con, ATPForm f)
        {
            connString = con;
            form = f;
            InitializeComponent();
            this.ControlBox = false;
            usunButton.Enabled = false;

            conn = new SqlConnection(connString);
            conn.Open();
            dataAdapter =
                   new SqlDataAdapter("SELECT * FROM ZAWODNICY", conn);

            commandBuilder = new SqlCommandBuilder(dataAdapter);

            DS = new DataSet();
            dataAdapter.Fill(DS, "ZAWODNICY");

            foreach (DataRow dataRow in DS.Tables["ZAWODNICY"].Rows)
            {
                zawComboBox.Items.Add(dataRow["Nazwisko"].ToString());
            }
        }

        private void anulujButton_Click(object sender, EventArgs e)
        {
            form.enableTabs();
            this.Close();
        }

        private void usunButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Usunięcie zawodnika spowoduje usunięcie"+ 
                " wszystkich danych o finałach turniejów, w których uczestniczył. Czy chesz kontynuować?", "UWAGA",
            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string nazwisko = zawComboBox.SelectedItem.ToString();
                string command = "exec dbo.usunZawodnika '" + nazwisko + "'";
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

        private void zawComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            usunButton.Enabled = true;
       //     if(zawComboBox.SelectedItem)
        }
    }
}
