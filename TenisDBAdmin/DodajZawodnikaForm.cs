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
    public partial class DodajZawodnikaForm : Form
    {
        bool karVisible;
        string connString;
        ATPForm atpform;
        public DodajZawodnikaForm(string conString, ATPForm atp)
        {
            atpform = atp;
            connString = conString;
            InitializeComponent();
            this.ControlBox = false;
            karVisible = false;
            dodajButton2.Enabled = false;
            foreach (Control c in Controls)
            {
                if (c.TabIndex >= 12 && c.TabIndex <= 27)
                {
                    c.Visible = false;
                }
            }
        }


        private void textbox_textchanged()
        {
            {
                if (imieTextBox1.Text.Equals("") || nazwiskoTextBox.Text.Equals("") || krajTextBox.Text.Equals(""))
                {
                    dodajButton2.Enabled = false;
                }
                else
                    dodajButton2.Enabled = true;
            }
        }

        private void imieTextBox1_TextChanged(object sender, EventArgs e)
        {
            textbox_textchanged();
        }

        private void karieraInfoButton_Click(object sender, EventArgs e)
        {
            karVisible = !karVisible;
            PunktyTextBox.Enabled = false;
            foreach (Control c in Controls)
            {
                if (c.TabIndex >= 12 && c.TabIndex <= 27)
                {
                    c.Visible = karVisible;
                }
            }
        }

        private void nazwiskoTextBox_TextChanged(object sender, EventArgs e)
        {
            textbox_textchanged();
        }

        private void krajTextBox_TextChanged(object sender, EventArgs e)
        {
            textbox_textchanged();
        }

        private void dodajButton2_Click(object sender, EventArgs e)
        {
            SqlConnection conn;
            conn = new SqlConnection(connString);
            conn.Open();
            string aktywny;
            if(aktywnyCheckBox.Checked == true)
            {
                aktywny = "1";
            }
            else
            {
                aktywny = "0";
            }
            string command = "exec dbo.dodajZaw '" + nazwiskoTextBox.Text + "', '" + imieTextBox1.Text + "', '" + krajTextBox.Text + "', " + aktywny;
            SqlCommand sqlcomm = new SqlCommand(command, conn);
            sqlcomm.ExecuteNonQuery();
            if(aktywny == "1")
            {
                PunktyTextBox.Enabled = true;
            }
            if(PunktyTextBox.Text.Equals(""))
                command = "exec dbo.dodaj'" + nazwiskoTextBox.Text + "', 0";
            else
                command = "exec dbo.dodaj'" + nazwiskoTextBox.Text + "', " + PunktyTextBox.Text;
            sqlcomm = new SqlCommand(command, conn);
            sqlcomm.ExecuteNonQuery();
            if (!karVisible)
            {
                command = "exec dbo.dodajKar '" + nazwiskoTextBox.Text + "', 0, null, 0, 0, 0, 0, 0";
            }
            else
            {
                string poczatek = startKarTextBox.Text;
                if (poczatek.Equals(""))
                    poczatek = "0";
                string koniec = konKarTextBox.Text;
                if (koniec.Equals(""))
                    koniec = "null";
                string tytuly = tytulyTextBox.Text;
                if (tytuly.Equals(""))
                    tytuly = "0";
                string finaly = finalyTextBox.Text;
                if (finaly.Equals(""))
                    finaly = "0";
                string zwyciestwa = zwyciestwaTextBox.Text;
                if (zwyciestwa.Equals(""))
                    zwyciestwa = "0";
                string porazki = porazkiTextBox.Text;
                if (porazki.Equals(""))
                    porazki = "0";
                string pieniadze = moneyTextBox.Text;
                if (pieniadze.Equals(""))
                    pieniadze = "0";
                command = "exec dbo.dodajKar '" + nazwiskoTextBox.Text + "',"+ poczatek +","+koniec 
                    +","+ tytuly + "," + finaly + "," + zwyciestwa + "," + porazki + "," + pieniadze;
            }
            sqlcomm = new SqlCommand(command, conn);
            sqlcomm.ExecuteNonQuery();
            atpform.refreshDataGridViews();
            atpform.enableTabs();
            this.Close();
            //exec dbo.dodajZaw 'Murray', 'Andy', 'Wielka Brytania', 1;
        }

        private void aktywnyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (aktywnyCheckBox.Checked)
            {
                PunktyTextBox.Enabled = true;
            }
            else
                PunktyTextBox.Enabled = false;
        }

        private void anulujButton_Click(object sender, EventArgs e)
        {
            atpform.enableTabs();
            this.Close();
        }
    }
}