using Stantehnika.Dal;
using Stantehnika.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Stantehnika.APP.UsersControls
{
    public partial class UC_Profil : UserControl
    {
        #region constructor
        public UC_Profil()
        {
            InitializeComponent();
            NaloziObrazecProfil();

        }
        #endregion constructor

        #region methods
        public void NaloziObrazecProfil()
        {
            RacunManager racunManager = new RacunManager();
            List<RacunGlava> izpisek = racunManager.GetOpravljenaDelaZaTekociMesec();

            dataGridView.DataSource = izpisek;

            dataGridView.Columns["StevilkaRacuna"].HeaderText = "Številka računa";
            dataGridView.Columns["Datum"].HeaderText = "Izdano";
            dataGridView.Columns["DatumOpravljeno"].HeaderText = "Opravljeno";
            dataGridView.Columns["SkupnaCena"].HeaderText = "Skupna cena";

            dataGridView.Columns["StevilkaRacuna"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["Datum"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["DatumOpravljeno"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns["SkupnaCena"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView.Columns["Datumzapade"].Visible = false;
            dataGridView.Columns["NazivPodjetja"].Visible = false;
            dataGridView.Columns["CenaDelo"].Visible = false;
            dataGridView.Columns["CenaMaterial"].Visible = false;
            dataGridView.Columns["Kraj"].Visible = false;
            dataGridView.Columns["RacunGlavaID"].Visible = false;
            dataGridView.Columns["StrankaID"].Visible = false;
            dataGridView.Columns["Stranka"].Visible = false;
            dataGridView.Columns["ImeInPriimek"].Visible = false;
            dataGridView.Columns["XMLPodatki"].Visible = false;

            var (prviDanTrenutnegaMeseca, zadnjiDanTrenutnegaMeseca, prviDanPrejsnjegaMeseca, zadnjiDanPrejsnjegaMeseca) = PridobiDatumskiObsegZaPrejsnjiMesec();
            PrikaziSkupniPrilivZaMesec(prviDanTrenutnegaMeseca, zadnjiDanTrenutnegaMeseca);
            PrikaziSkupniPrilivPrejsnjiMesec(prviDanPrejsnjegaMeseca, zadnjiDanPrejsnjegaMeseca);

            PrikaziSkupajIzdanihRacunov();
            PrikaziSkupniPrilivOdZacetka();
            PrikaziSteviloStrank();
        }

        public (DateTime prviDanMeseca, DateTime zadnjiDanMeseca) PridobiDatumskiObsegZaTekociMesec()
        {
            DateTime prviDanMeseca = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime zadnjiDanMeseca = prviDanMeseca.AddMonths(1).AddDays(-1);
            return (prviDanMeseca, zadnjiDanMeseca);
        }

        public (DateTime prviDanMeseca, DateTime zadnjiDanMeseca, DateTime prviDanPrejsnjegaMeseca, DateTime zadnjiDanPrejsnjegaMeseca) PridobiDatumskiObsegZaPrejsnjiMesec()
        {
            DateTime prviDanMeseca = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime zadnjiDanMeseca = prviDanMeseca.AddMonths(1).AddDays(-1);

            DateTime prviDanPrejsnjegaMeseca = prviDanMeseca.AddMonths(-1);
            DateTime zadnjiDanPrejsnjegaMeseca = prviDanMeseca.AddDays(-1);

            return (prviDanMeseca, zadnjiDanMeseca, prviDanPrejsnjegaMeseca, zadnjiDanPrejsnjegaMeseca);
        }

        private void PrikaziSkupniPrilivZaMesec(DateTime prviDanMeseca, DateTime zadnjiDanMeseca)
        {
            RacunManager racunManager = new RacunManager();
            decimal skupniPriliv = racunManager.GetSkupniPrilivZaMesec(prviDanMeseca, zadnjiDanMeseca);

            lblZnesekTekociMesec.Text = skupniPriliv.ToString("N2") + " €";
        }

        private void PrikaziSkupniPrilivPrejsnjiMesec(DateTime prviDanPrejsnjegaMeseca, DateTime zadnjiDanPrejsnjegaMeseca)
        {
            RacunManager racunManager = new RacunManager();
            decimal skupniPrilivPrejsnjiMesec = racunManager.GetSkupniPrilivZaMesec(prviDanPrejsnjegaMeseca, zadnjiDanPrejsnjegaMeseca);

            lblZnesekPrejsnjiMesec.Text = skupniPrilivPrejsnjiMesec.ToString("N2") + " €";
        }

        private void ShraniIzpisekStoritev()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            saveFileDialog.FileName = "Tabela_" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveDataGridViewToCSV(saveFileDialog.FileName);
            }
        }

        private void SaveDataGridViewToCSV(string filePath)
        {
            StringBuilder sb = new StringBuilder();

            var columnNames = dataGridView.Columns.Cast<DataGridViewColumn>();
            sb.AppendLine(string.Join(",", columnNames.Select(column => column.HeaderText)));

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                var cells = row.Cells.Cast<DataGridViewCell>();
                sb.AppendLine(string.Join(",", cells.Select(cell => cell.Value?.ToString()?.Replace(",", ";") ?? "")));
            }

            File.WriteAllText(filePath, sb.ToString());
        }

        private void PrikaziSkupajIzdanihRacunov()
        {
            RacunManager racunManager = new RacunManager();
            int skupajIzdanihRacunov = racunManager.GetSkupajIzdanihRacunov();

            lblSkupajIzdanihRacunov.Text = skupajIzdanihRacunov.ToString();
        }

        private void PrikaziSkupniPrilivOdZacetka()
        {
            RacunManager racunManager = new RacunManager();
            decimal skupniPriliv = racunManager.GetSkupniPrilivOdZacetka();

            lblSkupniPriliv.Text = skupniPriliv.ToString("N2") + " €";
        }

        private void PrikaziSteviloStrank()
        {
            RacunManager racunManager = new RacunManager();
            int steviloStrank = racunManager.GetSkupnoSteviloStrank();

            lblSkupnoSteviloStrank.Text = steviloStrank.ToString();
        }
        #endregion methods

        #region events
        private void lblPrenesiRacun_Click(object sender, EventArgs e)
        {
            ShraniIzpisekStoritev();
        }

        #endregion events
    }
}