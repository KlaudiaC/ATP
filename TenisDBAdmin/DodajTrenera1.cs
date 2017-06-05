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
    public partial class DodajTrenera1 : Form
    {
        string connString;
        ATPForm atpform;
        public DodajTrenera1(string conString, ATPForm atp)
        {
            InitializeComponent();
            atpform = atp;
            connString = conString;
            this.ControlBox = false;
            dodajButton.Enabled = false;
        }

        private void AnulujButton_Click(object sender, EventArgs e)
        {
            atpform.enableTabs();
            this.Close();
        }

        private void dodajButton_Click(object sender, EventArgs e)
        {
            SqlConnection conn;
            conn = new SqlConnection(connString);
            conn.Open();
            string imie = imieTextbox.Text;
            string nazwisko = nazwiskoTextbox.Text;
            string zawodnik = zawodnikTextbox.Text;
            string command;
            if (zawodnik.Equals(""))
            {
                zawodnik = "null";
                command = "exec dbo.dodajTren '" + nazwisko + "', '" + imie + "', " + zawodnik;
            }
            else
            {
                command = "exec dbo.dodajTren '" + nazwisko + "', '" + imie + "', '" + zawodnik + "'";
            }
            SqlCommand sqlcomm = new SqlCommand(command, conn);
            sqlcomm.ExecuteNonQuery();
            atpform.refreshDataGridViews();
            atpform.enableTabs();
            this.Close();

        }

        private void imieTextbox_TextChanged(object sender, EventArgs e)
        {
            textbox_textchanged();
        }

        private void nazwiskoTextbox_TextChanged(object sender, EventArgs e)
        {
            textbox_textchanged();
        }

        private void textbox_textchanged()
        {
            {
                if (imieTextbox.Text.Equals("") || nazwiskoTextbox.Text.Equals("") )
                {
                    dodajButton.Enabled = false;
                }
                else
                    dodajButton.Enabled = true;
            }
        }
    }
}
