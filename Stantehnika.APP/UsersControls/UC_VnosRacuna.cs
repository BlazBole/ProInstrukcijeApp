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

namespace Stantehnika.APP.UsersControls
{
    public partial class UC_VnosRacuna : UserControl
    {
        public UC_VnosRacuna()
        {
            InitializeComponent();
            PripraviIzbiroRacunov();
            PripraviDatumeZaRacun();
        }

        #region private methods
        public void PripraviIzbiroRacunov()
        {
            cmbVrstaRacuna.Items.Clear();

            cmbVrstaRacuna.Items.Add("Račun");
            cmbVrstaRacuna.Items.Add("Predračun");

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

        public void ValidateStevikaRacuna()
        {
            // Odstranite vse presledke iz vnosnega polja
            string racunStevilka = tbStevikaRacuna.Text.Replace(" ", "");

            // Regex za format: LLLL-N ali LLLL-NN
            string pattern = @"^\d{4}-\d{1,2}$";

            // Preverite, ali se vnos ujema z zahtevanim formatom
            if (System.Text.RegularExpressions.Regex.IsMatch(racunStevilka, pattern))
            {
                // Ločimo leto in številko
                string[] parts = racunStevilka.Split('-');
                string leto = parts[0];  // LLLL (leto)
                string stevilka = parts[1];  // N ali NN

                // Preverimo, če je številka manjša od 10 in odstranimo vodilno ničlo, če obstaja
                if (int.TryParse(stevilka, out int number))
                {
                    if (number < 10)
                    {
                        stevilka = number.ToString();  // Zapišemo brez vodilne ničle
                    }

                    // Posodobimo številko računa brez vodilne ničle, če je to potrebno
                    tbStevikaRacuna.Text = $"{leto}-{stevilka}";

                    // Nastavite kazalec na konec besedila
                    tbStevikaRacuna.SelectionStart = tbStevikaRacuna.Text.Length;
                }
            }
            else
            {
                // Številka računa ni pravilna - prikažite sporočilo o napaki
                MessageBox.Show("Številka računa mora biti v formatu LLLL-N ali LLLL-NN (npr. 2024-1 ali 2024-23).",
                                "Napaka", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Ponastavite polje in fokusirajte polje
                tbStevikaRacuna.Clear();
                tbStevikaRacuna.Focus();
            }
        }



        #endregion private methods

        #region events

        private void pbPrikaziVecInfoPodjetja_Click(object sender, EventArgs e)
        {
            PregledInfoPodjetja form = new PregledInfoPodjetja();

            form.ShowDialog();
        }

        private void pbPrikaziVecInfoPodjetja_MouseEnter(object sender, EventArgs e)
        {
            pbPrikaziVecInfoPodjetja.Cursor = Cursors.Hand;
        } 

        private void pbPrikaziVecInfoPodjetja_MouseLeave(object sender, EventArgs e)
        {
            pbPrikaziVecInfoPodjetja.Cursor = Cursors.Default;
        }

        private void btnKoncajRacun_Click(object sender, EventArgs e)
        {
            ValidateStevikaRacuna();
        }

        private void tbIsciStranko_TextChanged(object sender, EventArgs e)
        {
            string input = tbIsciStranko.Text;

            // Ustvari instanco RacunManager
            RacunManager racunManager = new RacunManager();

            // Pokliči funkcijo za iskanje strank in vrni predloge
            List<Stranka> predlogiStrank = racunManager.IsciStranke(input);

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

        private void dataGridViewPredlogi_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Pridobimo izbrano stranko
                var selectedRow = dataGridViewPredlogi.Rows[e.RowIndex];
                string izbranaStranka = selectedRow.Cells["ImeInPriimek"].Value.ToString(); // Predpostavljamo, da je ime stranke v stolpcu z imenom "Stranka"

                // Nastavimo tbIsciStranko na izbrano ime stranke
                tbNaslovnikStranka.Text = izbranaStranka;

            }
        }
        #endregion events
    }
}
