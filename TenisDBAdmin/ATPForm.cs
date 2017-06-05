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
    public partial class ATPForm : Form
    {
        SqlConnection conn;
        SqlDataAdapter dataAdapter = null;
        string connectionString;
        DataSet dataSet = null;

        public ATPForm()
        {
            // connectionString = "Server=PATRYK;Database=ProjectTenis;Trusted_Connection=true";
            connectionString = "Server = KLAUDIA_PC\\SQLEXPRESS; Database = ProjectTenis; Trusted_Connection = true";
            conn = new SqlConnection(connectionString);
            conn.Open();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ZawodnicyOKButton.Enabled = false;
            ZawodnicyAnulujButton.Enabled = false;

            RankingOKButton.Enabled = false;
            RankingAnulujButton.Enabled = false;

            KarieryOKButton.Enabled = false;
            KarieryAnulujButton.Enabled = false;

            MastersOKButton.Enabled = false;
            MastersAnulujButton.Enabled = false;

            SzlemyOKButton.Enabled = false;
            SzlemyAnulujButton.Enabled = false;

            OlimpiadyOKButton.Enabled = false;
            OlimpiadyAnulujButton.Enabled = false;

            TrenerzyOKButton.Enabled = false;
            TrenerzyAnulujButton.Enabled = false;

            dataSet = new DataSet();

            string cmd = "IF (SELECT COUNT(*) FROM REKORDY) = 0 exec.dbo.zainicjujRekordy";
            SqlCommand sqlcomm = new SqlCommand(cmd, conn);
            sqlcomm.ExecuteNonQuery();
            cmd = "exec dbo.aktualizujRekordy";
            sqlcomm = new SqlCommand(cmd, conn);
            sqlcomm.ExecuteNonQuery();

            dataAdapter = new SqlDataAdapter("SELECT * FROM ZAWODNICY", conn);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dataSet, "ZAWODNICY");
            ZawodnicyDataGridView.DataSource = dataSet.Tables["ZAWODNICY"];

            dataAdapter = new SqlDataAdapter("SELECT * FROM rankingView ORDER BY Punkty DESC", conn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dataSet, "RANKING");
            RankingDataGridView.DataSource = dataSet.Tables["RANKING"];

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

        public void refreshDataGridViews()
        {
            dataSet.Tables["ZAWODNICY"].Clear();
            dataAdapter = new SqlDataAdapter("SELECT * FROM ZAWODNICY", conn);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dataSet, "ZAWODNICY");
            KarieryDataGridView.DataSource = dataSet.Tables["ZAWODNICY"];

            dataSet.Tables["KARIERY"].Clear();
            dataAdapter = new SqlDataAdapter("SELECT * FROM karieryView", conn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dataSet, "KARIERY");
            KarieryDataGridView.DataSource = dataSet.Tables["KARIERY"];

            dataSet.Tables["RANKING"].Clear();
            dataAdapter = new SqlDataAdapter("SELECT * FROM rankingView ORDER BY Punkty DESC", conn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dataSet, "RANKING");
            RankingDataGridView.DataSource = dataSet.Tables["RANKING"];

            dataSet.Tables["KARIERY"].Clear();
            dataAdapter = new SqlDataAdapter("SELECT * FROM karieryView", conn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dataSet, "KARIERY");
            KarieryDataGridView.DataSource = dataSet.Tables["KARIERY"];

            dataSet.Tables["TRENERZY"].Clear();
            dataAdapter = new SqlDataAdapter("SELECT * FROM trenerzyView", conn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dataSet, "TRENERZY");
            TrenerzyDataGridView.DataSource = dataSet.Tables["TRENERZY"];

            dataSet.Tables["MASTERS"].Clear();
            dataAdapter = new SqlDataAdapter("SELECT * FROM mastersView", conn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dataSet, "MASTERS");
            MastersDataGridView.DataSource = dataSet.Tables["MASTERS"];

            dataSet.Tables["SZLEMY"].Clear();
            dataAdapter = new SqlDataAdapter("SELECT * FROM szlemyView", conn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dataSet, "SZLEMY");
            SzlemyDataGridView.DataSource = dataSet.Tables["SZLEMY"];

            dataSet.Tables["OLIMPIADY"].Clear();
            dataAdapter = new SqlDataAdapter("SELECT * FROM olimpiadyView", conn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dataSet, "OLIMPIADY");
            OlimpiadyDataGridView.DataSource = dataSet.Tables["OLIMPIADY"];

            dataSet.Tables["REKORDY"].Clear();
            dataAdapter = new SqlDataAdapter("SELECT * FROM rekordyView", conn);
            commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dataSet, "REKORDY");
            RekordyDataGridView.DataSource = dataSet.Tables["REKORDY"];
        }

        private void ZawodnicyEdytujButton_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                if(tab != zawodnicyTabPage)
                    tab.Enabled = false;
            }
            ZawodnicyDataGridView.ReadOnly = false;
            ZawodnicyDataGridView.Columns[0].ReadOnly = true;
            ZawodnicyEdytujButton.Enabled = false;
            ZawodnicyAnulujButton.Enabled = true;
            ZawodnicyOKButton.Enabled = true;
            dataSet.AcceptChanges();
        }

        private void ZawodnicyAnulujButton_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                    tab.Enabled = true;
            }
            dataSet.RejectChanges();
            ZawodnicyDataGridView.Refresh();
            ZawodnicyDataGridView.Refresh();
            ZawodnicyDataGridView.ReadOnly = true;
            ZawodnicyEdytujButton.Enabled = true;
            ZawodnicyAnulujButton.Enabled = false;
            ZawodnicyOKButton.Enabled = false;
        }

        private void ZawodnicyOKButton_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                tab.Enabled = true;
            }
            dataAdapter = new SqlDataAdapter("SELECT * FROM ZAWODNICY", conn);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Update(dataSet, "ZAWODNICY");

            ZawodnicyDataGridView.ReadOnly = true;
            ZawodnicyEdytujButton.Enabled = true;
            ZawodnicyAnulujButton.Enabled = false;
            ZawodnicyOKButton.Enabled = false;
            refreshDataGridViews();
        }

        private void RankingEdytujButton_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                if (tab != rankingTabPage)
                    tab.Enabled = false;
            }
            RankingDataGridView.ReadOnly = false;
            RankingDataGridView.Columns[0].ReadOnly = true;
            RankingDataGridView.Columns[1].ReadOnly = true;
            RankingDataGridView.Columns[2].ReadOnly = true;
            RankingEdytujButton.Enabled = false;
            RankingAnulujButton.Enabled = true;
            RankingOKButton.Enabled = true;
            dataSet.AcceptChanges();
        }

        private void RankingAnulujButton_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                tab.Enabled = true;
            }
            dataSet.RejectChanges();
            RankingDataGridView.Refresh();
            RankingDataGridView.Refresh();
            RankingDataGridView.ReadOnly = true;
            RankingEdytujButton.Enabled = true;
            RankingAnulujButton.Enabled = false;
            RankingOKButton.Enabled = false;
        }

        private void RankingOKButton_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                tab.Enabled = true;
            }
            dataAdapter = new SqlDataAdapter("SELECT * FROM rankingView ORDER BY Punkty DESC", conn);
            SqlCommand command = new SqlCommand("UPDATE RANKING SET Punkty = @Punkty WHERE Id_Zawodnika = @ID", conn);
            command.Parameters.Add(new SqlParameter("@Punkty", SqlDbType.Int, 5, "Punkty"));
            command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 5, "Id_Zawodnika"));
            dataAdapter.UpdateCommand = command;
            dataAdapter.Update(dataSet, "RANKING");
            RankingDataGridView.DataSource = null;
            RankingDataGridView.ReadOnly = true;
            RankingEdytujButton.Enabled = true;
            RankingAnulujButton.Enabled = false;
            RankingOKButton.Enabled = false;
            refreshDataGridViews();
        }

        private void KarieryEdytujButton_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                if (tab != karieryTabPage)
                    tab.Enabled = false;
            }
            KarieryDataGridView.ReadOnly = false;
            KarieryDataGridView.Columns[0].ReadOnly = true;
            KarieryDataGridView.Columns[1].ReadOnly = true;
            KarieryDataGridView.Columns[2].ReadOnly = true;
            KarieryEdytujButton.Enabled = false;
            KarieryAnulujButton.Enabled = true;
            KarieryOKButton.Enabled = true;
            dataSet.AcceptChanges();
        }

        private void KarieryAnulujButton_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                tab.Enabled = true;
            }
            dataSet.RejectChanges();
            KarieryDataGridView.Refresh();
            KarieryDataGridView.Refresh();
            KarieryDataGridView.ReadOnly = true;
            KarieryEdytujButton.Enabled = true;
            KarieryAnulujButton.Enabled = false;
            KarieryOKButton.Enabled = false;
        }

        private void KarieryOKButton_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                tab.Enabled = true;
            }
            dataAdapter = new SqlDataAdapter("SELECT * FROM karieryView", conn);

            SqlCommand command = new SqlCommand("UPDATE KARIERY SET Poczatek_kariery = @Poczatek_kariery, Koniec_kariery = @Koniec_kariery,"+
                " Tytuly = @Tytuly, Finaly = @Finaly, Zwyciestwa = @Zwyciestwa, Porazki = @Porazki, Zarobione_pieniadze = @Zarobione_pieniadze Where Id_Zawodnika=@Id_Zawodnika", conn);
            command.Parameters.Add(new SqlParameter("@Poczatek_kariery", SqlDbType.Int, 5, "Poczatek_kariery"));
            command.Parameters.Add(new SqlParameter("@Koniec_kariery", SqlDbType.Int, 5, "Koniec_kariery"));
            command.Parameters.Add(new SqlParameter("@Finaly", SqlDbType.Int, 5, "Finaly"));
            command.Parameters.Add(new SqlParameter("@Zwyciestwa", SqlDbType.Int, 5, "Zwyciestwa"));
            command.Parameters.Add(new SqlParameter("@Porazki", SqlDbType.Int, 5, "Porazki"));
            command.Parameters.Add(new SqlParameter("@Zarobione_pieniadze", SqlDbType.Int, 5, "Zarobione_pieniadze"));
            command.Parameters.Add(new SqlParameter("@Tytuly", SqlDbType.Int, 5, "Tytuly"));
            command.Parameters.Add(new SqlParameter("@Id_Zawodnika", SqlDbType.Int, 5, "Id_Zawodnika"));
            dataAdapter.UpdateCommand = command;
            dataAdapter.Update(dataSet, "KARIERY");

            KarieryDataGridView.ReadOnly = true;
            KarieryEdytujButton.Enabled = true;
            KarieryAnulujButton.Enabled = false;
            KarieryOKButton.Enabled = false;
            refreshDataGridViews();
        }

        private void TrenerzyEdytujButton_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                if (tab != trenerzyTabPage)
                    tab.Enabled = false;
            }
            TrenerzyDataGridView.ReadOnly = false;
            TrenerzyEdytujButton.Enabled = false;
            TrenerzyAnulujButton.Enabled = true;
            TrenerzyOKButton.Enabled = true;
            dataSet.AcceptChanges();
        }

        private void TrenerzyAnulujButton_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                tab.Enabled = true;
            }
            dataSet.RejectChanges();
            TrenerzyDataGridView.Refresh();
            TrenerzyDataGridView.Refresh();
            TrenerzyDataGridView.ReadOnly = true;
            TrenerzyEdytujButton.Enabled = true;
            TrenerzyAnulujButton.Enabled = false;
            TrenerzyOKButton.Enabled = false;
        }

        private void TrenerzyOKButton_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                tab.Enabled = true;
            }
            dataAdapter = new SqlDataAdapter("SELECT * FROM trenerzyView", conn);

            SqlCommand command = new SqlCommand("exec dbo.updTren @Nazwisko, @Imie, @NazwiskoZaw, @Id_Trenera", conn);
            command.Parameters.Add(new SqlParameter("@Imie", SqlDbType.VarChar, 30, "Imie trenera"));
            command.Parameters.Add(new SqlParameter("@Nazwisko", SqlDbType.VarChar, 30, "Nazwisko trenera"));
            command.Parameters.Add(new SqlParameter("@NazwiskoZaw", SqlDbType.VarChar, 30, "Nazwisko zawodnika"));
            command.Parameters.Add(new SqlParameter("@Id_Trenera", SqlDbType.Int, 5, "Id_Trenera"));
            dataAdapter.UpdateCommand = command;
            dataAdapter.Update(dataSet, "TRENERZY");

            TrenerzyDataGridView.ReadOnly = true;
            TrenerzyEdytujButton.Enabled = true;
            TrenerzyAnulujButton.Enabled = false;
            TrenerzyOKButton.Enabled = false;
            refreshDataGridViews();
        }

        private void MastersEdytujButton_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                if (tab != mastersTabPage)
                    tab.Enabled = false;
            }
            MastersDataGridView.ReadOnly = false;
            MastersEdytujButton.Enabled = false;
            MastersAnulujButton.Enabled = true;
            MastersOKButton.Enabled = true;
            dataSet.AcceptChanges();
        }

        private void MastersAnulujButton_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                tab.Enabled = true;
            }
            dataSet.RejectChanges();
            MastersDataGridView.Refresh();
            MastersDataGridView.Refresh();
            MastersDataGridView.ReadOnly = true;
            MastersEdytujButton.Enabled = true;
            MastersAnulujButton.Enabled = false;
            MastersOKButton.Enabled = false;
        }

        private void MastersOKButton_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                tab.Enabled = true;
            }
            dataAdapter =  new SqlDataAdapter("SELECT * FROM mastersView", conn);

            SqlCommand command = new SqlCommand("exec dbo.updMasters @ID, @NazwaTurnieju, @Nawierzchnia, @Zwyciezca, @Finalista, @I_SET, @II_SET, @III_SET", conn);
            command.Parameters.Add(new SqlParameter("@NazwaTurnieju", SqlDbType.VarChar, 30, "NazwaTurnieju"));
            command.Parameters.Add(new SqlParameter("@Nawierzchnia", SqlDbType.VarChar, 30, "Nawierzchnia"));
            command.Parameters.Add(new SqlParameter("@Zwyciezca", SqlDbType.VarChar, 30, "Nazwisko zwyciezcy"));
            command.Parameters.Add(new SqlParameter("@Finalista", SqlDbType.VarChar, 30, "Nazwisko finalisty"));
            command.Parameters.Add(new SqlParameter("@I_SET", SqlDbType.VarChar, 30, "I_SET"));
            command.Parameters.Add(new SqlParameter("@II_SET", SqlDbType.VarChar, 30, "II_SET"));
            command.Parameters.Add(new SqlParameter("@III_SET", SqlDbType.VarChar, 30, "III_SET"));
            command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 5, "Id"));
            dataAdapter.UpdateCommand = command;
            dataAdapter.Update(dataSet, "MASTERS");

            MastersDataGridView.ReadOnly = true;
            MastersEdytujButton.Enabled = true;
            MastersAnulujButton.Enabled = false;
            MastersOKButton.Enabled = false;
            refreshDataGridViews();
        }

        private void SzlemyEdytujButton_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                if (tab != szlemyTabPage)
                    tab.Enabled = false;
            }
            SzlemyDataGridView.ReadOnly = false;
            SzlemyEdytujButton.Enabled = false;
            SzlemyAnulujButton.Enabled = true;
            SzlemyOKButton.Enabled = true;
            dataSet.AcceptChanges();
        }

        private void SzlemyAnulujButton_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                tab.Enabled = true;
            }
            dataSet.RejectChanges();
            SzlemyDataGridView.Refresh();
            SzlemyDataGridView.Refresh();
            SzlemyDataGridView.ReadOnly = true;
            SzlemyEdytujButton.Enabled = true;
            SzlemyAnulujButton.Enabled = false;
            SzlemyOKButton.Enabled = false;
        }

        private void SzlemyOKButton_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                tab.Enabled = true;
            }
            dataAdapter = new SqlDataAdapter("SELECT * FROM szlemyView", conn);

            SqlCommand command = new SqlCommand("exec dbo.updSzlemy @ID, @NazwaTurnieju, @Nawierzchnia, @Zwyciezca, @Finalista, @I_SET, @II_SET, @III_SET, @IV_SET, @V_SET", conn);
            command.Parameters.Add(new SqlParameter("@NazwaTurnieju", SqlDbType.VarChar, 30, "NazwaTurnieju"));
            command.Parameters.Add(new SqlParameter("@Nawierzchnia", SqlDbType.VarChar, 30, "Nawierzchnia"));
            command.Parameters.Add(new SqlParameter("@Zwyciezca", SqlDbType.VarChar, 30, "Nazwisko zwyciezcy"));
            command.Parameters.Add(new SqlParameter("@Finalista", SqlDbType.VarChar, 30, "Nazwisko finalisty"));
            command.Parameters.Add(new SqlParameter("@I_SET", SqlDbType.VarChar, 30, "I_SET"));
            command.Parameters.Add(new SqlParameter("@II_SET", SqlDbType.VarChar, 30, "II_SET"));
            command.Parameters.Add(new SqlParameter("@III_SET", SqlDbType.VarChar, 30, "III_SET"));
            command.Parameters.Add(new SqlParameter("@IV_SET", SqlDbType.VarChar, 30, "IV_SET"));
            command.Parameters.Add(new SqlParameter("@V_SET", SqlDbType.VarChar, 30, "V_SET"));
            command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 5, "Id"));
            dataAdapter.UpdateCommand = command;
            dataAdapter.Update(dataSet, "SZLEMY");

            SzlemyDataGridView.ReadOnly = true;
            SzlemyEdytujButton.Enabled = true;
            SzlemyAnulujButton.Enabled = false;
            SzlemyOKButton.Enabled = false;
            refreshDataGridViews();
        }

        private void OlimpiadyEdytujButton_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                if (tab != olimpTabPage)
                    tab.Enabled = false;
            }
            OlimpiadyDataGridView.ReadOnly = false;
            OlimpiadyDataGridView.Columns[0].ReadOnly = true;
            OlimpiadyEdytujButton.Enabled = false;
            OlimpiadyAnulujButton.Enabled = true;
            OlimpiadyOKButton.Enabled = true;
            dataSet.AcceptChanges();
        }

        private void OlimpiadyAnulujButton_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                tab.Enabled = true;
            }
            dataSet.RejectChanges();
            OlimpiadyDataGridView.Refresh();
            OlimpiadyDataGridView.Refresh();
            OlimpiadyDataGridView.ReadOnly = true;
            OlimpiadyEdytujButton.Enabled = true;
            OlimpiadyAnulujButton.Enabled = false;
            OlimpiadyOKButton.Enabled = false;
        }

        private void OlimpiadyOKButton_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                tab.Enabled = true;
            }
            dataAdapter =  new SqlDataAdapter("SELECT * FROM olimpiadyView", conn);

            SqlCommand command = new SqlCommand("exec dbo.updOlimp @Rok, @Miejsce, @Nawierzchnia, @Zloto, @Srebro, @Braz, @I_SET, @II_SET, @III_SET, @IV_SET, @V_SET", conn);
            command.Parameters.Add(new SqlParameter("@Miejsce", SqlDbType.VarChar, 30, "Miejsce"));
            command.Parameters.Add(new SqlParameter("@Nawierzchnia", SqlDbType.VarChar, 30, "Nawierzchnia"));
            command.Parameters.Add(new SqlParameter("@Zloto", SqlDbType.VarChar, 30, "Zloty medal"));
            command.Parameters.Add(new SqlParameter("@Srebro", SqlDbType.VarChar, 30, "Srebrny medal"));
            command.Parameters.Add(new SqlParameter("@Braz", SqlDbType.VarChar, 30, "Brazowy medal"));
            command.Parameters.Add(new SqlParameter("@I_SET", SqlDbType.VarChar, 30, "I_SET"));
            command.Parameters.Add(new SqlParameter("@II_SET", SqlDbType.VarChar, 30, "II_SET"));
            command.Parameters.Add(new SqlParameter("@III_SET", SqlDbType.VarChar, 30, "III_SET"));
            command.Parameters.Add(new SqlParameter("@IV_SET", SqlDbType.VarChar, 30, "IV_SET"));
            command.Parameters.Add(new SqlParameter("@V_SET", SqlDbType.VarChar, 30, "V_SET"));
            command.Parameters.Add(new SqlParameter("@Rok", SqlDbType.Int, 5, "Rok"));
            dataAdapter.UpdateCommand = command;
            dataAdapter.Update(dataSet, "OLIMPIADY");

            OlimpiadyDataGridView.ReadOnly = true;
            OlimpiadyEdytujButton.Enabled = true;
            OlimpiadyAnulujButton.Enabled = false;
            OlimpiadyOKButton.Enabled = false;
            refreshDataGridViews();
        }

        private void DodajZawodnikaButton_Click(object sender, EventArgs e)
        {
            DodajZawodnikaForm okienko = new DodajZawodnikaForm(connectionString, this);
            disableTabs();
            okienko.Visible = true;
        }

        private void disableTabs()
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                    tab.Enabled = false;
            }
        }
        public void enableTabs()
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                    tab.Enabled = true;
            }
        }

        private void UsunZawodnikaButton_Click(object sender, EventArgs e)
        {
            UsunZawodnika okienko = new UsunZawodnika(connectionString, this);
            disableTabs();
            okienko.Visible = true;
        }

        private void DodajTreneraButton_Click(object sender, EventArgs e)
        {
            DodajTrenera1 okienko = new DodajTrenera1(connectionString, this);
            disableTabs();
            okienko.Visible = true;
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            enableTabs();
        }

        private void DodajMastersButton_Click(object sender, EventArgs e)
        {
            DodajMasters ok = new DodajMasters(connectionString, this);
            disableTabs();
            ok.Visible = true;
        }

        private void DodajOlimpButton_Click(object sender, EventArgs e)
        {
            DodajOlimpiade ok = new DodajOlimpiade(connectionString, this);
            disableTabs();
            ok.Visible = true;
        }

        private void UsunOlimpButton_Click(object sender, EventArgs e)
        {
            usunOlimp ok = new usunOlimp(connectionString, this);
            disableTabs();
            ok.Visible = true;
        }

        private void UsunTreneraButton_Click(object sender, EventArgs e)
        {
            usunTrenera ok = new usunTrenera(connectionString, this);
            disableTabs();
            ok.Visible = true;
        }

        private void UsunMastersButton_Click(object sender, EventArgs e)
        {
            usunMasters ok = new usunMasters(connectionString, this);
            disableTabs();
            ok.Visible = true;
        }
    }
}