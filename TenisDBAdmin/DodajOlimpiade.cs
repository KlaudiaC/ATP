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
    public partial class DodajOlimpiade : Form
    {
        string connString;
        ATPForm atpform;
        bool extravisible;
        public DodajOlimpiade(string conString, ATPForm atp)
        {
            InitializeComponent();
            extravisible = true;
            extraClick();
            atpform = atp;
            connString = conString;
            this.ControlBox = false;
            dodajButton.Enabled = false;
        }

        private void anulujButton_Click(object sender, EventArgs e)
        {
            atpform.enableTabs();
            this.Close();
        }

        private void extraButton_Click(object sender, EventArgs e)
        {
            extraClick();
        }
        private void extraClick()
        {
            {
                extravisible = !extravisible;
                foreach (Control c in Controls)
                {
                    if (c.TabIndex >= 9 && c.TabIndex <= 24)
                    {
                        c.Visible = extravisible;
                    }
                }
            }
        }

        private void rokTextBox_TextChanged(object sender, EventArgs e)
        {
            textbox_textchanged();
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
                if (rokTextBox.Text.Equals("") || miejsceTextBox.Text.Equals("") || nawierchniaTextBox.Text.Equals(""))
                {
                    dodajButton.Enabled = false;
                }
                else
                    dodajButton.Enabled = true;
            }
        }

        private void dodajButton_Click(object sender, EventArgs e)
        {
            SqlConnection conn;
            conn = new SqlConnection(connString);
            conn.Open();
            string rok = rokTextBox.Text;
            string command = "exec dbo.dodajOlimpiade " + rok + " , '" + miejsceTextBox.Text + "', '" + nawierchniaTextBox.Text + "'";
            SqlCommand sqlcomm = new SqlCommand(command, conn);
            //      exec dbo.dodajOlimpiade 2016, 'Rio de Janerio', 'Twarda';
            sqlcomm.ExecuteNonQuery();
            string zloto, srebro, braz;
            string set1, set2, set3, set4, set5;
            zloto = zlotoTextBox.Text;
            srebro = srebroTextBox.Text;
            braz = brazTextBox.Text;
            set1 = Set1TextBox.Text;
            set2 = Set2TextBox.Text;
            set3 = Set3TextBox.Text;
            set4 = Set4TextBox.Text;
            set5 = Set5TextBox.Text;
            if (zloto.Equals(""))
            {
                zloto = "null";
            }
            if (srebro.Equals(""))
            {
                srebro = "null";
            }
            if (braz.Equals(""))
            {
                braz = "null";
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
            if (set4.Equals(""))
            {
                set4 = "null";
            }
            if (set5.Equals(""))
            {
                set5 = "null";
            }
            command = "exec dbo.dodajWynikOlimpiada " + rok + " ,'" + zloto + "', '" + srebro + "', '" + braz + "', '" + set1 + "', '" + set2 + "', '" + set3 + "', '" + set4 + "', '" + set5 + "'";
            //exec dbo.dodajWynikOlimpiada 2008, 'Nadal', 'Gonzalez', 'Djokovic', '6:3', '7:6(5)', '6:3', null, null;
            sqlcomm = new SqlCommand(command, conn);
            sqlcomm.ExecuteNonQuery();
            atpform.refreshDataGridViews();
            atpform.enableTabs();
            this.Close();
        }
    }
}
