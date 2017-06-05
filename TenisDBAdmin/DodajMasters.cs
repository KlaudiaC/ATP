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
    public partial class DodajMasters : Form
    {
        string connString;
        ATPForm atpform;
        bool extravisible;
        public DodajMasters(string conString, ATPForm atp)
        {
            InitializeComponent();
            extravisible = true;
            extraClick();
            atpform = atp;
            connString = conString;
            this.ControlBox = false;
            dodajButton.Enabled = false;
        }

        private void extraClick()
        {
            {
                extravisible = !extravisible;
                foreach (Control c in Controls)
                {
                    if (c.TabIndex >= 34 && c.TabIndex <= 47)
                    {
                        c.Visible = extravisible;
                    }
                }
            }
        }

        private void anulujButton_Click(object sender, EventArgs e)
        {
            atpform.enableTabs();
            this.Close();
        }

        private void miejsceTextBox_TextChanged(object sender, EventArgs e)
        {
            textbox_textchanged();
        }

        private void nawierchniaTextBox_TextChanged(object sender, EventArgs e)
        {
            textbox_textchanged();
        }
        private void textbox_textchanged()
        {
            {
                if (nazwaTextBox.Text.Equals("") || nawierzchniaTextBox.Text.Equals("") )
                {
                    dodajButton.Enabled = false;
                }
                else
                    dodajButton.Enabled = true;
            }
        }

        private void extraButton_Click(object sender, EventArgs e)
        {
            extraClick();
        }

        private void dodajButton_Click(object sender, EventArgs e)
        {
            SqlConnection conn;
            conn = new SqlConnection(connString);
            conn.Open();
            string nazwa = nazwaTextBox.Text;
            string nawierzchnia = nawierzchniaTextBox.Text;
            string command = "exec dbo.dodajTurniejMasters '" + nazwa + "' , '" + nawierzchnia + "'";
            SqlCommand sqlcomm = new SqlCommand(command, conn);
            sqlcomm.ExecuteNonQuery();
            //exec dbo.dodajTurniejMasters 'Indian Wells', 'Twarda'; sqlcomm.ExecuteNonQuery();
            string zwyciezca, finalista;
            string set1, set2, set3;
            zwyciezca = zwyciezcaTextBox.Text;
            finalista = finalistaTextBox.Text;
            set1 = Set1TextBox.Text;
            set2 = Set2TextBox.Text;
            set3 = Set3TextBox.Text;
            if (zwyciezca.Equals(""))
            {
                zwyciezca = "null";
            }
            if (finalista.Equals(""))
            {
                finalista = "null";
            }
            if (set1.Equals(""))
            {
                set1 = "null";
            }
            if (set2.Equals(""))
            {
                set2 = "null";
            }
            if (set3.Equals(""))
            {
                set3 = "null";
            }
            command = "exec dbo.dodajWynikMasters '" + nazwa + "' ,'" + zwyciezca + "', '" + finalista + "', '" + set1 + "', '" + set2 + "', '" + set3 + "'";
            //exec dbo.dodajWynikMasters 'Indian Wells', 'Djokovic', 'Federer','6:3', '6:7(5)', '6:2';
            sqlcomm = new SqlCommand(command, conn);
            sqlcomm.ExecuteNonQuery();
            atpform.refreshDataGridViews();
            atpform.enableTabs();
            this.Close();
        }
    }
}
