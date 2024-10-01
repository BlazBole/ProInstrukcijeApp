using Stantehnika.Dal;
using Stantehnika.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stantehnika.APP
{
    public partial class VnosRacuna : Form
    {
        public VnosRacuna()
        {
            InitializeComponent();
            PripraviIzbiroRacunov();
            PripraviDatumeZaRacun();
            PripraviZadetkeZaStranke();
            PripraviTabeloPostavk();
        }

        #region private methods
        public void PripraviIzbiroRacunov()
        {
            cmbVrstaRacuna.Items.Clear();

            cmbVrstaRacuna.Items.Add("RAČUN");
            cmbVrstaRacuna.Items.Add("PREDRAČUN");

            cmbVrstaRacuna.SelectedIndex = 0;
        }

        public void PripraviDatumeZaRacun()
        {
            dtpDatum.Value = DateTime.Today;
            dtpDatumOpravljeno.Value = DateTime.Today;
            DateTime datumZapadlosti = DateTime.Today.AddDays(5);
            if (datumZapadlosti.DayOfWeek == DayOfWeek.Saturday)
            {
                datumZapadlosti = datumZapadlosti.AddDays(2);
            }
            else if (datumZapadlosti.DayOfWeek == DayOfWeek.Sunday)
            {
                datumZapadlosti = datumZapadlosti.AddDays(1);
            }

            dtpDatumZapade.Value = datumZapadlosti;
        }

        public void PripraviZadetkeZaStranke()
        {
            dataGridViewPredlogi.Visible = false;
            lblNaslovnikPodjetje.Visible = false;
            gbNaslovnikPodjetje.Visible= false;
            lblIzberiStranko.Visible = false;
            gbPodatkiFizicneOsebe.Visible = false;
        }

        public void ValidateStevikaRacuna()
        {
            string racunStevilka = tbStevikaRacuna.Text.Replace(" ", "");

            string pattern = @"^\d{4}-\d{1,2}$";

            if (System.Text.RegularExpressions.Regex.IsMatch(racunStevilka, pattern))
            {
                string[] parts = racunStevilka.Split('-');
                string leto = parts[0];  
                string stevilka = parts[1]; 

                if (int.TryParse(stevilka, out int number))
                {
                    if (number < 10)
                    {
                        stevilka = number.ToString();  
                    }

                    tbStevikaRacuna.Text = $"{leto}-{stevilka}";

                    tbStevikaRacuna.SelectionStart = tbStevikaRacuna.Text.Length;
                }
            }
            else
            {
                MessageBox.Show("Številka računa mora biti v formatu LLLL-N ali LLLL-NN (npr. 2024-1 ali 2024-23).",
                                "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);

                tbStevikaRacuna.Clear();
                tbStevikaRacuna.Focus();
            }
        }

        public void PripraviTabeloPostavk()
        {
            //TODO
        }

        #endregion private methods

        #region events
        private void pbPrikaziVecInfoPodjetja_Click_1(object sender, EventArgs e)
        {
            PregledInfoPodjetja form = new PregledInfoPodjetja();

            form.ShowDialog();
        }

        private void pbPrikaziVecInfoPodjetja_MouseEnter_1(object sender, EventArgs e)
        {
            pbPrikaziVecInfoPodjetja.Cursor = Cursors.Hand;
        }

        private void pbPrikaziVecInfoPodjetja_MouseLeave_1(object sender, EventArgs e)
        {
            pbPrikaziVecInfoPodjetja.Cursor = Cursors.Default;
        }

        private void btnKoncajRacun_Click_1(object sender, EventArgs e)
        {
            ValidateStevikaRacuna();
        }

        private void tbIsciStranko_TextChanged_1(object sender, EventArgs e)
        {
            string input = tbIsciStranko.Text;

            if(input.Length >= 2 && input != "Išči stranko...")
            {

                // Ustvari instanco RacunManager
                RacunManager racunManager = new RacunManager();

                // Pokliči funkcijo za iskanje strank in vrni predloge
                List<Stranka> predlogiStrank = racunManager.IsciStranke(input);

                if(predlogiStrank.Count > 0)
                {
                    dataGridViewPredlogi.Visible = true;
                    lblIzberiStranko.Visible = true;
                }
                else
                {
                  
                }

                // Prikaz predlogov v DataGridView ali ListBox
                dataGridViewPredlogi.DataSource = predlogiStrank;

                dataGridViewPredlogi.Columns["ImeInPriimek"].HeaderText = "Stranka";
                dataGridViewPredlogi.Columns["Email"].HeaderText = "E-naslov";


                dataGridViewPredlogi.Columns["StrankaID"].Visible = false;
                dataGridViewPredlogi.Columns["UlicaInHisnaStevilka"].Visible = false;
                dataGridViewPredlogi.Columns["PostaInKraj"].Visible = false;
                dataGridViewPredlogi.Columns["SedezPodjetja"].Visible = false;
                dataGridViewPredlogi.Columns["NazivPodjetja"].Visible = false;
                dataGridViewPredlogi.Columns["SedezPodjetja"].Visible = false;
                dataGridViewPredlogi.Columns["Naslov"].Visible = false;
                dataGridViewPredlogi.Columns["DavcnaStevilka"].Visible = false;

                dataGridViewPredlogi.Columns["ImeInPriimek"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridViewPredlogi.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            else
            {
                dataGridViewPredlogi.Visible = false;
                lblIzberiStranko.Visible = false;
            }
        }

        private void dataGridViewPredlogi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Pridobimo izbrano stranko
                var selectedRow = dataGridViewPredlogi.Rows[e.RowIndex];
                string izbranaStranka = selectedRow.Cells["ImeInPriimek"].Value.ToString(); // Predpostavljamo, da je ime stranke v stolpcu z imenom "Stranka"


                // Nastavimo tbIsciStranko na izbrano ime stranke
                lblNaslovnikPodjetje.Text = izbranaStranka;

                lblIzberiStranko.Visible = false;
                dataGridViewPredlogi.Visible = false;
                tbIsciStranko.Text = "Išči stranko...";
                lblNaslovnikPodjetje.Visible = true;

                RacunManager racunManager = new RacunManager();
                Stranka izbranaStrankaPodatki = racunManager.PridobiPodrobnostiStranke(izbranaStranka);

                // Preverimo, če je izbrano podjetje, ne fizicna oseba
                if (izbranaStrankaPodatki != null && izbranaStrankaPodatki.NazivPodjetja != null)
                {
                    // Izpolnimo podatke za podjetje
                    lblDavcnaStevilkaPodjetje.Text = izbranaStrankaPodatki.DavcnaStevilka;
                    lblPEPodjetje.Text = izbranaStrankaPodatki.SedezPodjetja;
                    lblEnaslovPodjetje.Text = izbranaStrankaPodatki.Email;
                    gbNaslovnikPodjetje.Visible = true;

                }
                else
                {
                    // Izpolnimo podatke za fizično osebo
                    lblStrankaFizicnaOseba.Text = izbranaStrankaPodatki.ImeInPriimek;
                    lblNaslovFizicnaOseba.Text = $"{izbranaStrankaPodatki.UlicaInHisnaStevilka}, {izbranaStrankaPodatki.PostaInKraj}";
                    lblEnaslovFizicnaOseba.Text = izbranaStrankaPodatki.Email;
                    gbPodatkiFizicneOsebe.Visible = true;
                }

                // Skrij dataGridView in resetiraj iskalno polje
                dataGridViewPredlogi.Visible = false;
                tbIsciStranko.Text = "Išči stranko...";
            }
        }

        private void tbIsciStranko_Click(object sender, EventArgs e)
        {
            if (tbIsciStranko.Text == "Išči stranko...")
            {
                tbIsciStranko.Text = "";
                gbNaslovnikPodjetje.Visible = false;
                gbPodatkiFizicneOsebe.Visible = false;
            }
        }

        private void tbIsciStranko_Leave(object sender, EventArgs e)
        {
            if (tbIsciStranko.Text == "")
            {
                tbIsciStranko.Text = "Išči stranko...";
            }
        }


        private void cmbVrstaRacuna_TextChanged(object sender, EventArgs e)
        {
            if(cmbVrstaRacuna.Text == "RAČUN")
            {
                lblŠtevilkaRacunaInfo.Visible = true;
                tbStevikaRacuna.Visible = true;
                lblDatumOpravljenoInfo.Visible = true;
                dtpDatumOpravljeno.Visible = true;
                lblDatumZapadeInfo.Visible = true;
                dtpDatumZapade.Visible = true;
            }
            else if(cmbVrstaRacuna.Text == "PREDRAČUN")
            {
                lblŠtevilkaRacunaInfo.Visible = false;
                tbStevikaRacuna.Visible = false;
                lblDatumOpravljenoInfo.Visible = false;
                dtpDatumOpravljeno.Visible = false;
                lblDatumZapadeInfo.Visible = false;
                dtpDatumZapade.Visible = false;
            }
        }

        #endregion events

    }
}
