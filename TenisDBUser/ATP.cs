using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TenisDBUser
{
    public partial class ATP : Form
    {
        SqlConnection conn;
        SqlDataAdapter dataAdapter = null;
        string connectionString;
        DataSet dataSet = null;

        public ATP()
        {
            // connectionString = "Server=PATRYK;Database=ProjectTenis;Trusted_Connection=true";
            connectionString = "Server = KLAUDIA_PC\\SQLEXPRESS; Database = ProjectTenis; Trusted_Connection = true";
            conn = new SqlConnection(connectionString);
            conn.Open();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataSet = new DataSet();

            dataAdapter = new SqlDataAdapter("SELECT * FROM ZAWODNICY", conn);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dataSet, "ZAWODNICY");
            ZawodnicyDataGridView.DataSource = dataSet.Tables["ZAWODNICY"];
            ZawodnicyDataGridView.ReadOnly = true;

            dataAdapter = new SqlDataAdapter("SELECT * FROM rankingView ORDER BY Punkty DESC", conn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dataSet, "RANKING");
            RankingDataGridView.DataSource = dataSet.Tables["Ranking"];

            dataAdapter = new SqlDataAdapter("SELECT * FROM karieryView", conn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dataSet, "KARIERY");
            KarieryDataGridView.DataSource = dataSet.Tables["KARIERY"];

            dataAdapter = new SqlDataAdapter("SELECT * FROM trenerzyView", conn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dataSet, "TRENERZY");
            TrenerzyDataGridView.DataSource = dataSet.Tables["TRENERZY"];
            
            dataAdapter = new SqlDataAdapter("SELECT * FROM mastersView", conn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dataSet, "MASTERS");
            MastersDataGridView.DataSource = dataSet.Tables["MASTERS"];
            for (int i = 0; i < 8; i++)
                MastersDataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            
            dataAdapter = new SqlDataAdapter("SELECT * FROM szlemyView", conn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dataSet, "SZLEMY");
            SzlemyDataGridView.DataSource = dataSet.Tables["SZLEMY"];
            for (int i = 0; i < 10; i++)
                SzlemyDataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataAdapter = new SqlDataAdapter("SELECT * FROM olimpiadyView", conn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dataSet, "OLIMPIADY");
            OlimpiadyDataGridView.DataSource = dataSet.Tables["OLIMPIADY"];
            for (int i = 0; i < 11; i++)
                OlimpiadyDataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataAdapter = new SqlDataAdapter("SELECT * FROM rekordyView", conn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dataSet, "REKORDY");
            RekordyDataGridView.DataSource = dataSet.Tables["REKORDY"];
            for (int i = 0; i < 4; i++)
                RekordyDataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

            krajTextBox.Enabled = false;
            OKButton.Enabled = false;
            FromTextBox.Enabled = false;
            ToTextBox.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet filtrDataSet;
            filtrDataSet = new DataSet();
            OKButton.Enabled = false;
            krajTextBox.Enabled = false;
            FromTextBox.Enabled = false;
            ToTextBox.Enabled = false;

            String s = rozwijalnaLista1.Text.ToString();
            if (s.Equals("Kraj"))
            {
                OKButton.Enabled = true;
                krajTextBox.Enabled = true;
                czyKraj = true;
            }
            else if (s.Equals("Aktywni"))
            {
                dataAdapter = new SqlDataAdapter("SELECT * FROM aktywni", conn);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                filtrDataSet = new DataSet();
                dataAdapter.Fill(filtrDataSet, "AKTYWNI");
                FiltrDataGridView.DataSource = filtrDataSet.Tables["AKTYWNI"];
                FiltrDataGridView.ReadOnly = true;
            }
            else if (s.Equals("Nieaktywni"))
            {
                dataAdapter = new SqlDataAdapter("SELECT * FROM nieaktywni", conn);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                filtrDataSet = new DataSet();
                dataAdapter.Fill(filtrDataSet, "NIEAKTYWNI");
                FiltrDataGridView.DataSource = filtrDataSet.Tables["NIEAKTYWNI"];
                FiltrDataGridView.ReadOnly = true;
            }
            else if(s.Equals("Skutecznosc"))
            {
                dataAdapter = new SqlDataAdapter("SELECT * FROM Skutecznosc", conn);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                filtrDataSet = new DataSet();
                dataAdapter.Fill(filtrDataSet, "SKUTECZNOSC");
                FiltrDataGridView.DataSource = filtrDataSet.Tables["SKUTECZNOSC"];
                FiltrDataGridView.ReadOnly = true;
            }
            else if(s.Equals("Okres kariery"))
            {
                FromTextBox.Enabled = true;
                ToTextBox.Enabled = true;
                OKButton.Enabled = true;
                czyKraj = false; 
            }
        }

        Boolean czyKraj;
        private void OKButton_Click(object sender, EventArgs e)
        {
            DataSet filtrDataSet;
            if (czyKraj == true)
            {
                String kraj;
                kraj = krajTextBox.Text.ToString();
                dataAdapter =  new SqlDataAdapter("SELECT * FROM wszystkoView WHERE Kraj = '" + kraj + "'", conn);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                filtrDataSet = new DataSet();
                dataAdapter.Fill(filtrDataSet, "F_KRAJ");
                FiltrDataGridView.DataSource = filtrDataSet.Tables["F_KRAJ"];
                FiltrDataGridView.ReadOnly = true;
            }
            else
            {
                String from = FromTextBox.Text.ToString();
                String to = ToTextBox.Text.ToString();
                if(to.Equals(""))
                {
                    dataAdapter = new SqlDataAdapter("SELECT * FROM okresKarieryStart(" + from + ")", conn);
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                    filtrDataSet = new DataSet();
                    dataAdapter.Fill(filtrDataSet, "OKRES");
                    FiltrDataGridView.DataSource = filtrDataSet.Tables["OKRES"];
                    FiltrDataGridView.ReadOnly = true;
                }
                else
                {
                    dataAdapter = new SqlDataAdapter("SELECT * FROM okresKarieryStartKoniec(" + from + "," + to + ")", conn);
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                    filtrDataSet = new DataSet();
                    dataAdapter.Fill(filtrDataSet, "OKRES");
                    FiltrDataGridView.DataSource = filtrDataSet.Tables["OKRES"];
                    FiltrDataGridView.ReadOnly = true;
                }
            }
        }

        private void krajTextBox_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OKButton.PerformClick();
                // these last two lines will stop the beep sound
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void FromTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OKButton.PerformClick();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void ToTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OKButton.PerformClick();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }
    }
}